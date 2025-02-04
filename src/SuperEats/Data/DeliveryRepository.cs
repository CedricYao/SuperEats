using SuperEats.Features.Deliveries.Models;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class DeliveryRepository : IDeliveryRepository
{
    private readonly ConcurrentDictionary<int, Delivery> _deliveries = new ConcurrentDictionary<int, Delivery>();

    public DeliveryRepository()
    {
        // Add some sample deliveries for testing purposes.  Remove in production.
        _deliveries.TryAdd(1, new Delivery
        {
            DeliveryId = 1,
            OrderId = 1,
            DriverId = 1,
            AssignedTime = System.DateTime.Now,
            PickupTime = System.DateTime.Now.AddMinutes(10),
            DeliveryTime = System.DateTime.Now.AddMinutes(30)
        });
          _deliveries.TryAdd(2, new Delivery
        {
            DeliveryId = 2,
            OrderId = 2,
            DriverId = 2,
            AssignedTime = System.DateTime.Now,
            PickupTime = System.DateTime.Now.AddMinutes(5),
            DeliveryTime = System.DateTime.Now.AddMinutes(20)
        });
    }

    public Task<IEnumerable<Delivery>> GetAllAsync()
    {
        return Task.FromResult(_deliveries.Values.AsEnumerable());
    }

    public Task<Delivery?> GetByIdAsync(int deliveryId)
    {
        return Task.FromResult(_deliveries.GetValueOrDefault(deliveryId));
    }
    public Task AddDeliveryAsync(Delivery delivery)
    {
        _deliveries.TryAdd(delivery.DeliveryId, delivery);
        return Task.CompletedTask;
    }

    public Task UpdateDeliveryAsync(Delivery delivery)
    {
         _deliveries.TryGetValue(delivery.DeliveryId, out var oldDelivery);
        if (oldDelivery != null)
        {
            _deliveries.TryUpdate(delivery.DeliveryId, delivery, oldDelivery);
        }
        return Task.CompletedTask;
    }

    public Task DeleteDeliveryAsync(int deliveryId)
    {
         _deliveries.TryRemove(deliveryId, out Delivery delivery);
        return Task.CompletedTask;
    }
}

public interface IDeliveryRepository
{
    Task<IEnumerable<Delivery>> GetAllAsync();
    Task<Delivery?> GetByIdAsync(int deliveryId);
    Task AddDeliveryAsync(Delivery delivery);
    Task UpdateDeliveryAsync(Delivery delivery);
    Task DeleteDeliveryAsync(int deliveryId);
}
