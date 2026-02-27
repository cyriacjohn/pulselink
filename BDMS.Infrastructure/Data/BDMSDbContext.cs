using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BDMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BDMS.Infrastructure.Data
{
    public class BDMSDbContext : DbContext
    {
        public BDMSDbContext(DbContextOptions<BDMSDbContext> options) : base(options) { }
        public DbSet<Donor> Donors => Set<Donor>();
        public DbSet<User> Users => Set<User>();
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
        }
    }
}
