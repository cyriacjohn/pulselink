using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BDMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using BDMS.Application.Interfaces;

namespace BDMS.Infrastructure.Data
{
    public class BDMSDbContext : DbContext
    {
        public BDMSDbContext(DbContextOptions<BDMSDbContext> options) : base(options) { }
        public DbSet<Donor> Donors => Set<Donor>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Hospital> Hospitals => Set<Hospital>();
        public DbSet<Donation> Donations => Set<Donation>();
        public DbSet<BloodInventory> BloodInventory => Set<BloodInventory>();
        public DbSet<Notification> Notifications => Set<Notification>();
        public DbSet<BloodRequest> BloodRequests => Set<BloodRequest>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Donor>(entity
                =>
            {
                entity.HasKey(d => d.Id);
                entity.Property(d => d.Name)
                .IsRequired()
                .HasMaxLength(150);
                entity.Property(d => d.Email)
                .IsRequired()
                .HasMaxLength(150);
                entity.Property(d => d.Phone)
                .IsRequired()
                .HasMaxLength(20); ;
                entity.Property(d => d.BloodGroup)
                .IsRequired()
                .HasMaxLength(5);
            }
                );

            modelBuilder.Entity<Donation>()
                .HasOne(d => d.Donor)
                .WithMany()
                .HasForeignKey(d => d.DonorId)
                .OnDelete(DeleteBehavior.Restrict);
                
             modelBuilder.Entity<Donation>()
                  .HasOne(d => d.Hospital)
                  .WithMany()
                  .HasForeignKey(d => d.HospitalId)
                  .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<BloodRequest>()
                .HasOne(br => br.Hospital)
                .WithMany()
                .HasForeignKey(br => br.HospitalId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<BloodInventory>()
                .HasIndex(i => new { i.HospitalId, i.BloodGroup }).IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email).IsUnique();
            modelBuilder.Entity<User>()
                .HasIndex(u => u.UserName).IsUnique();

        }
    }
}
