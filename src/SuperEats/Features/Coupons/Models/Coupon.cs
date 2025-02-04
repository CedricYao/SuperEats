using System;
using System.ComponentModel.DataAnnotations;

namespace SuperEats.Features.Coupons.Models;

public class Coupon
{
    [Key]
    public int CouponId { get; set; }

    [Required]
    public string Code { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal DiscountPercentage { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}
