namespace Blood_Link.Models
{
    public class AppointmentRequest
    {
        public int AppointmentRequestId { get; set; }
        public int ClientId {  get; set; }
        public Client Client { get; set; }
        public int AppointmentSetupId {  get; set; }
        public AppointmentSetup AppointmentSetup { get; set; }
        public bool isAppointed { get; set; }
        public bool isActive { get; set; }
    }
}
