using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SuperEats.Features.MenuItems.Models;

namespace SuperEats.Features.MenuItems
{
    public class GetMenuItems
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
                var menuItems = await _menuItemRepository.GetAllAsync();

                return new Response
                {
                    MenuItemDetails = menuItems.Select(r => new MenuItemDetails
                    {
                        ItemId = r.ItemId,
                        RestaurantId = r.RestaurantId,
                        Name = r.Name,
                        Description = r.Description,
                        Price = r.Price
                    }).ToList()
                };
            }
        }

        public class Request : IRequest<Response>
        {
        }

        public class Response
        {
            public List<MenuItemDetails> MenuItemDetails { get; set; } = new List<MenuItemDetails>();
        }

        public class MenuItemDetails
        {
            public int ItemId { get; set; }
            public int RestaurantId { get; set; }
            public string Name { get; set; } = string.Empty;
            public string? Description { get; set; }
            public decimal Price { get; set; }
        }
    }
}
