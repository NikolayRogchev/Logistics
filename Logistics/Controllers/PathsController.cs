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
    public class PathsController : BaseApiController
    {
        private readonly IPathService paths;

        public PathsController(ILogger<PathsController> logger, IPathService paths)
            : base(logger)
        {
            this.paths = paths;
        }

        [Route("all")]
        public async Task<IEnumerable<Path>> All()
        {
            return await this.paths.AllAsync();
        }

        [Route("add")]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody]CreatePathModel model)
        {
            try
            {
                await paths.AddAsync(model.FromCityId, model.ToCityId, model.SelectedPathLength);
                return this.Ok();
            }
            catch (Exception e)
            {
                return this.ValidationProblem(detail: e.Message);
            }
        }

        [Route("update")]
        [HttpPost]
        public async Task<IActionResult> Update([FromBody]UpdatePathModel model)
        {
            try
            {
                await paths.UpdateAsync(model.Id, model.FromId, model.ToId, model.Length);
                return this.Ok();
            }
            catch (Exception e)
            {
                return this.ValidationProblem(detail: e.Message);
            }
        }
    }
}