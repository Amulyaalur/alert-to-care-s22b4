using Entities;
using Microsoft.EntityFrameworkCore;
public class IcuContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Filename=PatientDatabase.db");
    }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<PatientAddress> PatientAddress { get; set; }
    public DbSet<ICU> ICUs { get; set; }
    public DbSet<Vitals> Vitals { get; set; }
    public DbSet<Beds> Beds { get; set; }
}