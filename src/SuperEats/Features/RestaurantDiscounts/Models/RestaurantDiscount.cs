using System;
using System.ComponentModel.DataAnnotations;

namespace SuperEats.Features.RestaurantDiscounts.Models;

public class RestaurantDiscount
{
    [Key]
    public int RestaurantDiscountId { get; set; }
    public int RestaurantId { get; set; }
    public string? Description { get; set; }
    public decimal DiscountPercentage { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}
