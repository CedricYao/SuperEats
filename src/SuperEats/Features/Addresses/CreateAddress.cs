using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SuperEats.Features.Addresses.Models;

namespace SuperEats.Features.Addresses
{
    public class CreateAddress
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
                await _addressRepository.AddAddressAsync(request.Address);

                return new Response
                {
                   AddressId = request.Address.AddressId
                };
            }
        }

        public class Request : IRequest<Response>
        {
            public Address Address { get; set; } = new Address();
        }

        public class Response
        {
           public int AddressId { get; set; }
        }

    }
}
