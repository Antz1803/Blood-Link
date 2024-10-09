namespace Blood_Link.Models
{
    public class Nurse
    {
        public int NurseId { get; set; }
        public int personId { get; set; }
        public Person? Person { get; set; }
    }
}
