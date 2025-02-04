using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SuperEats.Features.MenuItems.Models;

namespace SuperEats.Features.MenuItems
{
    public class UpdateMenuItem
    {
        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IMenuItemRepository _menuItemRepository;

            public Handler(IMenuItemRepository menuItemRepository)
            {
                _menuItemRepository = menuItemRepository;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                await _menuItemRepository.UpdateMenuItemAsync(request.MenuItem);

                return new Response
                {
                   ItemId = request.MenuItem.ItemId
                };
            }
        }

        public class Request : IRequest<Response>
        {
            public MenuItem MenuItem { get; set; } = new MenuItem();
        }

        public class Response
        {
           public int ItemId { get; set; }
        }

    }
}
