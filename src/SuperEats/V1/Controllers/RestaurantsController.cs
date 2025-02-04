using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SuperEats.Features.Restaurants;

namespace SuperEats.V1.Controllers
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class RestaurantsController : ControllerBase
    {
        private readonly IMediator mediator;

        public RestaurantsController(IMediator mediator)
        {
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }


        [HttpGet("")]
        public async Task<ActionResult<GetRestaurants.Response>> GetValues(string param1, string param2)
        {
            var request = new GetRestaurants.Request();
            return await mediator.Send<GetRestaurants.Response>(request);
        }
    }
}