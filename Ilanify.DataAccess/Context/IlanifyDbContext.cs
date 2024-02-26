using Ilanify.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Ilanify.DataAccess.Context
{
    public class IlanifyDbContext : IdentityDbContext<ApplicationUser>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Server=localhost;Database=Ilanify;User Id=sa;Password=myPassw0rd;TrustServerCertificate=True;");
        }

        public DbSet<RealEstate> RealEstates { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<RealEstateImage> RealEstateImages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<RealEstate>()
                .HasOne(r => r.Location)
                .WithMany(l => l.RealEstates)
                .HasForeignKey(r => r.LocationId);

            modelBuilder.Entity<RealEstate>()
                .HasOne(r => r.Category)
                .WithMany(c => c.RealEstates)
                .HasForeignKey(r => r.CategoryId);

            modelBuilder.Entity<RealEstate>()
                .HasOne(r => r.ApplicationUser)
                .WithMany(u => u.RealEstates)
                .HasForeignKey(r => r.ApplicationUserId);

            modelBuilder.Entity<RealEstateImage>()
                .HasOne(i => i.RealEstate)
                .WithMany(r => r.Images)
                .HasForeignKey(i => i.RealEstateId);
        }
    }
}