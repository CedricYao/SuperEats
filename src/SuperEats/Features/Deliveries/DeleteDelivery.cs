using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace SuperEats.Features.Deliveries
{
    public class DeleteDelivery
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
                await _deliveryRepository.DeleteDeliveryAsync(request.DeliveryId);

                return new Response
                {
                   DeliveryId = request.DeliveryId
                };
            }
        }

        public class Request : IRequest<Response>
        {
            public int DeliveryId { get; set; }
        }

        public class Response
        {
           public int DeliveryId { get; set; }
        }

    }
}
