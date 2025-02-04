using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SuperEats.Features.DeliveryDrivers.Models;

namespace SuperEats.Features.DeliveryDrivers
{
    public class CreateDeliveryDriver
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
                await _deliveryDriverRepository.AddDeliveryDriverAsync(request.DeliveryDriver);

                return new Response
                {
                   DriverId = request.DeliveryDriver.DriverId
                };
            }
        }

        public class Request : IRequest<Response>
        {
            public DeliveryDriver DeliveryDriver { get; set; } = new DeliveryDriver();
        }

        public class Response
        {
           public int DriverId { get; set; }
        }

    }
}
