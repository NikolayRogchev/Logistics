using Logistics.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Logistics.Services.Interfaces
{
    public interface ICityService : IService
    {
        Task<IEnumerable<City>> AllAsync();

        Task<City> GetByIdAsync(int id);
        Task<City> GetByNameAsync(string name);

        Task<City> AddAsync(string name, float? latitude, float? longitude);

        Task EditAsync(int id, string name, float? latitude, float? longitude);

        Task AddFromPath(int id, int pathId);

        Task AddToPath(int id, int pathId);

        Task RemoveFromPath(int id, int pathId);

        Task RemoveToPath(int id, int pathId);
        Task<City> UpdateAsync(int cityId, string newName);
    }
}
