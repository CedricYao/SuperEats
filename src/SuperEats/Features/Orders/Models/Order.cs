using System;
using System.ComponentModel.DataAnnotations;

namespace SuperEats.Features.Orders.Models;

public class Order
{
    [Key]
    public int OrderId { get; set; }

    public int UserId { get; set; }
    public int RestaurantId { get; set; }
    public DateTime OrderTime { get; set; } = DateTime.Now;
    public DateTime? EstimatedDeliveryTime { get; set; }
    public DateTime? ActualDeliveryTime { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal FinalAmount { get; set; }
    public string? Status { get; set; }
    public int? DeliveryAddressId  { get; set; }
    public int? PaymentId  { get; set; }
    public int? RestaurantDiscountId { get;  set; }
    public int? SuperEatsDiscountId  { get; set; }
    public int? CouponId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}
