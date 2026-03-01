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

            modelBuilder.Entity<BloodInventory>()
                .HasIndex(i => new { i.HospitalId, i.BloodGroup }).IsUnique();

            modelBuilder.Entity<Hospital>().HasData(
    new Hospital { Id = 1, Name = "Aster Medcity", City = "Kochi", Address = "Cheranallur, Kochi", ContactPhone = "0484-6699999" },
    new Hospital { Id = 2, Name = "Rajagiri Hospital", City = "Aluva", Address = "Rajagiri Valley Rd, Aluva", ContactPhone = "0484-2700600" },
    new Hospital { Id = 3, Name = "Amrita Institute of Medical Sciences", City = "Kochi", Address = "Ponekkara, Kochi", ContactPhone = "0484-2802000" },
    new Hospital { Id = 4, Name = "Lisie Hospital", City = "Kochi", Address = "Pettah, Kochi", ContactPhone = "0484-2662222" },
    new Hospital { Id = 5, Name = "Medical Trust Hospital", City = "Kochi", Address = "MG Road, Kochi", ContactPhone = "0484-2361400" }
);


            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email).IsUnique();
            modelBuilder.Entity<User>()
                .HasIndex(u => u.UserName).IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}
