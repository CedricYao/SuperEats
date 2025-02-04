using Xunit;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SuperEats.Features.Payments.Models;

public class PaymentRepositoryTests
{
    private readonly PaymentRepository _repository;

    public PaymentRepositoryTests()
    {
        // Initialize a new repository instance before each test
        _repository = new PaymentRepository();
    }

    [Fact]
    public async Task GetAllAsync_ReturnsAllPayments()
    {
        // Arrange

        // Act
        var payments = await _repository.GetAllAsync();

        // Assert
        Assert.NotNull(payments);
        Assert.True(payments.Count() >= 2); // Assuming initial sample data
    }

    [Fact]
    public async Task GetByIdAsync_ExistingId_ReturnsCorrectPayment()
    {
        // Arrange
        int existingId = 1;

        // Act
        var payment = await _repository.GetByIdAsync(existingId);

        // Assert
        Assert.NotNull(payment);
        Assert.Equal(existingId, payment.PaymentId);
    }

    [Fact]
    public async Task GetByIdAsync_NonExistingId_ReturnsNull()
    {
        // Arrange
        int nonExistingId = 999;

        // Act
        var payment = await _repository.GetByIdAsync(nonExistingId);

        // Assert
        Assert.Null(payment);
    }

    [Fact]
    public async Task AddPaymentAsync_AddsNewPayment()
    {
        // Arrange
        var newPayment = new Payment
        {
            PaymentId = 3,
            OrderId = 3,
            PaymentMethod = "Gift Card",
            Amount = 10.00m,
            TransactionId = "TXN789"
        };

        // Act
        await _repository.AddPaymentAsync(newPayment);
        var addedPayment = await _repository.GetByIdAsync(3);

        // Assert
        Assert.NotNull(addedPayment);
        Assert.Equal(newPayment.OrderId, addedPayment.OrderId);
        Assert.Equal(newPayment.PaymentMethod, addedPayment.PaymentMethod);
        Assert.Equal(newPayment.Amount, addedPayment.Amount);
        Assert.Equal(newPayment.TransactionId, addedPayment.TransactionId);
    }

    [Fact]
    public async Task UpdatePaymentAsync_UpdatesExistingPayment()
    {
        // Arrange
        int existingId = 1;
        var updatedPayment = new Payment
        {
            PaymentId = existingId,
            OrderId = 4,
            PaymentMethod = "Credit Card",
            Amount = 25.99m,
            TransactionId = "TXN123Updated"
        };

        // Act
        await _repository.UpdatePaymentAsync(updatedPayment);
        var retrievedPayment = await _repository.GetByIdAsync(existingId);

        // Assert
        Assert.NotNull(retrievedPayment);
        Assert.Equal(updatedPayment.OrderId, retrievedPayment.OrderId);
        Assert.Equal(updatedPayment.PaymentMethod, retrievedPayment.PaymentMethod);
        Assert.Equal(updatedPayment.Amount, retrievedPayment.Amount);
        Assert.Equal(updatedPayment.TransactionId, retrievedPayment.TransactionId);
    }

    [Fact]
    public async Task UpdatePaymentAsync_NonExistingId_DoesNotThrowException()
    {
        // Arrange
        int nonExistingId = 999;
        var updatedPayment = new Payment
        {
            PaymentId = nonExistingId,
            OrderId = 4,
            PaymentMethod = "Credit Card",
            Amount = 25.99m,
            TransactionId = "TXN123Updated"
        };

        // Act
        await _repository.UpdatePaymentAsync(updatedPayment);
        var retrievedPayment = await _repository.GetByIdAsync(nonExistingId);

        // Assert
        Assert.Null(retrievedPayment);
    }


    [Fact]
    public async Task DeletePaymentAsync_DeletesExistingPayment()
    {
        // Arrange
        int existingId = 1;

        // Act
        await _repository.DeletePaymentAsync(existingId);
        var deletedPayment = await _repository.GetByIdAsync(existingId);

        // Assert
        Assert.Null(deletedPayment);
    }

    [Fact]
    public async Task DeletePaymentAsync_NonExistingId_DoesNotThrowException()
    {
        // Arrange
        int nonExistingId = 999;

        // Act
        await _repository.DeletePaymentAsync(nonExistingId);
        var deletedPayment = await _repository.GetByIdAsync(nonExistingId);

        // Assert
        Assert.Null(deletedPayment); // Already null, but ensures no exception
    }
}
