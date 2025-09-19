using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queries.Models
{
     class SalesResult
    {
        public string CustomerName { get; set; } = string.Empty;

        public string ManagerName { get; set; } = string.Empty; 

        public decimal TotalAmount { get; set; }
    }
}
