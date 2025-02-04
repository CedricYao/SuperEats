using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace SuperEats.Features.DeliveryDrivers
{
    public class DeleteDeliveryDriver
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
                await _deliveryDriverRepository.DeleteDeliveryDriverAsync(request.DriverId);

                return new Response
                {
                   DriverId = request.DriverId
                };
            }
        }

        public class Request : IRequest<Response>
        {
            public int DriverId { get; set; }
        }

        public class Response
        {
           public int DriverId { get; set; }
        }

    }
}
