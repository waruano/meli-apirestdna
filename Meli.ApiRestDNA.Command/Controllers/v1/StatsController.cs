using System.Net;
using System.Threading.Tasks;
using Mapster;
using MediatR;
using Meli.ApiRestDNA.Application.Queries;
using Meli.ApiRestDNA.Model;
using Microsoft.AspNetCore.Mvc;

namespace Meli.ApiRestDNA.Controllers.v1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class StatsController
    {

        private readonly IMediator _mediator;

        public StatsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get Stats
        /// </summary>
        /// <returns>json with dna statistics </returns>
        /// <response code="200">ok</response>
        /// <response code="500">Internal server error</response>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult> GetAsync()
        {
            var humanReport = await _mediator.Send(new StatsQuery());
            var response = new ReportResponse()
            {
                CountHumanDna = 0,
                Ratio = 0,
                CountMutantDna = 0
            };
            if (humanReport != null)
            {
                response = humanReport.Adapt<ReportResponse>();
            }
            return new OkObjectResult(response);
        }
    }
}
