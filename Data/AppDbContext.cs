using Microsoft.EntityFrameworkCore;
using Payroll.Models;
using System.Collections.Generic;

namespace Payroll.Data
{

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {

        }

        public DbSet<JobDtlM> JobTitles { get; set; }
        public DbSet<EmployeeM> Employees { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmployeeM>()
                .HasKey(e => e.EmployeeId);

            modelBuilder.Entity<JobDtlM>()
                .HasKey(j => j.JobId); 
        }
    }
   
}
