using AlertToCareAPI.ICUDatabase.Entities;
using Microsoft.EntityFrameworkCore;

namespace AlertToCareAPI.ICUDatabase
{
    public class IcuContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=PatientDatabase.db");
        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<PatientAddress> PatientAddress { get; set; }
        public DbSet<ICU> IcuList { get; set; }
        public DbSet<Vitals> Vitals { get; set; }
        public DbSet<Beds> Beds { get; set; }
    }

}