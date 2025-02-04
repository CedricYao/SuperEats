using System;
using System.ComponentModel.DataAnnotations;

namespace SuperEats.Features.OrderItems.Models;

public class OrderItem
{
    [Key]
    public int OrderItemId { get; set; }

    public int OrderId { get; set; }
    public int ItemId { get; set; }
    public int Quantity { get; set; }

    [Range(0.01, double.MaxValue)]
    public decimal PriceAtOrder { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}
