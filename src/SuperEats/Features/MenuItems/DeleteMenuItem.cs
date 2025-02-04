using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace SuperEats.Features.MenuItems
{
    public class DeleteMenuItem
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
                await _menuItemRepository.DeleteMenuItemAsync(request.ItemId);

                return new Response
                {
                   ItemId = request.ItemId
                };
            }
        }

        public class Request : IRequest<Response>
        {
            public int ItemId { get; set; }
        }

        public class Response
        {
           public int ItemId { get; set; }
        }

    }
}
