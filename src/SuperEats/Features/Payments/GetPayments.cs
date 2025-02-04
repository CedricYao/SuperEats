using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SuperEats.Features.Payments.Models;

namespace SuperEats.Features.Payments
{
    public class GetPayments
    {
        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IPaymentRepository _paymentRepository;

            public Handler(IPaymentRepository paymentRepository)
            {
                _paymentRepository = paymentRepository;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var payments = await _paymentRepository.GetAllAsync();

                return new Response
                {
                    PaymentDetails = payments.Select(r => new PaymentDetails
                    {
                        PaymentId = r.PaymentId,
                        OrderId = r.OrderId,
                        PaymentMethod = r.PaymentMethod,
                        Amount = r.Amount,
                        TransactionId = r.TransactionId
                    }).ToList()
                };
            }
        }

        public class Request : IRequest<Response>
        {
        }

        public class Response
        {
            public List<PaymentDetails> PaymentDetails { get; set; } = new List<PaymentDetails>();
        }

        public class PaymentDetails
        {
            public int PaymentId { get; set; }
            public int OrderId { get; set; }
            public string? PaymentMethod { get; set; }
            public decimal Amount { get; set; }
            public string? TransactionId { get; set; }
        }
    }
}
