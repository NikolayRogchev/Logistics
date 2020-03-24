using System;
using System.Collections.Generic;
using System.Text;

namespace Logistics.Models
{
    public class Path
    {
        public int Id { get; set; }

        public double Length { get; set; }

        public City From { get; set; }
        public int FromId { get; set; }

        public City To { get; set; }
        public int ToId { get; set; }
    }
}
