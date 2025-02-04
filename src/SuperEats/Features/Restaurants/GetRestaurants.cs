using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SuperEats.Restaurants.Models;

namespace SuperEats.Features.Restaurants
{
    public class GetRestaurants
    {
        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IRestaurantRepository _restaurantRepository;

            public Handler(IRestaurantRepository restaurantRepository)
            {
                _restaurantRepository = restaurantRepository;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var restaurants = await _restaurantRepository.GetAllAsync();

                return new Response
                {
                    RestaurantDetails = restaurants.Select(r => new RestaurantDetails
                    {
                        RestaurantId = r.RestaurantId,
                        Name = r.Name,
                        Cuisine = r.Cuisine,
                        OperatingHours = r.OperatingHours
                    }).ToList()
                };
            }
        }

        public class Request : IRequest<Response>
        {
        }

        public class Response
        {
            public List<RestaurantDetails> RestaurantDetails { get; set; } = new List<RestaurantDetails>();
        }

        public class RestaurantDetails
        {
            public int RestaurantId { get; set; }
            public string Name { get; set; } = string.Empty;
            public string? Cuisine { get; set; }
            public string? OperatingHours { get; set; }
        }
    }
}
