using Blood_Link.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace Blood_Link.Controllers
{
    public class ClientController : Controller
    {
        readonly BloodLinkDbContext unitOfWork;

        public ClientController(BloodLinkDbContext _db)
        {
            unitOfWork = _db;
        }

        bool isAccountEmpty()
        {
            TempData["empty"] = false;

            int? id = int.Parse(User.FindFirst("id").Value);
            Person person = unitOfWork.Persons.First(p => p.personId == id);
            if (person.FirstName == "")
            {
                TempData["empty"] = true;
                return true;
            }
            return false;
        }

        public IActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("UserLogin", "Login");
            }

           if (isAccountEmpty())
           {
                return RedirectToAction("PersonalDetails");
           }

           return View(getClient());
        }

        UsersVM getClient()
        {
            int? id = int.Parse(User.FindFirst("id").Value);

            UsersVM usersVM = new UsersVM
            {
                person = unitOfWork.Persons.First(p => p.personId == id),
                client = unitOfWork.Clients.FirstOrDefault(p => p.Person.personId == id)
            };

            return usersVM;
        }

        public IActionResult PersonalDetails()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("UserLogin", "Login");
            }

            ViewBag.message = TempData["message"] ?? "";
            ViewBag.empty = TempData["empty"] ?? false;
            return View(getClient());
        }

        public IActionResult UpdateInformation(Person person)
        {
            ModelState.Remove("Role");
            ModelState.Remove("Username");
            ModelState.Remove("Password");
            ModelState.Remove("WalletAddress");

            TempData["message"] = "";
            if (!ModelState.IsValid)
            {
                TempData["message"] = "error";
            }
            else
            {
                Person uPerson = unitOfWork.Persons.First(p => p.personId == person.personId);
                uPerson.LastName = person.LastName;
                uPerson.FirstName = person.FirstName;
                uPerson.MiddleName = person.MiddleName;
                uPerson.BloodType = person.BloodType;
                uPerson.ContactNo = person.ContactNo;
                uPerson.Email = person.Email;
                uPerson.Gender = person.Gender;
                uPerson.BirthDate = person.BirthDate;

                TempData["message"] = "success";
                unitOfWork.Persons.Update(uPerson);
                unitOfWork.SaveChanges();
            }

            return RedirectToAction("PersonalDetails");
        }

        public IActionResult Appointment()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("UserLogin", "Login");
            }

            if (isAccountEmpty())
            {
                return RedirectToAction("PersonalDetails");
            }

            List<AppointmentSetup> appointmentSetups = unitOfWork.AppointmentSetups.Where(a => a.isActive).ToList();
            AppointmentSetup? CurrentAppointment = (from setup in unitOfWork.AppointmentSetups
                         join request in unitOfWork.AppointmentRequests
                         on setup.AppointmentSetupId equals request.AppointmentSetupId
                         join cl in unitOfWork.Clients on request.ClientId equals cl.clientId
                         where cl.Person.personId == int.Parse(User.FindFirst("id").Value)
                         && request.isActive && setup.isActive
                         select setup).FirstOrDefault();

            appointmentSetups.Remove(CurrentAppointment);
            
            AppointmentVM appointmentVM = new AppointmentVM
            {
                CurrentAppointment = CurrentAppointment,
                AppointmentSetups = appointmentSetups
            };
            return View(appointmentVM);
        }

        [HttpPost]
        public IActionResult Appoint(int AppointmentSetupId)
        {
            Client client = unitOfWork.Clients.First(c => c.Person.personId == int.Parse(User.FindFirst("id").Value));

            AppointmentRequest appointmentRequest = new AppointmentRequest
            {
                ClientId = client.clientId,
                AppointmentSetupId = AppointmentSetupId,
                isAppointed = true,
                isActive = true,
            };

            unitOfWork.AppointmentRequests.Add(appointmentRequest);
            unitOfWork.SaveChanges();

            return RedirectToAction("Appointment");
        }

        public IActionResult CancelAppointment(int AppointmentSetupId)
        {


            AppointmentRequest appRequest = unitOfWork.AppointmentRequests.First(a => a.AppointmentSetupId == AppointmentSetupId);
            unitOfWork.AppointmentRequests.Remove(appRequest);
            unitOfWork.SaveChanges();
            return RedirectToAction("Appointment");
        }
    }
}
