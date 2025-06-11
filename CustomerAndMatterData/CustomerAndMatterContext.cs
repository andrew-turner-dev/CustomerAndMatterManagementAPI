using CustomerAndMatterData.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerAndMatterData
{
    public class CustomerAndMatterContext : DbContext
    {

        public DbSet<Lawyer> Lawyers { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Matter> Matters { get; set; }

        public CustomerAndMatterContext(DbContextOptions<CustomerAndMatterContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // TODO: Update connection string to use app settings 
            optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=CustomerAndMatter;User Id=postgres;Password=postgres;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //TODO: Make sure this does not run on prod
            modelBuilder.Entity<Lawyer>().HasData(
                new Lawyer { Id = 1, FirstName = "Harvey", LastName = "Dent", Firm="Firm1", LoginEmail = "harvey.dent@cityofgotham.com", Password = "two-faced", IsAdmin = true },
                new Lawyer { Id = 2, FirstName = "Saul", LastName = "Goodman", Firm="Firm2", LoginEmail = "agoodman@goodlaw.com", Password = "callMe", IsAdmin = false }
            );


            modelBuilder.Entity<Customer>().HasData(
                new Customer { Id = 1, Name = "Customer1", PhoneNumber = "1-111-111-1111", Email = "customer1@customer1.com", LawyerId = 1},
                new Customer { Id = 2, Name = "Customer2", PhoneNumber = "2-222-222-2222", Email = "customer12@customer12.com", LawyerId = 2 }
                );



            modelBuilder.Entity<Matter>().HasData(
                new Matter { Id = 1, Name = "Name of Case 1", Description = "Case 1 description", IsClosed = false, CustomerId  = 1, LawyerId = 1 },
                new Matter { Id = 2, Name = "Name of Case 2", Description = "Case 2 description", IsClosed = false, CustomerId = 2, LawyerId = 2 }
            );

        }



    }
}
