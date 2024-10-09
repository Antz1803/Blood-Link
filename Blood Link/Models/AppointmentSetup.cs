using System.ComponentModel.DataAnnotations;

namespace Blood_Link.Models
{
    public class AppointmentSetup
    {
        public int AppointmentSetupId {  get; set; }
        public int NurseId { get; set; }
        public Nurse Nurse { get; set; }
        public DateTime DateCreated { get; set; }
        public string Name { get;set; }
        public string Location { get; set; }
        public string Desc { get; set; }
        public bool isActive { get; set; }
    }
}
