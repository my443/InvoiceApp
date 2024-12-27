﻿// <auto-generated />
using System;
using InvoiceApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace InvoiceApp.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241227122425_EmployeeConnectionToUser")]
    partial class EmployeeConnectionToUser
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.0");

            modelBuilder.Entity("InvoiceApp.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("TEXT");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("TEXT");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("InvoiceApp.Models.Approval", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ApproverId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DateApproved")
                        .HasColumnType("TEXT");

                    b.Property<int?>("ExpenseId")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsApproved")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ApproverId");

                    b.HasIndex("ExpenseId");

                    b.ToTable("Approvals");
                });

            modelBuilder.Entity("InvoiceApp.Models.Department", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("DepartmentNumber")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("InvoiceApp.Models.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ApprovalLimit")
                        .HasColumnType("INTEGER");

                    b.Property<string>("EE_ID")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsProcessor")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("NotificationId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("NotificationId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("InvoiceApp.Models.Expense", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ExpenseStatusId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SubmittedById")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("TotalAmount")
                        .HasPrecision(18, 2)
                        .HasColumnType("TEXT");

                    b.Property<decimal>("TotalHst")
                        .HasPrecision(18, 2)
                        .HasColumnType("TEXT");

                    b.Property<int>("VendorId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ExpenseStatusId");

                    b.HasIndex("SubmittedById");

                    b.HasIndex("VendorId");

                    b.ToTable("Expenses");
                });

            modelBuilder.Entity("InvoiceApp.Models.ExpenseDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AccountId")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Amount")
                        .HasPrecision(18, 2)
                        .HasColumnType("TEXT");

                    b.Property<int>("DepartmentId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ExpenseId")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Hst")
                        .HasPrecision(18, 2)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("ExpenseId");

                    b.ToTable("ExpenseDetails");
                });

            modelBuilder.Entity("InvoiceApp.Models.ExpenseStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("StatusName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("ExpenseStatus");
                });

            modelBuilder.Entity("InvoiceApp.Models.FileType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Extension")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("FileTypes");
                });

            modelBuilder.Entity("InvoiceApp.Models.GLAccount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("AccountNumber")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("GLAccounts");
                });

            modelBuilder.Entity("InvoiceApp.Models.Image", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ExpenseId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("FileTypeId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("UploadTime")
                        .HasColumnType("TEXT");

                    b.Property<int>("UploadedById")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ExpenseId");

                    b.HasIndex("FileTypeId");

                    b.HasIndex("UploadedById");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("InvoiceApp.Models.Note", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ApprovalId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DateAdded")
                        .HasColumnType("TEXT");

                    b.Property<int?>("ExpenseId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("NoteText")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("WrittenById")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ApprovalId");

                    b.HasIndex("ExpenseId");

                    b.HasIndex("WrittenById");

                    b.ToTable("Notes");
                });

            modelBuilder.Entity("InvoiceApp.Models.Notification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("NotificationDateTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("NotificationMessage")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("NotificationType")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("InvoiceApp.Models.Vendor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Vendors");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Value")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("InvoiceApp.Models.ApplicationUser", b =>
                {
                    b.HasOne("InvoiceApp.Models.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("InvoiceApp.Models.Approval", b =>
                {
                    b.HasOne("InvoiceApp.Models.Employee", "Approver")
                        .WithMany()
                        .HasForeignKey("ApproverId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InvoiceApp.Models.Expense", null)
                        .WithMany("Approvals")
                        .HasForeignKey("ExpenseId");

                    b.Navigation("Approver");
                });

            modelBuilder.Entity("InvoiceApp.Models.Employee", b =>
                {
                    b.HasOne("InvoiceApp.Models.Notification", null)
                        .WithMany("Reciptients")
                        .HasForeignKey("NotificationId");
                });

            modelBuilder.Entity("InvoiceApp.Models.Expense", b =>
                {
                    b.HasOne("InvoiceApp.Models.ExpenseStatus", "ExpenseStatus")
                        .WithMany()
                        .HasForeignKey("ExpenseStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InvoiceApp.Models.Employee", "SubmittedBy")
                        .WithMany()
                        .HasForeignKey("SubmittedById")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InvoiceApp.Models.Vendor", "Vendor")
                        .WithMany()
                        .HasForeignKey("VendorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ExpenseStatus");

                    b.Navigation("SubmittedBy");

                    b.Navigation("Vendor");
                });

            modelBuilder.Entity("InvoiceApp.Models.ExpenseDetail", b =>
                {
                    b.HasOne("InvoiceApp.Models.GLAccount", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InvoiceApp.Models.Department", "Department")
                        .WithMany()
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InvoiceApp.Models.Expense", null)
                        .WithMany("ExpenseDetails")
                        .HasForeignKey("ExpenseId");

                    b.Navigation("Account");

                    b.Navigation("Department");
                });

            modelBuilder.Entity("InvoiceApp.Models.Image", b =>
                {
                    b.HasOne("InvoiceApp.Models.Expense", null)
                        .WithMany("Images")
                        .HasForeignKey("ExpenseId");

                    b.HasOne("InvoiceApp.Models.FileType", "FileType")
                        .WithMany()
                        .HasForeignKey("FileTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InvoiceApp.Models.Employee", "UploadedBy")
                        .WithMany()
                        .HasForeignKey("UploadedById")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FileType");

                    b.Navigation("UploadedBy");
                });

            modelBuilder.Entity("InvoiceApp.Models.Note", b =>
                {
                    b.HasOne("InvoiceApp.Models.Approval", null)
                        .WithMany("ApprovalComments")
                        .HasForeignKey("ApprovalId");

                    b.HasOne("InvoiceApp.Models.Expense", null)
                        .WithMany("Notes")
                        .HasForeignKey("ExpenseId");

                    b.HasOne("InvoiceApp.Models.Employee", "WrittenBy")
                        .WithMany()
                        .HasForeignKey("WrittenById")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("WrittenBy");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("InvoiceApp.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("InvoiceApp.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InvoiceApp.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("InvoiceApp.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("InvoiceApp.Models.Approval", b =>
                {
                    b.Navigation("ApprovalComments");
                });

            modelBuilder.Entity("InvoiceApp.Models.Expense", b =>
                {
                    b.Navigation("Approvals");

                    b.Navigation("ExpenseDetails");

                    b.Navigation("Images");

                    b.Navigation("Notes");
                });

            modelBuilder.Entity("InvoiceApp.Models.Notification", b =>
                {
                    b.Navigation("Reciptients");
                });
#pragma warning restore 612, 618
        }
    }
}
