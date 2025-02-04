using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace SuperEats.Features.Users
{
    public class DeleteUser
    {
        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IUserRepository _userRepository;

            public Handler(IUserRepository userRepository)
            {
                _userRepository = userRepository;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                await _userRepository.DeleteUserAsync(request.UserId);

                return new Response
                {
                   UserId = request.UserId
                };
            }
        }

        public class Request : IRequest<Response>
        {
            public int UserId { get; set; }
        }

        public class Response
        {
           public int UserId { get; set; }
        }

    }
}
