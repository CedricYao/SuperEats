using System;
using System.ComponentModel.DataAnnotations;

namespace SuperEats.Features.SuperEatsDiscounts.Models;

public class SuperEatsDiscount
{
    [Key]
    public int SuperEatsDiscountId { get; set; }
    public string? Description { get; set; }
    public decimal DiscountPercentage { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}
