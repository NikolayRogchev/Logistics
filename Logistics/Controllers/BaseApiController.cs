using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Logistics.App.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController : ControllerBase
    {
        protected readonly ILogger<BaseApiController> logger;

        public BaseApiController(ILogger<BaseApiController> logger)
        {
            this.logger = logger;
        }

        protected void LogError(Exception e)
        {
            this.logger.LogError(e.Message, new object[] { e });
        }
    }
}