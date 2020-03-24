using Logistics.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logistics.Services.Interfaces
{
    public interface IService
    {
        public LogisticsDbContext Db { get; }
    }
}
