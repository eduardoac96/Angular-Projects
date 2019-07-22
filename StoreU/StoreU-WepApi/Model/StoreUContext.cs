using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using StoreU_WepApi.Model.Entities;

namespace StoreU_WebApi.Model
{
    public partial class StoreUContext : DbContext
    {
        public StoreUContext()
        {
        }

        public StoreUContext(DbContextOptions<StoreUContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Banks> Banks { get; set; }
        public virtual DbSet<CategoryCompany> CategoryCompany { get; set; }
        public virtual DbSet<CategoryProducts> CategoryProducts { get; set; }
        public virtual DbSet<Company> Company { get; set; }
        public virtual DbSet<CompanyProducts> CompanyProducts { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<SubscriptionPlans> SubscriptionPlans { get; set; }
        public virtual DbSet<UserAddress> UserAddress { get; set; }
        public virtual DbSet<UserBank> UserBank { get; set; }
        public virtual DbSet<UserCompany> UserCompany { get; set; }
        public virtual DbSet<UserPlan> UserPlan { get; set; }


        public virtual DbSet<UserProfile> UserProfile { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        public virtual DbSet<UserVerificationCode> UserVerificationCode { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=StoreU;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Users>()
              .HasOne(p => p.UserProfile)
              .WithOne(i => i.Users)
              .HasForeignKey<UserProfile>(b => b.UserId);


            modelBuilder.Entity<CompanyProducts>()
                .HasKey(table => new
                {
                    table.CompanyId,
                    table.ProductId
                });

            modelBuilder.Entity<UserAddress>()
                .HasKey(table => new
                {
                    table.UserId,
                    table.AddressId
                });

            modelBuilder.Entity<UserBank>()
                .HasKey(table => new
                {
                    table.UserId,
                    table.BankId
                });


            modelBuilder.Entity<UserCompany>()
                .HasKey(table => new
                {
                    table.UserId,
                    table.CompanyId
                });


            modelBuilder.Entity<UserPlan>()
                .HasKey(table => new
                {
                    table.UserId,
                    table.PlanId
                });



             


        }
    }
}
