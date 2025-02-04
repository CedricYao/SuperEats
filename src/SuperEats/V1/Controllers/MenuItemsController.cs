using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SuperEats.Features.MenuItems;

namespace SuperEats.V1.Controllers
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class MenuItemsController : ControllerBase
    {
        private readonly IMediator mediator;

        public MenuItemsController(IMediator mediator)
        {
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        //endpoints will be added here
    }
}
