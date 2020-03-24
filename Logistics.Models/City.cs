using System.Collections.Generic;

namespace Logistics.Models
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float? Latitude { get; set; }
        public float? Longitude { get; set; }
    }
}
