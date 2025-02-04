using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SuperEats.Features.Orders.Models;

namespace SuperEats.Features.Orders
{
    public class CreateOrder
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
                await _orderRepository.AddOrderAsync(request.Order);

                return new Response
                {
                   OrderId = request.Order.OrderId
                };
            }
        }

        public class Request : IRequest<Response>
        {
            public Order Order { get; set; } = new Order();
        }

        public class Response
        {
           public int OrderId { get; set; }
        }

    }
}
