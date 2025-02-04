using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SuperEats.Features.Deliveries.Models;

namespace SuperEats.Features.Deliveries
{
    public class CreateDelivery
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
                await _deliveryRepository.AddDeliveryAsync(request.Delivery);

                return new Response
                {
                   DeliveryId = request.Delivery.DeliveryId
                };
            }
        }

        public class Request : IRequest<Response>
        {
            public Delivery Delivery { get; set; } = new Delivery();
        }

        public class Response
        {
           public int DeliveryId { get; set; }
        }

    }
}
