namespace Blood_Link.Models
{
    public class Doctor
    {
        public int DoctorId { get; set; }
        public int personId { get; set; }
        public Person? Person { get; set; }
    }
}
