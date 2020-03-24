using Logistics.Data;
using Logistics.Models;
using Logistics.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Logistics.Services.Implementations
{
    public class PathService : IPathService
    {
        public PathService(LogisticsDbContext db)
        {
            Db = db;
        }


        public LogisticsDbContext Db { get; }

        public async Task AddAsync(int fromCityId, int toCityId, double pathLength)
        {
            //todo fetch the cities and set the navigation properties
            Path path = new Path()
            {
                FromId = fromCityId,
                ToId = toCityId,
                Length = pathLength
            };

            await this.Db.Paths.AddAsync(path);
            await this.Db.SaveChangesAsync();
        }

        public async Task<IEnumerable<Path>> AllAsync()
        {
            return await this.Db.Paths.Include(p => p.From).Include(p => p.To).ToListAsync();
        }

        public async Task UpdateAsync(int id, int fromCityId, int toCityId, double length)
        {
            City from = await this.Db.Cities.FirstOrDefaultAsync(c => c.Id == fromCityId);
            City to = await this.Db.Cities.FirstOrDefaultAsync(c => c.Id == toCityId);
            Path path = await this.Db.Paths.FirstOrDefaultAsync(p => p.Id == id);

            path.From = from;
            path.To = to;
            path.Length = length;

            await this.Db.SaveChangesAsync();
        }
    }
}
