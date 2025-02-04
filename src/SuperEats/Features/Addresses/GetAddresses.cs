using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SuperEats.Features.Addresses.Models;

namespace SuperEats.Features.Addresses
{
    public class GetAddresses
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
                var addresses = await _addressRepository.GetAllAsync();

                return new Response
                {
                    AddressDetails = addresses.Select(r => new AddressDetails
                    {
                        AddressId = r.AddressId,
                        StreetAddress = r.StreetAddress,
                        City = r.City,
                        State = r.State,
                        ZipCode = r.ZipCode,
                        Latitude = r.Latitude,
                        Longitude = r.Longitude
                    }).ToList()
                };
            }
        }

        public class Request : IRequest<Response>
        {
        }

        public class Response
        {
            public List<AddressDetails> AddressDetails { get; set; } = new List<AddressDetails>();
        }

        public class AddressDetails
        {
            public int AddressId { get; set; }
            public string? StreetAddress { get; set; }
            public string? City { get; set; }
            public string? State { get; set; }
            public string? ZipCode { get; set; }
            public decimal? Latitude { get; set; }
            public decimal? Longitude { get; set; }
        }
    }
}
