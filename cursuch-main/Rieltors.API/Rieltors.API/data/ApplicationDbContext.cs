using Microsoft.EntityFrameworkCore;
using Rieltors.API.Models;

namespace Rieltors.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Realtor> Realtors { get; set; }
        public DbSet<Seller> Sellers { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<Deal> Deals { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Admin configuration
            modelBuilder.Entity<Admin>(entity =>
            {
                entity.HasKey(a => a.Id);
                entity.HasIndex(a => a.Email).IsUnique();
                entity.HasIndex(a => a.Username).IsUnique();
                entity.Property(a => a.FullName).IsRequired().HasMaxLength(100);
                entity.Property(a => a.Email).IsRequired().HasMaxLength(100);
                entity.Property(a => a.Username).IsRequired().HasMaxLength(50);
                entity.Property(a => a.PasswordHash).IsRequired();
                entity.Property(a => a.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
                entity.Property(a => a.IsActive).HasDefaultValue(true);
            });

            // Client configuration
            modelBuilder.Entity<Client>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.HasIndex(c => c.Email).IsUnique();
                entity.HasIndex(c => c.PhoneNumber).IsUnique();
                entity.Property(c => c.FirstName).IsRequired().HasMaxLength(50);
                entity.Property(c => c.LastName).IsRequired().HasMaxLength(50);
                entity.Property(c => c.PhoneNumber).IsRequired().HasMaxLength(20);
                entity.Property(c => c.Email).IsRequired().HasMaxLength(100);
                entity.Property(c => c.MinPrice).HasColumnType("decimal(18,2)");
                entity.Property(c => c.MaxPrice).HasColumnType("decimal(18,2)");
                entity.Property(c => c.Status).HasDefaultValue("Активный поиск");
                entity.Property(c => c.RegistrationDate).HasDefaultValueSql("GETUTCDATE()");
                entity.Property(c => c.IsActive).HasDefaultValue(true);
            });

            // Realtor configuration
            modelBuilder.Entity<Realtor>(entity =>
            {
                entity.HasKey(r => r.Id);
                entity.HasIndex(r => r.Email).IsUnique();
                entity.HasIndex(r => r.LicenseNumber).IsUnique();
                entity.Property(r => r.FullName).IsRequired().HasMaxLength(100);
                entity.Property(r => r.Email).IsRequired().HasMaxLength(100);
                entity.Property(r => r.Phone).IsRequired().HasMaxLength(20);
                entity.Property(r => r.LicenseNumber).IsRequired().HasMaxLength(50);
                entity.Property(r => r.Rating).HasDefaultValue(0);
                entity.Property(r => r.CompletedDeals).HasDefaultValue(0);
                entity.Property(r => r.IsAvailable).HasDefaultValue(true);
                entity.Property(r => r.HireDate).HasDefaultValueSql("GETUTCDATE()");
            });

            // Seller configuration
            modelBuilder.Entity<Seller>(entity =>
            {
                entity.HasKey(s => s.Id);
                entity.HasIndex(s => s.Email).IsUnique();
                entity.Property(s => s.FullName).IsRequired().HasMaxLength(100);
                entity.Property(s => s.Email).IsRequired().HasMaxLength(100);
                entity.Property(s => s.Phone).IsRequired().HasMaxLength(20);
                entity.Property(s => s.RegistrationDate).HasDefaultValueSql("GETUTCDATE()");
            });

            // Property configuration
            modelBuilder.Entity<Property>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Address).IsRequired().HasMaxLength(200);
                entity.Property(p => p.PropertyType).IsRequired().HasMaxLength(50);
                entity.Property(p => p.Price).HasColumnType("decimal(18,2)");
                entity.Property(p => p.IsActive).HasDefaultValue(true);
                entity.Property(p => p.CreatedAt).HasDefaultValueSql("GETUTCDATE()");

                entity.HasOne(p => p.Seller)
                    .WithMany(s => s.Properties)
                    .HasForeignKey(p => p.SellerId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            // Deal configuration
            modelBuilder.Entity<Deal>(entity =>
            {
                entity.HasKey(d => d.Id);
                entity.HasIndex(d => d.DealNumber).IsUnique();
                entity.Property(d => d.DealNumber).IsRequired().HasMaxLength(50);
                entity.Property(d => d.Amount).HasColumnType("decimal(18,2)");
                entity.Property(d => d.CommissionAmount).HasColumnType("decimal(18,2)");
                entity.Property(d => d.CommissionPercentage).HasColumnType("decimal(5,2)");
                entity.Property(d => d.Status).IsRequired().HasMaxLength(50);
                entity.Property(d => d.DealDate).HasDefaultValueSql("GETUTCDATE()");

                entity.HasOne(d => d.Property)
                    .WithMany(p => p.Deals)
                    .HasForeignKey(d => d.PropertyId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.Client)
                    .WithMany()
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.Seller)
                    .WithMany(s => s.Deals)
                    .HasForeignKey(d => d.SellerId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.Realtor)
                    .WithMany(r => r.Deals)
                    .HasForeignKey(d => d.RealtorId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}