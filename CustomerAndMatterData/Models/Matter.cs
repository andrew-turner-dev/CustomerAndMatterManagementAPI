﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerAndMatterData.Models
{
    public class Matter
    {
        public long Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required Boolean IsClosed { get; set; }


        //Foreign keys
        public long CustomerId { get; set; }
        public long LawyerId { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Lawyer Lawyer { get; set; }
    }
}
