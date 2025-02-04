using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SuperEats.Features.OrderItems.Models;

namespace SuperEats.Features.OrderItems
{
    public class GetOrderItems
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
                var orderItems = await _orderItemRepository.GetAllAsync();

                return new Response
                {
                    OrderItemDetails = orderItems.Select(r => new OrderItemDetails
                    {
                        OrderItemId = r.OrderItemId,
                        OrderId = r.OrderId,
                        ItemId = r.ItemId,
                        Quantity = r.Quantity,
                        PriceAtOrder = r.PriceAtOrder
                    }).ToList()
                };
            }
        }

        public class Request : IRequest<Response>
        {
        }

        public class Response
        {
            public List<OrderItemDetails> OrderItemDetails { get; set; } = new List<OrderItemDetails>();
        }

        public class OrderItemDetails
        {
            public int OrderItemId { get; set; }
            public int OrderId { get; set; }
            public int ItemId { get; set; }
            public int Quantity { get; set; }
            public decimal PriceAtOrder { get; set; }
        }
    }
}
