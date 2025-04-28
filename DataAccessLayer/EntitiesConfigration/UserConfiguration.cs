using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntitiesConfigration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.UserName)
                .IsRequired()
                .HasColumnName("UserName")
                .HasMaxLength(100);
            builder.Property(x => x.Password)
                .IsRequired()
                .HasColumnName("Password")
                .HasMaxLength(255);
            builder.Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(255);
               
            builder.Property(x => x.CreatedAt).IsRequired().HasColumnName("CreatedAt").HasColumnType("datetime2");

            // Configure the one-to-many relationship with Project
            builder.HasOne(x => x.Project)
                .WithMany(x => x.Users)
                .HasForeignKey(x => x.ProjectId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure the many-to-many relationship with Users_Bug
            builder.HasMany(x => x.User_Bugs)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
