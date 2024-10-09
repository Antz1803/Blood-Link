namespace Blood_Link.Models
{
    public class AppointmentVM
    {
        public AppointmentRequest AppointmentRequest { get; set; }
        public AppointmentSetup CurrentAppointment { get; set; }
        public List<AppointmentSetup> AppointmentSetups { get; set; }
    }
}
