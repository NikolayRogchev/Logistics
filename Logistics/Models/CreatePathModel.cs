using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logistics.App.Models
{
    public class CreatePathModel
    {
        public int FromCityId { get; set; }
        public int ToCityId { get; set; }
        public double SelectedPathLength { get; set; }
    }
}
