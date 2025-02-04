using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SuperEats.Features.Users.Models;

namespace SuperEats.Features.Users
{
    public class GetUsers
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
                var users = await _userRepository.GetAllAsync();

                return new Response
                {
                    UserDetails = users.Select(r => new UserDetails
                    {
                        UserId = r.UserId,
                        Username = r.Username,
                        Email = r.Email,
                        PhoneNumber = r.PhoneNumber
                    }).ToList()
                };
            }
        }

        public class Request : IRequest<Response>
        {
        }

        public class Response
        {
            public List<UserDetails> UserDetails { get; set; } = new List<UserDetails>();
        }

        public class UserDetails
        {
            public int UserId { get; set; }
            public string Username { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
            public string? PhoneNumber { get; set; }
        }
    }
}
