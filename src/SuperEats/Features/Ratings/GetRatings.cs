using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SuperEats.Features.Ratings.Models;

namespace SuperEats.Features.Ratings
{
    public class GetRatings
    {
        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IRatingRepository _ratingRepository;

            public Handler(IRatingRepository ratingRepository)
            {
                _ratingRepository = ratingRepository;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var ratings = await _ratingRepository.GetAllAsync();

                return new Response
                {
                    RatingDetails = ratings.Select(r => new RatingDetails
                    {
                        RatingId = r.RatingId,
                        UserId = r.UserId,
                        RestaurantId = r.RestaurantId,
                        RatingValue = r.RatingValue,
                        Comment = r.Comment
                    }).ToList()
                };
            }
        }

        public class Request : IRequest<Response>
        {
        }

        public class Response
        {
            public List<RatingDetails> RatingDetails { get; set; } = new List<RatingDetails>();
        }

        public class RatingDetails
        {
            public int RatingId { get; set; }
            public int UserId { get; set; }
            public int RestaurantId { get; set; }
            public int RatingValue { get; set; }
            public string? Comment { get; set; }
        }
    }
}
