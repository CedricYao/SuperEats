using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SuperEats.Features.Payments.Models;

namespace SuperEats.Features.Payments
{
    public class UpdatePayment
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
                await _paymentRepository.UpdatePaymentAsync(request.Payment);

                return new Response
                {
                   PaymentId = request.Payment.PaymentId
                };
            }
        }

        public class Request : IRequest<Response>
        {
            public Payment Payment { get; set; } = new Payment();
        }

        public class Response
        {
           public int PaymentId { get; set; }
        }

    }
}
