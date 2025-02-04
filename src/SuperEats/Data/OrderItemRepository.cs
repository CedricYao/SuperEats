using SuperEats.Features.OrderItems.Models;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class OrderItemRepository : IOrderItemRepository
{
    private readonly ConcurrentDictionary<int, OrderItem> _orderItems = new ConcurrentDictionary<int, OrderItem>();

    public OrderItemRepository()
    {
        // Add some sample order items for testing purposes.  Remove in production.
         _orderItems.TryAdd(1, new OrderItem
        {
            OrderItemId = 1,
            OrderId = 1,
            ItemId = 1,
            Quantity = 2,
            PriceAtOrder = 12.99m
        });
        _orderItems.TryAdd(2, new OrderItem
        {
            OrderItemId = 2,
            OrderId = 2,
            ItemId = 2,
            Quantity = 1,
            PriceAtOrder = 8.99m
        });
    }

    public Task<IEnumerable<OrderItem>> GetAllAsync()
    {
        return Task.FromResult(_orderItems.Values.AsEnumerable());
    }

    public Task<OrderItem?> GetByIdAsync(int orderItemId)
    {
        return Task.FromResult(_orderItems.GetValueOrDefault(orderItemId));
    }
    public Task AddOrderItemAsync(OrderItem orderItem)
    {
        _orderItems.TryAdd(orderItem.OrderItemId, orderItem);
        return Task.CompletedTask;
    }

    public Task UpdateOrderItemAsync(OrderItem orderItem)
    {
        _orderItems.TryGetValue(orderItem.OrderItemId, out var oldOrderItem);
        if (oldOrderItem != null)
        {
            _orderItems.TryUpdate(orderItem.OrderItemId, orderItem, oldOrderItem);
        }
        return Task.CompletedTask;
    }

    public Task DeleteOrderItemAsync(int orderItemId)
    {
         _orderItems.TryRemove(orderItemId, out OrderItem orderItem);
        return Task.CompletedTask;
    }
}

public interface IOrderItemRepository
{
    Task<IEnumerable<OrderItem>> GetAllAsync();
    Task<OrderItem?> GetByIdAsync(int orderItemId);
    Task AddOrderItemAsync(OrderItem orderItem);
    Task UpdateOrderItemAsync(OrderItem orderItem);
    Task DeleteOrderItemAsync(int orderItemId);
}
