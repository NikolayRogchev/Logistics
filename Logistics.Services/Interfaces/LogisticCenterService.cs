using Logistics.Data;
using Logistics.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace Logistics.Services.Interfaces
{
    public class LogisticCenterService : ILogisticCenterService
    {
        private readonly ICityService cities;
        private readonly IPathService paths;

        public LogisticCenterService(LogisticsDbContext db, ICityService cities, IPathService paths)
        {
            Db = db;
            this.cities = cities;
            this.paths = paths;
        }

        public LogisticsDbContext Db { get; }

        public async Task<string> GetCalculated()
        {
            IEnumerable<City> cities = await this.cities.AllAsync();
            IEnumerable<Path> paths = await this.paths.AllAsync();
            City logisticCenterLocation = null;
            double logisticCenterLocationLongetstPath = double.MaxValue;
            foreach (City city in cities)
            {
                IEnumerable<Path> cityPaths = paths.Where(p => p.From.Id == city.Id);
                if (cityPaths.Any())
                {
                    double longestPath = cityPaths.Select(p => p.Length).Max();
                    if (longestPath < logisticCenterLocationLongetstPath)
                    {
                        logisticCenterLocation = city;
                        logisticCenterLocationLongetstPath = longestPath;
                    }
                }
            }

            return logisticCenterLocation?.Name;
        }

        public async Task<string> GetCurrent()
        {
            LogisticCenter result = await this.Db.LogisticCenters.Include(lc => lc.City).FirstOrDefaultAsync();
            return result?.City?.Name;
        }

        public async Task SetLocation(string cityName)
        {
            City city = await this.cities.GetByNameAsync(cityName);
            if (city != null)
            {
                LogisticCenter lc = await this.Db.LogisticCenters.FirstOrDefaultAsync();
                if (lc == null)
                {
                    lc = new LogisticCenter
                    {
                        City = city
                    };
                    await this.Db.LogisticCenters.AddAsync(lc);
                }
                else
                {
                    lc.City = city;
                }
                await this.Db.SaveChangesAsync();
            }
        }
    }
}
