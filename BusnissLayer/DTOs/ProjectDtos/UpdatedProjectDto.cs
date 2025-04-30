using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusnissLayer.DTOs.ProjectDtos
{
    public class UpdatedProjectDto
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public int? ManagerId { get; set; }
    }
}
