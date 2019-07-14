using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace StoreU_WepApi.Model
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

        public virtual DbSet<BankId> BankId { get; set; }
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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=StoreU;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<BankId>(entity =>
            {
                entity.HasKey(e => e.BankId1);

                entity.ToTable("BankID");

                entity.Property(e => e.BankId1)
                    .HasColumnName("BankID")
                    .ValueGeneratedNever();

                entity.Property(e => e.BankName)
                    .HasMaxLength(80)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CategoryCompany>(entity =>
            {
                entity.HasKey(e => e.CategoryId);

                entity.Property(e => e.CategoryId)
                    .HasColumnName("CategoryID")
                    .ValueGeneratedNever();

                entity.Property(e => e.CategoryName)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CategoryProducts>(entity =>
            {
                entity.HasKey(e => new { e.CompanyId, e.CategoryProductId });

                entity.Property(e => e.CompanyId).HasColumnName("CompanyID");

                entity.Property(e => e.CategoryProductId).HasColumnName("CategoryProductID");

                entity.Property(e => e.CategoryName)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.CategoryProducts)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CategoryProducts_Company");
            });

            modelBuilder.Entity<Company>(entity =>
            {
                entity.Property(e => e.CompanyId)
                    .HasColumnName("CompanyID")
                    .ValueGeneratedNever();

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.CompanyBirthDate).HasColumnType("date");

                entity.Property(e => e.CompanyName)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.CompanySummary)
                    .HasMaxLength(400)
                    .IsUnicode(false);

                entity.Property(e => e.CompanyWebPage)
                    .HasMaxLength(120)
                    .IsUnicode(false);

                entity.Property(e => e.MainAddress)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.RegistryDate).HasColumnType("datetime");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Company)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_Company_CategoryCompany");
            });

            modelBuilder.Entity<CompanyProducts>(entity =>
            {
                entity.HasKey(e => new { e.CompanyId, e.ProductId });

                entity.Property(e => e.CompanyId).HasColumnName("CompanyID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.CategoryProductId).HasColumnName("CategoryProductID");

                entity.Property(e => e.ProductName)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.RegistryDate).HasColumnType("datetime");

                entity.Property(e => e.UnitPrice).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.CompanyProducts)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CompanyProducts_Company");
            });

            modelBuilder.Entity<Roles>(entity =>
            {
                entity.HasKey(e => e.RoleId);

                entity.Property(e => e.RoleId)
                    .HasColumnName("RoleID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Description)
                    .HasMaxLength(120)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SubscriptionPlans>(entity =>
            {
                entity.HasKey(e => e.PlanId);

                entity.Property(e => e.PlanId)
                    .HasColumnName("PlanID")
                    .ValueGeneratedNever();

                entity.Property(e => e.MonthlyCharge).HasColumnType("money");

                entity.Property(e => e.PlanDescription)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.PlanName)
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UserAddress>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.AddressId });

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.AddressId).HasColumnName("AddressID");

                entity.Property(e => e.Address)
                    .HasMaxLength(400)
                    .IsUnicode(false);

                entity.Property(e => e.City)
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.Country)
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.State)
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserAddress)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserAddress_Users");
            });

            modelBuilder.Entity<UserBank>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.BankId });

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.BankId).HasColumnName("BankID");

                entity.Property(e => e.CardExpiration)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.CardNumber)
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.HasOne(d => d.Bank)
                    .WithMany(p => p.UserBank)
                    .HasForeignKey(d => d.BankId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserBank_BankID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserBank)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserBank_Users");
            });

            modelBuilder.Entity<UserCompany>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.CompanyId });

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.CompanyId).HasColumnName("CompanyID");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.UserCompany)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserCompany_Company");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserCompany)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserCompany_Users");
            });

            modelBuilder.Entity<UserPlan>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.PlanId });

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.PlanId).HasColumnName("PlanID");

                entity.Property(e => e.PayDate).HasColumnType("datetime");

                entity.HasOne(d => d.Plan)
                    .WithMany(p => p.UserPlan)
                    .HasForeignKey(d => d.PlanId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserPlan_SubscriptionPlans");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserPlan)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserPlan_Users");
            });

            modelBuilder.Entity<UserProfile>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.Property(e => e.UserId)
                    .HasColumnName("UserID")
                    .ValueGeneratedNever();

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.ProfileSummary)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.ProfileTitle)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Rfc)
                    .HasColumnName("RFC")
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__Users__1788CCACE346B048");

                entity.HasIndex(e => e.UserName)
                    .HasName("UQ__Users__C9F2845689BBAB72")
                    .IsUnique();

                entity.Property(e => e.UserId)
                    .HasColumnName("UserID")
                    .ValueGeneratedNever();

                entity.Property(e => e.BirthDate).HasColumnType("datetime");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(120)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(120)
                    .IsUnicode(false);

                entity.Property(e => e.RegistryDate).HasColumnType("datetime");

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.SecondLastName)
                    .HasMaxLength(120)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_Users_Roles");

                entity.HasOne(d => d.User)
                    .WithOne(p => p.Users)
                    .HasForeignKey<Users>(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Users_UserProfile");
            });
        }
    }
}
