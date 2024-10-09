using System.ComponentModel.DataAnnotations;

namespace Blood_Link.Models
{
    public class Person
    {
        public int personId { get; set; }
        public string Gender { get; set; } 
        public string Role { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string BloodType {  get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string BirthDate { get; set; }
        public string ContactNo {  get; set; }
        public string WalletAddress { get; set; }
    }
}
