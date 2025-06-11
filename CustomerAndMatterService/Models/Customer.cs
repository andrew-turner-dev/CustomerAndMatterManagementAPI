using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerAndMatterService.Models
{
    public class Customer
    {
        public long Id { get; set; }
        public required string Name { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
    }
}
