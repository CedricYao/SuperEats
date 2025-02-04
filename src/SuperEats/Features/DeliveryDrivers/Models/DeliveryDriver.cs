using System;
using System.ComponentModel.DataAnnotations;

namespace SuperEats.Features.DeliveryDrivers.Models;

public class DeliveryDriver
{
    [Key]
    public int DriverId { get; set; }

    public string? Name { get; set; }

    [Phone]
    public string? PhoneNumber { get; set; }

    public bool Availability { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}
