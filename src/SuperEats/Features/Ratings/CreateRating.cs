using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SuperEats.Features.Ratings.Models;

namespace SuperEats.Features.Ratings
{
    public class CreateRating
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
                await _ratingRepository.AddRatingAsync(request.Rating);

                return new Response
                {
                   RatingId = request.Rating.RatingId
                };
            }
        }

        public class Request : IRequest<Response>
        {
            public Rating Rating { get; set; } = new Rating();
        }

        public class Response
        {
           public int RatingId { get; set; }
        }

    }
}
