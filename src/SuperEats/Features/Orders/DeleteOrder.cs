using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace SuperEats.Features.Orders
{
    public class DeleteOrder
    {
        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IOrderRepository _orderRepository;

            public Handler(IOrderRepository orderRepository)
            {
                _orderRepository = orderRepository;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                await _orderRepository.DeleteOrderAsync(request.OrderId);

                return new Response
                {
                   OrderId = request.OrderId
                };
            }
        }

        public class Request : IRequest<Response>
        {
            public int OrderId { get; set; }
        }

        public class Response
        {
           public int OrderId { get; set; }
        }

    }
}
