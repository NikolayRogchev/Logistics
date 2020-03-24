using Logistics.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Logistics.Services.Interfaces
{
    public interface IPathService : IService
    {
        Task<IEnumerable<Path>> AllAsync();
        Task AddAsync(int fromCityId, int toCityId, double pathLength);
        Task UpdateAsync(int id, int fromCityId, int toCityId, double length);
    }
}
