using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerAndMatterData.Models
{
    public class Customer
    {
        public long Id { get; set; }
        public required string Name { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set;  }

        //ForeignKey to Lawyer
        public long LawyerId { get; set; }

        public List<Matter>? Matters { get; set;  }

    }
}
