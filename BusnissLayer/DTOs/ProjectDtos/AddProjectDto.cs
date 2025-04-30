using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusnissLayer.DTOs.ProjectDtos
{
    public class AddProjectDto
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
    }
}
