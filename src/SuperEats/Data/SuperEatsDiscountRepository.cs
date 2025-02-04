using SuperEats.Features.SuperEatsDiscounts.Models;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class SuperEatsDiscountRepository : ISuperEatsDiscountRepository
{
    private readonly ConcurrentDictionary<int, SuperEatsDiscount> _SuperEatsDiscounts = new ConcurrentDictionary<int, SuperEatsDiscount>();

    public SuperEatsDiscountRepository()
    {
        // Add some sample Uber Eats discounts for testing purposes.  Remove in production.
         _SuperEatsDiscounts.TryAdd(1, new SuperEatsDiscount
        {
            SuperEatsDiscountId = 1,
            Description = "Free delivery for new users",
            DiscountPercentage = 100.00m,
            StartTime = System.DateTime.Now,
            EndTime = System.DateTime.Now.AddDays(30)
        });
        _SuperEatsDiscounts.TryAdd(2, new SuperEatsDiscount
        {
            SuperEatsDiscountId = 2,
            Description = "20% off on your first order",
            DiscountPercentage = 20.00m,
            StartTime = System.DateTime.Now,
            EndTime = System.DateTime.Now.AddDays(15)
        });
    }

    public Task<IEnumerable<SuperEatsDiscount>> GetAllAsync()
    {
        return Task.FromResult(_SuperEatsDiscounts.Values.AsEnumerable());
    }

    public Task<SuperEatsDiscount?> GetByIdAsync(int SuperEatsDiscountId)
    {
        return Task.FromResult(_SuperEatsDiscounts.GetValueOrDefault(SuperEatsDiscountId));
    }
    public Task AddSuperEatsDiscountAsync(SuperEatsDiscount SuperEatsDiscount)
    {
        _SuperEatsDiscounts.TryAdd(SuperEatsDiscount.SuperEatsDiscountId, SuperEatsDiscount);
        return Task.CompletedTask;
    }

    public Task UpdateSuperEatsDiscountAsync(SuperEatsDiscount SuperEatsDiscount)
    {
         _SuperEatsDiscounts.TryGetValue(SuperEatsDiscount.SuperEatsDiscountId, out var oldSuperEatsDiscount);
        if (oldSuperEatsDiscount != null)
        {
            _SuperEatsDiscounts.TryUpdate(SuperEatsDiscount.SuperEatsDiscountId, SuperEatsDiscount, oldSuperEatsDiscount);
        }
        return Task.CompletedTask;
    }

    public Task DeleteSuperEatsDiscountAsync(int SuperEatsDiscountId)
    {
         _SuperEatsDiscounts.TryRemove(SuperEatsDiscountId, out SuperEatsDiscount SuperEatsDiscount);
        return Task.CompletedTask;
    }
}

public interface ISuperEatsDiscountRepository
{
    Task<IEnumerable<SuperEatsDiscount>> GetAllAsync();
    Task<SuperEatsDiscount?> GetByIdAsync(int SuperEatsDiscountId);
    Task AddSuperEatsDiscountAsync(SuperEatsDiscount SuperEatsDiscount);
    Task UpdateSuperEatsDiscountAsync(SuperEatsDiscount SuperEatsDiscount);
    Task DeleteSuperEatsDiscountAsync(int SuperEatsDiscountId);
}
