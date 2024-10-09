using Blood_Link.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.ExceptionServices;

namespace Blood_Link.Controllers
{
    public class NurseController : Controller
    {
        readonly BloodLinkDbContext unitOfWork;
        public NurseController(BloodLinkDbContext _db)
        {
            unitOfWork = _db;
        }

        UsersVM getNurse()
        {
            int? id = int.Parse(User.FindFirst("id").Value);

            UsersVM usersVM = new UsersVM
            {
                person = unitOfWork.Persons.First(p => p.personId == id),
                Nurse = unitOfWork.Nurses.First(p => p.Person.personId == id)
            };

            return usersVM;
        }

        public IActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("UserLogin", "Login");
            }

            return View(getNurse());
        }

        public IActionResult Appointment()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("UserLogin", "Login");
            }

            AppointmentSetup appointmentSetup = unitOfWork.AppointmentSetups.Include(a => a.Nurse).FirstOrDefault(p => p.Nurse.personId == int.Parse(User.FindFirst("id").Value) && p.isActive);
            return View(appointmentSetup);
        }

        public IActionResult SetAppointment(AppointmentSetup appointmentSetup)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("UserLogin", "Login");
            }

            if (unitOfWork.AppointmentSetups.FirstOrDefault(a => a.Name == appointmentSetup.Name) != null)
            {
                ModelState.AddModelError("", "Appointment name already exists.");
                return View(appointmentSetup);
            }

            ModelState.Remove("Nurse");
            appointmentSetup.Nurse = unitOfWork.Nurses.Include(n => n.Person).First(n => n.Person.personId == int.Parse(User.FindFirst("id").Value));
            appointmentSetup.DateCreated = DateTime.Now;
            appointmentSetup.isActive = true;

            if (!ModelState.IsValid)    
            {
                return View(appointmentSetup);
            }

            unitOfWork.AppointmentSetups.Add(appointmentSetup);
            unitOfWork.SaveChanges();
            
            return RedirectToAction("Appointment");
        }

        public IActionResult EndAppointment(int AppointmentSetupId) 
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("UserLogin", "Login");
            }

            AppointmentSetup appointmentSetup = unitOfWork.AppointmentSetups.First(a => a.AppointmentSetupId == AppointmentSetupId);
            appointmentSetup.isActive = false;
            unitOfWork.AppointmentSetups.Update(appointmentSetup);
            unitOfWork.SaveChanges();

            return RedirectToAction("Appointment");
        }

        public IActionResult AppointmentHistory()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("UserLogin", "Login");
            }

            Nurse nurse = unitOfWork.Nurses.Include(n => n.Person).First(n => n.personId == int.Parse(User.FindFirst("id").Value));
            List<AppointmentSetup> appointmentSetups = unitOfWork.AppointmentSetups.Where(a => a.NurseId == nurse.NurseId && !a.isActive).ToList();

            return View(appointmentSetups);
        }

        public IActionResult ViewClients(int AppointmentSetupId)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("UserLogin", "Login");
            }

            var result = (from appSetup in unitOfWork.AppointmentSetups
                                    join appRequest in unitOfWork.AppointmentRequests on appSetup.AppointmentSetupId equals appRequest.AppointmentSetupId
                                    join cl in unitOfWork.Clients on appRequest.ClientId equals cl.clientId
                                    join p in unitOfWork.Persons on cl.Person.personId equals p.personId
                                    where appSetup.AppointmentSetupId == AppointmentSetupId
                                    select new { p, appSetup.Name }).ToList();

            List<Person> persons = new List<Person>();

            ViewData["name"] = unitOfWork.AppointmentSetups.Where(a => a.AppointmentSetupId == AppointmentSetupId).Select(a => a.Name).First();
            foreach (var item in result)
            {
                persons.Add(item.p);
            }

            return View(persons);
        }
    }
}
