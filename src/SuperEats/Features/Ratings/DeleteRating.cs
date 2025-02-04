using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace SuperEats.Features.Ratings
{
    public class DeleteRating
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
                await _ratingRepository.DeleteRatingAsync(request.RatingId);

                return new Response
                {
                   RatingId = request.RatingId
                };
            }
        }

        public class Request : IRequest<Response>
        {
            public int RatingId { get; set; }
        }

        public class Response
        {
           public int RatingId { get; set; }
        }

    }
}
