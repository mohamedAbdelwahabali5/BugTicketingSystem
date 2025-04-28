using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore;
namespace DataAccessLayer.EntitiesConfigration
{
    public class User_BugConfiguration : IEntityTypeConfiguration<User_Bug>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<User_Bug> builder)
        {
            builder.HasKey(x => new { x.UserId, x.BugId });
           
        }
    }
}
