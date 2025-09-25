using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Entities
{
    public class Expence
    {
        public int Id { get; set; }
        public double Sum { get; set; }

        public string? Description { get; set; }
    }
}
