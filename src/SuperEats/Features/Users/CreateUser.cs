using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SuperEats.Features.Users.Models;

namespace SuperEats.Features.Users
{
    public class CreateUser
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
                await _userRepository.AddUserAsync(request.User);

                return new Response
                {
                   UserId = request.User.UserId
                };
            }
        }

        public class Request : IRequest<Response>
        {
            public User User { get; set; } = new User();
        }

        public class Response
        {
           public int UserId { get; set; }
        }

    }
}
