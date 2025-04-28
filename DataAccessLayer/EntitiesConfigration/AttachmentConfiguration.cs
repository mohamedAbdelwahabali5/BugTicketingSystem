using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace DataAccessLayer.EntitiesConfigration
{
    internal class AttachmentConfiguration : IEntityTypeConfiguration<Attachment>
    {
        public void Configure(EntityTypeBuilder<Attachment> builder)
        {
            // Primary Key
            builder.HasKey(x => x.Id);

            // Property configurations
            builder.Property(x => x.FileName)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false);

            builder.Property(x => x.FileType)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(x => x.ContentType)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(x => x.FilePath)
                .IsRequired()
                .HasMaxLength(500);
    

            builder.Property(x => x.CreatedAt)
                .IsRequired()
                .HasColumnType("datetime2")
                .HasDefaultValueSql("GETUTCDATE()");

            // Relationships
            builder.HasOne(x => x.Bug)
                .WithMany(x => x.Attachments)
                .HasForeignKey(x => x.BugId)
                .OnDelete(DeleteBehavior.Cascade);  
        }
    }
    
    
}
