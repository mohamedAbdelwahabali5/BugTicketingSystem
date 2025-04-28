using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class User_Bug
    {
        public int UserId { get; set; }
        public int BugId { get; set; }

        // relations with entities
        public  User? User { get; set; }
        public  Bug? Bug { get; set; }
    }
}
