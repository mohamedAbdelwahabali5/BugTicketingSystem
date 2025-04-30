using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BusnissLayer.DTOs.BugDtos
{
    public class UpdateBugDto
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public BugStatus Status { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public BugPriority Priority { get; set; }
        public int? ProjectId { get; set; }
    }
}
