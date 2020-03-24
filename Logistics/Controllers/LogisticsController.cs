using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Logistics.Models;
using Logistics.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Logistics.App.Controllers
{
    public class LogisticsController : BaseApiController
    {
        private readonly ICityService cities;
        private readonly IPathService paths;
        private readonly ILogisticCenterService logisticCenterService;

        public LogisticsController(ILogger<BaseApiController> logger, ICityService cities, IPathService paths, ILogisticCenterService logisticCenterService)
            : base(logger)
        {
            this.cities = cities;
            this.paths = paths;
            this.logisticCenterService = logisticCenterService;
        }

        [HttpGet]
        [Route("getcurrent")]
        public async Task<string> GetCurrent()
        {
            string result = await this.logisticCenterService.GetCurrent();
            return result;
        }

        [HttpPost]
        [Route("set")]
        public async Task<string> Set()
        {
            string currentLocation = await this.logisticCenterService.GetCurrent();
            string calculatedLocation = await this.logisticCenterService.GetCalculated();
            if (currentLocation != calculatedLocation)
            {
                await this.logisticCenterService.SetLocation(calculatedLocation);
                return calculatedLocation;
            }
            else
            {
                return currentLocation;
            }
        }
    }
}