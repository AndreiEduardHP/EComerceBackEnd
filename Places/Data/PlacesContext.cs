using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using Places.Models;
using System.Diagnostics;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Places.Data
{
    public class PlacesContext : DbContext
    {
        public PlacesContext(DbContextOptions<PlacesContext> options) : base(options)
        {

        }
        public DbSet<UserProfile> UserProfile { get; set; }

        public DbSet<Feedback> Feedbacks { get; set; }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Company> Company { get; set; }

        public DbSet<UserProductLimit> UserProductLimits { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasOne(d => d.User)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.NoAction); 

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.AddressId)
                    .OnDelete(DeleteBehavior.Cascade); 
            });




        }
    }
}
