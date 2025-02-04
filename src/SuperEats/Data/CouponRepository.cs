using SuperEats.Features.Coupons.Models;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class CouponRepository : ICouponRepository
{
    private readonly ConcurrentDictionary<int, Coupon> _coupons = new ConcurrentDictionary<int, Coupon>();

    public CouponRepository()
    {
        // Add some sample coupons for testing purposes.  Remove in production.
         _coupons.TryAdd(1, new Coupon
        {
            CouponId = 1,
            Code = "SAVE10",
            Description = "Save 10% on your next order",
            DiscountPercentage = 10.00m,
            StartTime = System.DateTime.Now,
            EndTime = System.DateTime.Now.AddDays(14)
        });
        _coupons.TryAdd(2, new Coupon
        {
            CouponId = 2,
            Code = "WELCOME5",
            Description = "$5 off your first order",
            DiscountPercentage = 5.00m,
            StartTime = System.DateTime.Now,
            EndTime = System.DateTime.Now.AddDays(7)
        });
    }

    public Task<IEnumerable<Coupon>> GetAllAsync()
    {
        return Task.FromResult(_coupons.Values.AsEnumerable());
    }

    public Task<Coupon?> GetByIdAsync(int couponId)
    {
        return Task.FromResult(_coupons.GetValueOrDefault(couponId));
    }
    public Task AddCouponAsync(Coupon coupon)
    {
        _coupons.TryAdd(coupon.CouponId, coupon);
        return Task.CompletedTask;
    }

    public Task UpdateCouponAsync(Coupon coupon)
    {
         _coupons.TryGetValue(coupon.CouponId, out var oldCoupon);
        if (oldCoupon != null)
        {
            _coupons.TryUpdate(coupon.CouponId, coupon, oldCoupon);
        }
        return Task.CompletedTask;
    }

    public Task DeleteCouponAsync(int couponId)
    {
         _coupons.TryRemove(couponId, out Coupon coupon);
        return Task.CompletedTask;
    }
}

public interface ICouponRepository
{
    Task<IEnumerable<Coupon>> GetAllAsync();
    Task<Coupon?> GetByIdAsync(int couponId);
    Task AddCouponAsync(Coupon coupon);
    Task UpdateCouponAsync(Coupon coupon);
    Task DeleteCouponAsync(int couponId);
}
