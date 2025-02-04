using SuperEats.Features.Orders.Models;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class OrderRepository : IOrderRepository
{
    private readonly ConcurrentDictionary<int, Order> _orders = new ConcurrentDictionary<int, Order>();

    public OrderRepository()
    {
        // Add some sample orders for testing purposes.  Remove in production.
        _orders.TryAdd(1, new Order
        {
            OrderId = 1,
            UserId = 1,
            RestaurantId = 1,
            TotalAmount = 25.99m,
            FinalAmount = 23.99m,
            Status = "Pending"
        });
         _orders.TryAdd(2, new Order
        {
            OrderId = 2,
            UserId = 2,
            RestaurantId = 2,
            TotalAmount = 15.99m,
            FinalAmount = 13.99m,
            Status = "Delivered"
        });
    }

    public Task<IEnumerable<Order>> GetAllAsync()
    {
        return Task.FromResult(_orders.Values.AsEnumerable());
    }

    public Task<Order?> GetByIdAsync(int orderId)
    {
        return Task.FromResult(_orders.GetValueOrDefault(orderId));
    }
    public Task AddOrderAsync(Order order)
    {
        _orders.TryAdd(order.OrderId, order);
        return Task.CompletedTask;
    }

    public Task UpdateOrderAsync(Order order)
    {
        _orders.TryGetValue(order.OrderId, out var oldOrder);
        if (oldOrder != null)
        {
            _orders.TryUpdate(order.OrderId, order, oldOrder);
        }
        return Task.CompletedTask;
    }

    public Task DeleteOrderAsync(int orderId)
    {
        _orders.TryRemove(orderId, out Order order);
        return Task.CompletedTask;
    }
}

public interface IOrderRepository
{
    Task<IEnumerable<Order>> GetAllAsync();
    Task<Order?> GetByIdAsync(int orderId);
    Task AddOrderAsync(Order order);
    Task UpdateOrderAsync(Order order);
    Task DeleteOrderAsync(int orderId);
}
