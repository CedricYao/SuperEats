using SuperEats.Features.DeliveryDrivers.Models;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class DeliveryDriverRepository : IDeliveryDriverRepository
{
    private readonly ConcurrentDictionary<int, DeliveryDriver> _deliveryDrivers = new ConcurrentDictionary<int, DeliveryDriver>();

    public DeliveryDriverRepository()
    {
        // Add some sample delivery drivers for testing purposes.  Remove in production.
         _deliveryDrivers.TryAdd(1, new DeliveryDriver
        {
            DriverId = 1,
            Name = "Driver 1",
            PhoneNumber = "123-456-7890",
            Availability = true
        });
         _deliveryDrivers.TryAdd(2, new DeliveryDriver
        {
            DriverId = 2,
            Name = "Driver 2",
            PhoneNumber = "987-654-3210",
            Availability = false
        });
    }

    public Task<IEnumerable<DeliveryDriver>> GetAllAsync()
    {
        return Task.FromResult(_deliveryDrivers.Values.AsEnumerable());
    }

    public Task<DeliveryDriver?> GetByIdAsync(int deliveryDriverId)
    {
        return Task.FromResult(_deliveryDrivers.GetValueOrDefault(deliveryDriverId));
    }
    public Task AddDeliveryDriverAsync(DeliveryDriver deliveryDriver)
    {
        _deliveryDrivers.TryAdd(deliveryDriver.DriverId, deliveryDriver);
        return Task.CompletedTask;
    }

    public Task UpdateDeliveryDriverAsync(DeliveryDriver deliveryDriver)
    {
        _deliveryDrivers.TryGetValue(deliveryDriver.DriverId, out var oldDeliveryDriver);
        if (oldDeliveryDriver != null)
        {
            _deliveryDrivers.TryUpdate(deliveryDriver.DriverId, deliveryDriver, oldDeliveryDriver);
        }
        return Task.CompletedTask;
    }

    public Task DeleteDeliveryDriverAsync(int deliveryDriverId)
    {
        _deliveryDrivers.TryRemove(deliveryDriverId, out DeliveryDriver deliveryDriver);
        return Task.CompletedTask;
    }
}

public interface IDeliveryDriverRepository
{
    Task<IEnumerable<DeliveryDriver>> GetAllAsync();
    Task<DeliveryDriver?> GetByIdAsync(int deliveryDriverId);
    Task AddDeliveryDriverAsync(DeliveryDriver deliveryDriver);
    Task UpdateDeliveryDriverAsync(DeliveryDriver deliveryDriver);
    Task DeleteDeliveryDriverAsync(int deliveryDriverId);
}
