using Blood_Link.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace Blood_Link.Controllers
{
    public class DoctorController : Controller
    {
        readonly BloodLinkDbContext unitOfWork;

        public DoctorController(BloodLinkDbContext _db)
        {
            unitOfWork = _db;
        }

        public IActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("UserLogin", "Login");
            }

            return View();
        }

        public IActionResult Nurse(Person nurse)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("UserLogin", "Login");
            }

            ViewBag.error = TempData["error"] ?? false;
            return View(nurse);
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

        public IActionResult AddNurse(Person person)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("UserLogin", "Login");
            }

            ModelState.Remove("WalletAddress");

            if (!ModelState.IsValid)
            {
                TempData["error"] = true;
                return RedirectToAction("Nurse", "Doctor");
            }

            person.WalletAddress = "";
            person.Password = getEncryptPassword(person.Password);

            unitOfWork.Persons.Add(person);

            Nurse nurse = new Nurse
            {
                Person = person
            };

            unitOfWork.Nurses.Add(nurse);
            unitOfWork.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
