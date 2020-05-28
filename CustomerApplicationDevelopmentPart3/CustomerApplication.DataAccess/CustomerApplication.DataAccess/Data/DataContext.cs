
using CustomerApplication.Model.Models;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.IO.Compression;

namespace CustomerApplication.DataAccess.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Employee> Users { get; set; }

        public DbSet<Company> Companies { get; set; }

        public DbSet<Inventory> Inventories { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {



            modelBuilder.Entity<Company>()
                .HasMany(c => c.Employees)
                .WithOne(e => e.Company);
            modelBuilder.Entity<Company>()
               .HasMany(c => c.Inventories)
               .WithOne(e => e.Company);

        }
        


    }
}