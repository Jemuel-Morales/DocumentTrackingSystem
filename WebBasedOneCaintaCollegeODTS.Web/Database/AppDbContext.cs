﻿using DocumentTrackingSystem.Web.Entities;
using DocumentTrackingSystem.Web.Entities.Document;
using DocumentTrackingSystem.Web.Entities.TrackingStatus;
using DocumentTrackingSystem.Web.Entities.User;
using Microsoft.EntityFrameworkCore;

namespace DocumentTrackingSystem.Web.Database
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<EType> Types { get; set; }
        public DbSet<EStatus> Status { get; set; }
        public DbSet<EDocument> Documents { get; set; }
        public DbSet<ETrackingStatus> TrackingStatus { get; set; }
        public DbSet<EUser> Users { get; set; }
        public DbSet<EPeople> People { get; set; }
        public DbSet<ERole> Roles { get; set; }
        public DbSet<EStudent> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<EDocument>(e =>
            {
                e.HasOne(e => e.Student).WithMany(e => e.Documents).HasForeignKey(fk => fk.StudentId);
                e.HasOne(e => e.Type).WithMany().HasForeignKey(fk => fk.TypeId);

            });

            builder.Entity<ETrackingStatus>(e =>
            {
                e.HasOne(e => e.Document).WithMany(e => e.TrackingStatus).HasForeignKey(fk => fk.DocumentId);
                e.HasOne(e => e.Status).WithMany().HasForeignKey(fk => fk.StatusId);
            });

            builder.Entity<EUser>(e =>
            {
                e.HasOne(e => e.Role).WithMany().HasForeignKey(e => e.RoleId);
            });

            builder.Entity<EPeople>(e =>
            {
                e.HasOne(e => e.User).WithOne(e => e.People).HasForeignKey<EPeople>(e => e.UserId).IsRequired();
            });


            builder.Entity<EStatus>().HasData(
                new EStatus() { Id = 1, StatusName = "Draft" },
                new EStatus() { Id = 2, StatusName = "Under Review" },
                new EStatus() { Id = 3, StatusName = "Pending Approval" },
                new EStatus() { Id = 4, StatusName = "Approved" },
                new EStatus() { Id = 5, StatusName = "Rejected" },
                new EStatus() { Id = 6, StatusName = "On Hold" },
                new EStatus() { Id = 7, StatusName = "In Progress" },
                new EStatus() { Id = 8, StatusName = "Completed" }
                );

            builder.Entity<EType>().HasData(
                new EType() { Id = 1, TypeName = "Letter" },
                new EType() { Id = 2, TypeName = "Form" },
                new EType() { Id = 3, TypeName = "Report" },
                new EType() { Id = 4, TypeName = "Compliance" },
                new EType() { Id = 5, TypeName = "Support" }
                );

            builder.Entity<EStudent>().HasData(
                new EStudent()
                {
                    Id = 1,
                    StudentNumber = "OCC202012020001",
                    LastName = "Doe",
                    FirstName = "John",
                    MiddleName = "Smith",
                    Gender = "Male",
                    Course = "BSCS",
                    DateOfBirth = new DateTime(1999, 12, 12),
                    Semester = 2,
                    YearLevel = 2
                },
                new EStudent()
                {
                    Id = 2,
                    StudentNumber = "OCC202012020002",
                    LastName = "Mantis",
                    FirstName = "Jane",
                    MiddleName = "Yor",
                    Gender = "Female",
                    Course = "BSIT",
                    DateOfBirth = new DateTime(1999, 12, 12),
                    Semester = 2,
                    YearLevel = 2
                },
                new EStudent()
                {
                    Id = 3,
                    StudentNumber = "OCC202012020003",
                    LastName = "Bernal",
                    FirstName = "Kate",
                    MiddleName = "Bernal",
                    Gender = "Female",
                    Course = "BSIT",
                    DateOfBirth = new DateTime(2000, 04, 12),
                    Semester = 2,
                    YearLevel = 4
                });

            builder.Entity<EDocument>().HasData(
                new EDocument
                {
                    Id = 1,
                    Subject = "Summary of Grades",
                    Description = "requesting for grades",
                    StudentId = 1,
                    TypeId = 1,
                }
                );

            builder.Entity<ETrackingStatus>().HasData(
                new ETrackingStatus
                {
                    Id = 1,
                    Comments = "this is autogenerated",
                    ModifiedBy = "computer",
                    StatusId = 3,
                    DocumentId = 1
                },
                new ETrackingStatus
                {
                    Id = 2,
                    Comments = "checking empty grades",
                    ModifiedBy = "Ms. Angela Reyes",
                    StatusId = 2,
                    DocumentId = 1
                }
                );

            builder.Entity<ERole>().HasData(
                new ERole { Id = 1, RoleName = "Administrator" },
                new ERole { Id = 2, RoleName = "Registrar" }
                );

            builder.Entity<EUser>().HasData(
                new EUser
                {
                    Id = 1,
                    Username = "Abraham24",
                    Password = "Abraham24",
                    Email = "Abraham24@occ-registrar.edu.ph",
                    RoleId = 2
                },
                new EUser
                {
                    Id = 2,
                    Username = "Atkinson6",
                    Password = "Atkinson6",
                    Email = "Atkinson6@occ-registrar.edu.ph",
                    RoleId = 2
                },
                new EUser
                {
                    Id = 3,
                    Username = "CelineV10",
                    Password = "CelineV10",
                    Email = "CelineV10@occ-registrar.edu.ph",
                    RoleId = 2
                }
                );

            builder.Entity<EPeople>().HasData(
                new EPeople
                {
                    Id = 1,
                    UserId = 1,
                    LastName = "De Leon",
                    MiddleName = "Tapel",
                    FirstName = "Abraham",
                    Gender = "Male",
                    DateOfBirth = new DateTime(1993, 04, 26)
                },
                new EPeople
                {
                    Id = 2,
                    UserId = 2,
                    LastName = "Atkinson",
                    MiddleName = "Cy",
                    FirstName = "Jessica",
                    Gender = "Female",
                    DateOfBirth = new DateTime(1993, 04, 26)
                },
                new EPeople
                {
                    Id = 3,
                    UserId = 3,
                    LastName = "Vargas",
                    MiddleName = "Bernal",
                    FirstName = "Celine",
                    Gender = "Female",
                    DateOfBirth = new DateTime(1993, 04, 26)
                }
                );
        }
    }
}
