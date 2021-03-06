﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StoreU_WebApi.Model;

namespace StoreU_WepApi.Migrations
{
    [DbContext(typeof(StoreUContext))]
    [Migration("20190802145620_UpdatedCompanyTable")]
    partial class UpdatedCompanyTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("StoreU_WebApi.Model.Banks", b =>
                {
                    b.Property<Guid>("BankId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BankName");

                    b.HasKey("BankId");

                    b.ToTable("Banks");
                });

            modelBuilder.Entity("StoreU_WebApi.Model.CategoryCompany", b =>
                {
                    b.Property<Guid>("CategoryId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CategoryName");

                    b.Property<Guid?>("CompanyId");

                    b.HasKey("CategoryId");

                    b.HasIndex("CompanyId");

                    b.ToTable("CategoryCompany");
                });

            modelBuilder.Entity("StoreU_WebApi.Model.CategoryProducts", b =>
                {
                    b.Property<Guid>("CategoryProductId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CategoryName");

                    b.Property<Guid>("CompanyId");

                    b.HasKey("CategoryProductId");

                    b.HasIndex("CompanyId");

                    b.ToTable("CategoryProducts");
                });

            modelBuilder.Entity("StoreU_WebApi.Model.Company", b =>
                {
                    b.Property<Guid>("CompanyId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("CategoryId");

                    b.Property<string>("City");

                    b.Property<DateTime?>("CompanyBirthDate");

                    b.Property<byte[]>("CompanyLogo");

                    b.Property<string>("CompanyName");

                    b.Property<string>("CompanySummary");

                    b.Property<string>("CompanyWebPage");

                    b.Property<string>("Country");

                    b.Property<int?>("EmployeesQty");

                    b.Property<string>("MainAddress");

                    b.Property<string>("PhoneNumber");

                    b.Property<string>("RFC");

                    b.Property<DateTime?>("RegistryDate");

                    b.HasKey("CompanyId");

                    b.ToTable("Company");
                });

            modelBuilder.Entity("StoreU_WebApi.Model.CompanyProducts", b =>
                {
                    b.Property<Guid>("CompanyId");

                    b.Property<Guid>("ProductId");

                    b.Property<Guid?>("CategoryProductId");

                    b.Property<int?>("MinimumShipment");

                    b.Property<string>("ProductName");

                    b.Property<DateTime?>("RegistryDate");

                    b.Property<int?>("Stock");

                    b.Property<decimal?>("UnitPrice");

                    b.HasKey("CompanyId", "ProductId");

                    b.ToTable("CompanyProducts");
                });

            modelBuilder.Entity("StoreU_WebApi.Model.Roles", b =>
                {
                    b.Property<int>("RoleId");

                    b.Property<string>("Description");

                    b.HasKey("RoleId");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("StoreU_WebApi.Model.SubscriptionPlans", b =>
                {
                    b.Property<int>("PlanId");

                    b.Property<decimal?>("MonthlyCharge");

                    b.Property<string>("PlanDescription");

                    b.Property<string>("PlanName");

                    b.HasKey("PlanId");

                    b.ToTable("SubscriptionPlans");
                });

            modelBuilder.Entity("StoreU_WebApi.Model.UserAddress", b =>
                {
                    b.Property<Guid>("UserId");

                    b.Property<Guid>("AddressId");

                    b.Property<string>("Address");

                    b.Property<string>("City");

                    b.Property<string>("Country");

                    b.Property<string>("State");

                    b.HasKey("UserId", "AddressId");

                    b.ToTable("UserAddress");
                });

            modelBuilder.Entity("StoreU_WebApi.Model.UserBank", b =>
                {
                    b.Property<Guid>("UserId");

                    b.Property<Guid>("BankId");

                    b.Property<string>("CardExpiration");

                    b.Property<string>("CardNumber");

                    b.HasKey("UserId", "BankId");

                    b.HasIndex("BankId");

                    b.ToTable("UserBank");
                });

            modelBuilder.Entity("StoreU_WebApi.Model.UserCompany", b =>
                {
                    b.Property<Guid>("UserId");

                    b.Property<Guid>("CompanyId");

                    b.HasKey("UserId", "CompanyId");

                    b.HasIndex("CompanyId");

                    b.ToTable("UserCompany");
                });

            modelBuilder.Entity("StoreU_WebApi.Model.UserPlan", b =>
                {
                    b.Property<Guid>("UserId");

                    b.Property<int>("PlanId");

                    b.Property<DateTime?>("PayDate");

                    b.Property<bool?>("Payed");

                    b.HasKey("UserId", "PlanId");

                    b.HasIndex("PlanId");

                    b.ToTable("UserPlan");
                });

            modelBuilder.Entity("StoreU_WebApi.Model.UserProfile", b =>
                {
                    b.Property<Guid>("UserId");

                    b.Property<string>("PhoneNumber");

                    b.Property<byte[]>("ProfilePhoto");

                    b.Property<string>("ProfileSummary");

                    b.Property<string>("ProfileTitle");

                    b.Property<string>("Rfc");

                    b.HasKey("UserId");

                    b.ToTable("UserProfile");
                });

            modelBuilder.Entity("StoreU_WebApi.Model.Users", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("BirthDate");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<byte[]>("Password");

                    b.Property<byte[]>("PasswordHash");

                    b.Property<string>("PersonalPhoneNumber");

                    b.Property<DateTime?>("RegistryDate");

                    b.Property<int?>("RoleId");

                    b.Property<string>("SecondLastName");

                    b.Property<string>("UserName");

                    b.HasKey("UserId");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("StoreU_WepApi.Model.Entities.UserVerificationCode", b =>
                {
                    b.Property<Guid>("VerificationId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("ExpirationTime");

                    b.Property<bool>("IsUsed");

                    b.Property<DateTime>("RegistryDate");

                    b.Property<int>("SecurityCode");

                    b.Property<Guid>("UserId");

                    b.HasKey("VerificationId");

                    b.HasIndex("UserId");

                    b.ToTable("UserVerificationCode");
                });

            modelBuilder.Entity("StoreU_WebApi.Model.CategoryCompany", b =>
                {
                    b.HasOne("StoreU_WebApi.Model.Company", "Company")
                        .WithMany("CategoryCompany")
                        .HasForeignKey("CompanyId");
                });

            modelBuilder.Entity("StoreU_WebApi.Model.CategoryProducts", b =>
                {
                    b.HasOne("StoreU_WebApi.Model.Company", "Company")
                        .WithMany("CategoryProducts")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("StoreU_WebApi.Model.CompanyProducts", b =>
                {
                    b.HasOne("StoreU_WebApi.Model.Company", "Company")
                        .WithMany("CompanyProducts")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("StoreU_WebApi.Model.UserAddress", b =>
                {
                    b.HasOne("StoreU_WebApi.Model.Users", "User")
                        .WithMany("UserAddress")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("StoreU_WebApi.Model.UserBank", b =>
                {
                    b.HasOne("StoreU_WebApi.Model.Banks", "Bank")
                        .WithMany("UserBank")
                        .HasForeignKey("BankId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("StoreU_WebApi.Model.Users", "User")
                        .WithMany("UserBank")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("StoreU_WebApi.Model.UserCompany", b =>
                {
                    b.HasOne("StoreU_WebApi.Model.Company", "Company")
                        .WithMany("UserCompany")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("StoreU_WebApi.Model.Users", "User")
                        .WithMany("UserCompany")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("StoreU_WebApi.Model.UserPlan", b =>
                {
                    b.HasOne("StoreU_WebApi.Model.SubscriptionPlans", "Plan")
                        .WithMany("UserPlan")
                        .HasForeignKey("PlanId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("StoreU_WebApi.Model.Users", "User")
                        .WithMany("UserPlan")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("StoreU_WebApi.Model.UserProfile", b =>
                {
                    b.HasOne("StoreU_WebApi.Model.Users", "Users")
                        .WithOne("UserProfile")
                        .HasForeignKey("StoreU_WebApi.Model.UserProfile", "UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("StoreU_WebApi.Model.Users", b =>
                {
                    b.HasOne("StoreU_WebApi.Model.Roles", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId");
                });

            modelBuilder.Entity("StoreU_WepApi.Model.Entities.UserVerificationCode", b =>
                {
                    b.HasOne("StoreU_WebApi.Model.Users", "Users")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
