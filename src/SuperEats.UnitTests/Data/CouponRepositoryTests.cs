using Xunit;
using SuperEats.Features.Coupons.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class CouponRepositoryTests
{
    private readonly CouponRepository _couponRepository;

    public CouponRepositoryTests()
    {
        _couponRepository = new CouponRepository();
    }

    [Fact]
    public async Task GetAllAsync_ReturnsAllCoupons()
    {
        // Act
        var coupons = await _couponRepository.GetAllAsync();

        // Assert
        Assert.NotNull(coupons);
        Assert.Equal(2, coupons.Count());
    }

    [Fact]
    public async Task GetByIdAsync_ExistingId_ReturnsCoupon()
    {
        // Act
        var coupon = await _couponRepository.GetByIdAsync(1);

        // Assert
        Assert.NotNull(coupon);
        Assert.Equal(1, coupon.CouponId);
    }

    [Fact]
    public async Task GetByIdAsync_NonExistingId_ReturnsNull()
    {
        // Act
        var coupon = await _couponRepository.GetByIdAsync(999);

        // Assert
        Assert.Null(coupon);
    }

    [Fact]
    public async Task AddCouponAsync_AddsNewCoupon()
    {
        // Arrange
        var newCoupon = new Coupon
        {
            CouponId = 3,
            Code = "SUMMER20",
            Description = "20% off Summer Menu",
            DiscountPercentage = 20.00m,
            StartTime = DateTime.Now,
            EndTime = DateTime.Now.AddDays(30)
        };

        // Act
        await _couponRepository.AddCouponAsync(newCoupon);
        var coupons = await _couponRepository.GetAllAsync();

        // Assert
        Assert.Equal(3, coupons.Count());
        var addedCoupon = await _couponRepository.GetByIdAsync(3);
        Assert.NotNull(addedCoupon);
        Assert.Equal("SUMMER20", addedCoupon.Code);
    }

    [Fact]
    public async Task UpdateCouponAsync_ExistingCoupon_UpdatesCoupon()
    {
        // Arrange
        var existingCoupon = await _couponRepository.GetByIdAsync(1);
        existingCoupon.Description = "Updated Description";
        existingCoupon.DiscountPercentage = 15.00m;

        // Act
        await _couponRepository.UpdateCouponAsync(existingCoupon);

        // Assert
        var updatedCoupon = await _couponRepository.GetByIdAsync(1);
        Assert.NotNull(updatedCoupon);
        Assert.Equal("Updated Description", updatedCoupon.Description);
        Assert.Equal(15.00m, updatedCoupon.DiscountPercentage);
    }

    [Fact]
    public async Task UpdateCouponAsync_NonExistingCoupon_DoesNotUpdate()
    {
        // Arrange
        var nonExistingCoupon = new Coupon { CouponId = 999, Code = "FAKE", Description = "Fake Coupon", DiscountPercentage = 10, StartTime = DateTime.Now, EndTime = DateTime.Now.AddDays(1) };

        // Act
        await _couponRepository.UpdateCouponAsync(nonExistingCoupon);

        // Assert
        var coupon = await _couponRepository.GetByIdAsync(999);
        Assert.Null(coupon);
    }


    [Fact]
    public async Task DeleteCouponAsync_ExistingId_DeletesCoupon()
    {
        // Act
        await _couponRepository.DeleteCouponAsync(1);

        // Assert
        var coupon = await _couponRepository.GetByIdAsync(1);
        Assert.Null(coupon);
        var coupons = await _couponRepository.GetAllAsync();
        Assert.Equal(1, coupons.Count());
    }

    [Fact]
    public async Task DeleteCouponAsync_NonExistingId_DoesNothing()
    {
        // Act
        await _couponRepository.DeleteCouponAsync(999);

        // Assert
        var coupons = await _couponRepository.GetAllAsync();
        Assert.Equal(2, coupons.Count()); // Ensure the number of coupons remains the same
    }
}
