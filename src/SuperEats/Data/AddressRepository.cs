using SuperEats.Features.Addresses.Models;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class AddressRepository : IAddressRepository
{
    private readonly ConcurrentDictionary<int, Address> _addresses = new ConcurrentDictionary<int, Address>();

    public AddressRepository()
    {
        // Add some sample addresses for testing purposes.  Remove in production.
         _addresses.TryAdd(1, new Address
        {
            AddressId = 1,
            StreetAddress = "123 Main St",
            City = "Anytown",
            State = "CA",
            ZipCode = "12345",
            Latitude = 34.052235m,
            Longitude = -118.243683m
        });
         _addresses.TryAdd(2, new Address
        {
            AddressId = 2,
            StreetAddress = "456 Oak Ave",
            City = "Springfield",
            State = "IL",
            ZipCode = "67890",
            Latitude = 39.783730m,
            Longitude = -89.667475m
        });
    }

    public Task<IEnumerable<Address>> GetAllAsync()
    {
        return Task.FromResult(_addresses.Values.AsEnumerable());
    }

    public Task<Address?> GetByIdAsync(int addressId)
    {
        return Task.FromResult(_addresses.GetValueOrDefault(addressId));
    }
    public Task AddAddressAsync(Address address)
    {
        _addresses.TryAdd(address.AddressId, address);
        return Task.CompletedTask;
    }

    public Task UpdateAddressAsync(Address address)
    {
        _addresses.TryGetValue(address.AddressId, out var oldAddress);
        if (oldAddress != null)
        {
            _addresses.TryUpdate(address.AddressId, address, oldAddress);
        }
        return Task.CompletedTask;
    }

    public Task DeleteAddressAsync(int addressId)
    {
         _addresses.TryRemove(addressId, out Address address);
        return Task.CompletedTask;
    }
}

public interface IAddressRepository
{
    Task<IEnumerable<Address>> GetAllAsync();
    Task<Address?> GetByIdAsync(int addressId);
    Task AddAddressAsync(Address address);
    Task UpdateAddressAsync(Address address);
    Task DeleteAddressAsync(int addressId);
}
