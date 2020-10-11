using Entities;
using Microsoft.EntityFrameworkCore;
using System;

public class IcuContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Filename=PatientDatabase.db");
    }
  /*  protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Vitals>().HasData(new Vitals
        {
            PatientId ="MRN001",
            Spo2 = 10,
            Bpm = 12,
            RespRate = 134

        }, new Vitals
        {
            PatientId = "MRN002",
            Spo2 = 80,
            Bpm = 112,
            RespRate = 134
        },
        new Vitals
        {
            PatientId = "MRN003",
            Spo2 = 180,
            Bpm = 120,
            RespRate = 138
        });
    }*/
    public DbSet<Patient> Patients { get; set; }
    public DbSet<PatientAddress> PatientAddress { get; set; }
    public DbSet<ICU> ICUs { get; set; }
    public DbSet<Vitals> Vitals { get; set; }
    public DbSet<Beds> Beds { get; set; }
}