using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace SuperEats.Features.Payments
{
    public class DeletePayment
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
                await _paymentRepository.DeletePaymentAsync(request.PaymentId);

                return new Response
                {
                   PaymentId = request.PaymentId
                };
            }
        }

        public class Request : IRequest<Response>
        {
            public int PaymentId { get; set; }
        }

        public class Response
        {
           public int PaymentId { get; set; }
        }

    }
}
