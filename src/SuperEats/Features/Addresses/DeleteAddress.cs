using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace SuperEats.Features.Addresses
{
    public class DeleteAddress
    {
        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAddressRepository _addressRepository;

            public Handler(IAddressRepository addressRepository)
            {
                _addressRepository = addressRepository;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                await _addressRepository.DeleteAddressAsync(request.AddressId);

                return new Response
                {
                   AddressId = request.AddressId
                };
            }
        }

        public class Request : IRequest<Response>
        {
            public int AddressId { get; set; }
        }

        public class Response
        {
           public int AddressId { get; set; }
        }

    }
}
