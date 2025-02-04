using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace SuperEats.Features.OrderItems
{
    public class DeleteOrderItem
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
                await _orderItemRepository.DeleteOrderItemAsync(request.OrderItemId);

                return new Response
                {
                   OrderItemId = request.OrderItemId
                };
            }
        }

        public class Request : IRequest<Response>
        {
            public int OrderItemId { get; set; }
        }

        public class Response
        {
           public int OrderItemId { get; set; }
        }

    }
}
