using Microsoft.EntityFrameworkCore;

namespace Blood_Link.Models
{
    public class BloodLinkDbContext : DbContext
    {
        public BloodLinkDbContext(DbContextOptions<BloodLinkDbContext> options) : base(options) { }

        public DbSet<Person> Persons { get; set;}
        public DbSet<Client> Clients { get; set;}
        public DbSet<Doctor> Doctors { get; set;}
        public DbSet<Nurse> Nurses { get; set;}
        public DbSet<AppointmentSetup> AppointmentSetups { get; set;}
        public DbSet<AppointmentRequest> AppointmentRequests { get; set;}
    }
}
