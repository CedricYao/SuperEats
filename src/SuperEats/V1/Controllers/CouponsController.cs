using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SuperEats.Features.Coupons;

namespace SuperEats.V1.Controllers
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class CouponsController : ControllerBase
    {
        private readonly IMediator mediator;

        public CouponsController(IMediator mediator)
        {
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        //endpoints will be added here
    }
}
