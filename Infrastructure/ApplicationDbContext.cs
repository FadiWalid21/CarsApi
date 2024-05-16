using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser,ApplicationRole,int>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public ApplicationDbContext()
        {
        }

        public DbSet<Car> Cars { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<Brand> Brands { get; set; }

        public DbSet<UserFavProducts> UserFavProducts { get; set; }
        // public DbSet<CarModel> CarModels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Car>()
                .Property(c => c.MaxPrice)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Car>()
                .Property(c => c.MinPrice)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Car>()
        .HasOne(c => c.Model)
        .WithMany(m => m.Cars)
        .HasForeignKey(c => c.ModelId)
        .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<UserFavProducts>()
                .HasKey(ufp => new { ufp.UserId, ufp.ProductId });

            modelBuilder.Entity<UserFavProducts>()
                .HasOne<ApplicationUser>()
                .WithMany(u => u.FavouriteCars)
                .HasForeignKey(ufp => ufp.UserId);

            modelBuilder.Entity<UserFavProducts>()
                .HasOne<Car>()
                .WithMany(c => c.FavouriteCars)
                .HasForeignKey(ufp => ufp.ProductId);
        }
    }
}
