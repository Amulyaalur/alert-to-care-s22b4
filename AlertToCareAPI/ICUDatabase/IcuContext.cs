using Entities;
using Microsoft.EntityFrameworkCore;
public class IcuContext : DbContext
{
    public IcuContext(DbContextOptions<IcuContext> options) : base(options)
    {
        Database.Migrate();
    }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<PatientAddress> PatientAddress { get; set; }
    public DbSet<ICU> ICUs { get; set; }
    public DbSet<Vitals> Vitals { get; set; }
}