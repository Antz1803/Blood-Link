using Blood_Link.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Blood_Link.Controllers
{
    public class WalletController : Controller
    {
        readonly BloodLinkDbContext unitOfWork;
        public WalletController(BloodLinkDbContext _db)
        {
            unitOfWork = _db;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult LinkPhantomWallet(string walletPublicKey) 
        {
            // Get the currently logged-in user (example with Identity)
            int? id = int.Parse(User.FindFirst("id").Value); // id refers to personId
            Person person = unitOfWork.Persons.SingleOrDefault(p => p.personId == id);

            if (person != null)
            {
                // Store the Phantom wallet public key in the database for this user
                person.WalletAddress = walletPublicKey;
                unitOfWork.SaveChanges();

                return Json(new { success = true });
            }

            return Json(new { success = false });
        }

        [HttpPost]
        public async Task<IActionResult> CheckPhantomLogin(string walletPublicKey)
        {
            // Query to check if this wallet is already associated with a user
            var user = await unitOfWork.Persons.SingleOrDefaultAsync(u => u.WalletAddress == walletPublicKey && u.personId == int.Parse(User.FindFirst("id").Value));

            if (user != null)
            {
                return Json(new { success = true });
            }

            return Json(new { success = false });
        }

        public  IActionResult RemovePhantomWallet()
        {
            Person person = unitOfWork.Persons.FirstOrDefault(p => p.personId == int.Parse(User.FindFirst("id").Value));

            if (person != null)
            {
                person.WalletAddress = "";
                unitOfWork.SaveChanges();

                return Json(new { success = true });
            }

            return Json(new { success = false });
        }

    }
}
