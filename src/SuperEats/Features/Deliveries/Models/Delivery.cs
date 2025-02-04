using System;
using System.ComponentModel.DataAnnotations;

namespace SuperEats.Features.Deliveries.Models;

public class Delivery
{
    [Key]
    public int DeliveryId { get; set; }

    public int OrderId { get; set; }
    public int DriverId { get; set; }
    public DateTime? AssignedTime { get; set; }
    public DateTime? PickupTime { get; set; }
    public DateTime? DeliveryTime { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}
