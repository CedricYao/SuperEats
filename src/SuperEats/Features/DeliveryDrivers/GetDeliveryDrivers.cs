using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SuperEats.Features.DeliveryDrivers.Models;

namespace SuperEats.Features.DeliveryDrivers
{
    public class GetDeliveryDrivers
    {
        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IDeliveryDriverRepository _deliveryDriverRepository;

            public Handler(IDeliveryDriverRepository deliveryDriverRepository)
            {
                _deliveryDriverRepository = deliveryDriverRepository;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var deliveryDrivers = await _deliveryDriverRepository.GetAllAsync();

                return new Response
                {
                    DeliveryDriverDetails = deliveryDrivers.Select(r => new DeliveryDriverDetails
                    {
                        DriverId = r.DriverId,
                        Name = r.Name,
                        PhoneNumber = r.PhoneNumber,
                        Availability = r.Availability
                    }).ToList()
                };
            }
        }

        public class Request : IRequest<Response>
        {
        }

        public class Response
        {
            public List<DeliveryDriverDetails> DeliveryDriverDetails { get; set; } = new List<DeliveryDriverDetails>();
        }

        public class DeliveryDriverDetails
        {
            public int DriverId { get; set; }
            public string? Name { get; set; }
            public string? PhoneNumber { get; set; }
            public bool Availability { get; set; }
        }
    }
}
