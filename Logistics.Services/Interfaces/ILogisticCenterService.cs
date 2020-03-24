using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Logistics.Services.Interfaces
{
    public interface ILogisticCenterService : IService
    {
        Task<string> GetCurrent();
        Task<string> GetCalculated();
        Task SetLocation(string cityName);
    }
}
