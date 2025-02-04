using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SuperEats.Features.Orders.Models;

namespace SuperEats.Features.Orders
{
    public class GetOrders
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
                var orders = await _orderRepository.GetAllAsync();

                return new Response
                {
                    OrderDetails = orders.Select(r => new OrderDetails
                    {
                        OrderId = r.OrderId,
                        UserId = r.UserId,
                        RestaurantId = r.RestaurantId,
                        TotalAmount = r.TotalAmount,
                        FinalAmount = r.FinalAmount,
                        Status = r.Status
                    }).ToList()
                };
            }
        }

        public class Request : IRequest<Response>
        {
        }

        public class Response
        {
            public List<OrderDetails> OrderDetails { get; set; } = new List<OrderDetails>();
        }

        public class OrderDetails
        {
            public int OrderId { get; set; }
            public int UserId { get; set; }
            public int RestaurantId { get; set; }
            public decimal TotalAmount { get; set; }
            public decimal FinalAmount { get; set; }
            public string? Status { get; set; }
        }
    }
}
