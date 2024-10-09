using Blood_Link.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Blood_Link.Controllers
{
    public class Login : Controller
    {
        readonly BloodLinkDbContext unitOfWork;

        public Login(BloodLinkDbContext _db)
        {
            unitOfWork = _db;
        }

        public IActionResult Index()
        {
            ViewBag.success = TempData["success"] ?? false;
            ViewBag.rerror = TempData["RError"] ?? "";
            ViewBag.error = TempData["Error"] ?? false;
            return View();
        }

        string getEncryptPassword(string pass)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(pass));

                StringBuilder result = new StringBuilder();
                foreach (byte b in bytes)
                {
                    result.Append(b.ToString("x2"));
                }

                return result.ToString();
            }
        }

        public IActionResult UserLogin(Person? tPerson)
        {
            string? username = tPerson?.Username ?? "";
            string? password = tPerson?.Password ?? "";

            if (username == "" || password == "")
            {
                TempData["Error"] = true;
                return RedirectToAction("Index");
            }

            string EncrpytPassword = getEncryptPassword(password);
            Person? person =  unitOfWork.Persons.FirstOrDefault(p => p.Username == tPerson.Username && p.Password == EncrpytPassword);

            if (person == null)
            {
                TempData["Error"] = true;
                return RedirectToAction("Index");
            }

            var claims = new List<Claim>
            {
                new Claim("role", person.Role), 
                new Claim("id", person.personId.ToString()),
                new Claim("Waddress", person.WalletAddress ?? "")
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            switch (person.Role) 
            {
                case "Nurse":
                    return RedirectToAction("Index", "Nurse");
                case "Doctor":
                    return RedirectToAction("Index", "Doctor");
                default:
                    return RedirectToAction("Index", "Client");
            }
        }

        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Login");
        }

        public IActionResult Register(string? Username, string? Password)
        {
            if (Username == null || Password == null || Username.Trim() == "" || Password.Trim() == "")
            {
                TempData["RError"] = "empty";
                return RedirectToAction("Index");
            }

            if (unitOfWork.Persons.FirstOrDefault(p => p.Username == Username) != null)
            {
                TempData["RError"] = "exist";
                return RedirectToAction("Index");
            }

            Person person = new Person 
            {
                Username = Username,
                Password = getEncryptPassword(Password),
                Role = "Client",
                BirthDate = "",
                BloodType = "",
                FirstName = "",
                LastName = "",
                Email = "",
                ContactNo = "",
                WalletAddress = "",
                Gender = "",
                MiddleName = "",
            };

            unitOfWork.Persons.Add(person);
            unitOfWork.SaveChanges();

            Client client = new Client
            {
                KapilaNaHospital = 0,
                Person = person,
            };

            unitOfWork.Clients.Add(client);
            unitOfWork.SaveChanges();

            TempData["success"] = true;
            return RedirectToAction("Index");
        }
    }
}
