using Logistics.Models;
using Logistics.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Logistics.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Logistics.Services.Implementations
{
    public class CityService : ICityService
    {
        public CityService(LogisticsDbContext db)
        {
            this.Db = db;
        }

        public LogisticsDbContext Db { get; }

        public async Task<City> AddAsync(string name, float? latitude, float? longitude)
        {
            City city = new City
            {
                Name = name
            };
            await this.Db.Cities.AddAsync(city);
            await this.Db.SaveChangesAsync();
            return city;
        }

        public Task AddFromPath(int id, int pathId)
        {
            throw new NotImplementedException();
        }

        public Task AddToPath(int id, int pathId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<City>> AllAsync()
        {
            return await this.Db.Cities.ToListAsync();
        }

        public Task EditAsync(int id, string name, float? latitude, float? longitude)
        {
            throw new NotImplementedException();
        }

        public Task<City> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<City> GetByNameAsync(string name)
        {
            return await this.Db.Cities.Where(c => c.Name == name).FirstOrDefaultAsync();
        }

        public Task RemoveFromPath(int id, int pathId)
        {
            throw new NotImplementedException();
        }

        public Task RemoveToPath(int id, int pathId)
        {
            throw new NotImplementedException();
        }

        public async Task<City> UpdateAsync(int cityId, string newName)
        {
            City city = this.Db.Cities.Where(c => c.Id == cityId).FirstOrDefault();
            city.Name = newName;
            await this.Db.SaveChangesAsync();
            return city;
        }
    }
}
