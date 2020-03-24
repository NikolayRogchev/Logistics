using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logistics.App.Models
{
    public class UpdatePathModel
    {
        public int Id { get; set; }
        public int FromId { get; set; }
        public int ToId { get; set; }
        public double Length { get; set; }
    }
}
