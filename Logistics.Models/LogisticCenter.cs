using System;
using System.Collections.Generic;
using System.Text;

namespace Logistics.Models
{
    public class LogisticCenter
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public City City { get; set; }
        public int CityId { get; set; }
    }
}
