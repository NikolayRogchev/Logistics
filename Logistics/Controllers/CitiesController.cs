using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Logistics.App.Models;
using Logistics.Models;
using Logistics.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Logistics.App.Controllers
{

    public class CitiesController : BaseApiController
    {
        private readonly ICityService cities;

        public CitiesController(ILogger<CitiesController> logger, ICityService cities)
            : base(logger)
        {
            this.cities = cities;
        }

        [HttpGet]
        [Route("all")]
        public async Task<IEnumerable<City>> All()
        {
            IEnumerable<City> result = await this.cities.AllAsync();
            return result;
        }

        [HttpPost]
        [Route("add")]
        public async Task<City> Add(CreateCityModel model)
        {
            try
            {
                City city = await this.cities.AddAsync(model.CityName, null, null);
                return city;
            }
            catch (Exception e)
            {
                this.LogError(e);
                return null;
            }
        }

        [HttpPost]
        [Route("update")]
        public async Task<City> Update(UpdateCityModel model)
        {
            try
            {
                City city = await this.cities.UpdateAsync(model.Id, model.NewName);
                return city;
            }
            catch (Exception e)
            {
                this.LogError(e);
                return null;
            }
        }
    }
}