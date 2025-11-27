
using Microsoft.EntityFrameworkCore;

namespace ClinicAppointmentSystem.BOL
{
    public class ClinicAppointmentContext : DbContext
    {
        public ClinicAppointmentContext(DbContextOptions<ClinicAppointmentContext> options)
            : base(options)
        {
        }

        public DbSet<Doctor> Doctor { get; set; }
        public DbSet<Patient> Patient { get; set; }
        public DbSet<Appointment> Appointment { get; set; }
    }
}
