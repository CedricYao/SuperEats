using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SuperEats.Features.Addresses;

namespace SuperEats.V1.Controllers
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        private readonly IMediator mediator;

        public AddressesController(IMediator mediator)
        {
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet("")]
        public async Task<ActionResult<GetAddresses.Response>> Get()
        {
            var request = new GetAddresses.Request();
            var response = await mediator.Send(request);
            return Ok(response); // Or return response; if you prefer
        }
    }
}
