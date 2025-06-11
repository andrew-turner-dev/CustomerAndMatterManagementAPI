using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerAndMatterService.Models
{
    public class Lawyer
    {
        public long Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public string Firm { get; set; }
        public required string LoginEmail { get; set; }
        public required string Password { get; set; }
        public Boolean IsAdmin { get; set; }
    }
}
