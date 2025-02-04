using SuperEats.Features.Payments.Models;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class PaymentRepository : IPaymentRepository
{
    private readonly ConcurrentDictionary<int, Payment> _payments = new ConcurrentDictionary<int, Payment>();

    public PaymentRepository()
    {
        // Add some sample payments for testing purposes.  Remove in production.
         _payments.TryAdd(1, new Payment
        {
            PaymentId = 1,
            OrderId = 1,
            PaymentMethod = "Credit Card",
            Amount = 23.99m,
            TransactionId = "TXN123"
        });
        _payments.TryAdd(2, new Payment
        {
            PaymentId = 2,
            OrderId = 2,
            PaymentMethod = "Paypal",
            Amount = 13.99m,
            TransactionId = "TXN456"
        });
    }

    public Task<IEnumerable<Payment>> GetAllAsync()
    {
        return Task.FromResult(_payments.Values.AsEnumerable());
    }

    public Task<Payment?> GetByIdAsync(int paymentId)
    {
        return Task.FromResult(_payments.GetValueOrDefault(paymentId));
    }
    public Task AddPaymentAsync(Payment payment)
    {
        _payments.TryAdd(payment.PaymentId, payment);
        return Task.CompletedTask;
    }

    public Task UpdatePaymentAsync(Payment payment)
    {
        _payments.TryGetValue(payment.PaymentId, out var oldPayment);
        if (oldPayment != null)
        {
            _payments.TryUpdate(payment.PaymentId, payment, oldPayment);
        }
        return Task.CompletedTask;
    }

    public Task DeletePaymentAsync(int paymentId)
    {
         _payments.TryRemove(paymentId, out Payment payment);
        return Task.CompletedTask;
    }
}

public interface IPaymentRepository
{
    Task<IEnumerable<Payment>> GetAllAsync();
    Task<Payment?> GetByIdAsync(int paymentId);
    Task AddPaymentAsync(Payment payment);
    Task UpdatePaymentAsync(Payment payment);
    Task DeletePaymentAsync(int paymentId);
}
