using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Proxies;
namespace DataAccessLayer.Context
{
    public class BTSDbContext : DbContext
    {
        public BTSDbContext(DbContextOptions<BTSDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ConfigureWarnings(warnings =>
                warnings.Ignore(RelationalEventId.PendingModelChangesWarning));

            //optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BTSDbContext).Assembly);
            SeedStaticData(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Bug> Bugs { get; set; }
        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<User_Bug> User_Bugs { get; set; }
        public DbSet<Role> Roles { get; set; }

        private void SeedStaticData(ModelBuilder modelBuilder)
        {
            // جميع القيم الثابتة هنا
            const string fixedDate = "2023-01-01T00:00:00.000Z";
            const string managerPassword = "manager123";
            const string dev1Password = "dev1123";
            const string dev2Password = "dev2123";
            const string testerPassword = "tester123";

            // Seed Roles
            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "Manager" },
                new Role { Id = 2, Name = "Developer" },
                new Role { Id = 3, Name = "Tester" }
            );

            // Seed Users
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    UserName = "manager",
                    Email = "manager@bugtracker.com",
                    Password = BCrypt.Net.BCrypt.HashPassword(managerPassword),
                    CreatedAt = DateTime.Parse(fixedDate)
                },
                new User
                {
                    Id = 2,
                    UserName = "dev1",
                    Email = "dev1@bugtracker.com",
                    Password = BCrypt.Net.BCrypt.HashPassword(dev1Password),
                    CreatedAt = DateTime.Parse(fixedDate),
                    ProjectId = 1
                },
                new User
                {
                    Id = 3,
                    UserName = "dev2",
                    Email = "dev2@bugtracker.com",
                    Password = BCrypt.Net.BCrypt.HashPassword(dev2Password),
                    CreatedAt = DateTime.Parse(fixedDate),
                    ProjectId = 2
                },
                new User
                {
                    Id = 4,
                    UserName = "tester1",
                    Email = "tester1@bugtracker.com",
                    Password = BCrypt.Net.BCrypt.HashPassword(testerPassword),
                    CreatedAt = DateTime.Parse(fixedDate),
                    ProjectId = 1
                }
            );

            // Seed User-Role relationships
            modelBuilder.Entity<User>()
                .HasMany(u => u.Roles)
                .WithMany(r => r.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "Users_Roles",
                    r => r.HasOne<Role>().WithMany().HasForeignKey("RoleId"),
                    l => l.HasOne<User>().WithMany().HasForeignKey("UserId"),
                    je =>
                    {
                        je.HasKey("UserId", "RoleId");
                        je.HasData(
                            new { UserId = 1, RoleId = 1 },
                            new { UserId = 2, RoleId = 2 },
                            new { UserId = 3, RoleId = 2 },
                            new { UserId = 4, RoleId = 3 }
                        );
                    });

            // Seed Projects
            modelBuilder.Entity<Project>().HasData(
                new Project
                {
                    Id = 1,
                    Name = "Website Redesign",
                    Description = "Complete overhaul of company website",
                    ManagerId = 1,
                    CreatedAt = DateTime.Parse(fixedDate)
                },
                new Project
                {
                    Id = 2,
                    Name = "Mobile App",
                    Description = "New cross-platform mobile application",
                    ManagerId = 1,
                    CreatedAt = DateTime.Parse(fixedDate)
                }
            );

            // Seed Bugs
            modelBuilder.Entity<Bug>().HasData(
                new Bug
                {
                    Id = 1,
                    Title = "Login page not responsive",
                    Description = "Login form breaks on mobile devices",
                    Status = BugStatus.Open,
                    Priority = BugPriority.High,
                    ProjectId = 1,
                    CreatedAt = DateTime.Parse(fixedDate)
                },
                new Bug
                {
                    Id = 2,
                    Title = "Dashboard loading slow",
                    Description = "Dashboard takes more than 5 seconds to load",
                    Status = BugStatus.InProgress,
                    Priority = BugPriority.Medium,
                    ProjectId = 1,
                    CreatedAt = DateTime.Parse(fixedDate)
                },
                new Bug
                {
                    Id = 3,
                    Title = "Push notifications not working",
                    Description = "Users not receiving push notifications on iOS",
                    Status = BugStatus.Open,
                    Priority = BugPriority.Critical,
                    ProjectId = 2,
                    CreatedAt = DateTime.Parse(fixedDate)
                }
            );

            // Seed User-Bug assignments
            modelBuilder.Entity<User_Bug>().HasData(
                new User_Bug { UserId = 2, BugId = 1 },
                new User_Bug { UserId = 2, BugId = 2 },
                new User_Bug { UserId = 3, BugId = 3 }
            );

            // Seed Attachments
            modelBuilder.Entity<Attachment>().HasData(
                new Attachment
                {
                    Id = 1,
                    FileName = "screenshot.png",
                    FileType = "image/png",
                    ContentType = "png",
                    FilePath = "/uploads/screenshot123.png",
                    BugId = 1,
                    CreatedAt = DateTime.Parse(fixedDate)
                }
            );
        }
    }
}