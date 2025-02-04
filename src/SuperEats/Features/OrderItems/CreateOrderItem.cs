using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SuperEats.Features.OrderItems.Models;

namespace SuperEats.Features.OrderItems
{
    public class CreateOrderItem
    {
        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IOrderItemRepository _orderItemRepository;

            public Handler(IOrderItemRepository orderItemRepository)
            {
                _orderItemRepository = orderItemRepository;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                await _orderItemRepository.AddOrderItemAsync(request.OrderItem);

                return new Response
                {
                   OrderItemId = request.OrderItem.OrderItemId
                };
            }
        }

        public class Request : IRequest<Response>
        {
            public OrderItem OrderItem { get; set; } = new OrderItem();
        }

        public class Response
        {
           public int OrderItemId { get; set; }
        }

    }
}
