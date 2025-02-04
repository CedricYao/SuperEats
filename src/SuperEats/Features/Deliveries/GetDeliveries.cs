using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SuperEats.Features.Deliveries.Models;

namespace SuperEats.Features.Deliveries
{
    public class GetDeliveries
    {
        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IDeliveryRepository _deliveryRepository;

            public Handler(IDeliveryRepository deliveryRepository)
            {
                _deliveryRepository = deliveryRepository;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var deliveries = await _deliveryRepository.GetAllAsync();

                return new Response
                {
                    DeliveryDetails = deliveries.Select(r => new DeliveryDetails
                    {
                        DeliveryId = r.DeliveryId,
                        OrderId = r.OrderId,
                        DriverId = r.DriverId,
                        AssignedTime = r.AssignedTime,
                        PickupTime = r.PickupTime,
                        DeliveryTime = r.DeliveryTime
                    }).ToList()
                };
            }
        }

        public class Request : IRequest<Response>
        {
        }

        public class Response
        {
            public List<DeliveryDetails> DeliveryDetails { get; set; } = new List<DeliveryDetails>();
        }

        public class DeliveryDetails
        {
            public int DeliveryId { get; set; }
            public int OrderId { get; set; }
            public int DriverId { get; set; }
            public System.DateTime? AssignedTime { get; set; }
            public System.DateTime? PickupTime { get; set; }
            public System.DateTime? DeliveryTime { get; set; }
        }
    }
}
