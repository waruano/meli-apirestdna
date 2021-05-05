using MediatR;
using Meli.ApiRestDNA.Application.Commands;
using Meli.ApiRestDNA.Model;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using Meli.ApiRestDNA.Shared.Model;

namespace Meli.ApiRestDNA.Controllers.v1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class MutantController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MutantController(IMediator mediator)
        {
            _mediator = mediator;
        }


        /// <summary>
        /// Evaluate dna.
        /// </summary>
        /// <param name="request"></param>
        /// <returns>dna evaluated.</returns>
        /// <response code="200">the dna is from a mutant</response>
        /// <response code="400">the dna was invalid</response>
        /// <response code="403">the dna is from a human</response>
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorDetails),(int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.Forbidden)]
        public async Task<ActionResult> PostAsync([FromBody] DnaRequest request)
        {
            await _mediator.Send(new DnaValidatorCommand(request.Dna));
            return Ok();
        }
    }
}
