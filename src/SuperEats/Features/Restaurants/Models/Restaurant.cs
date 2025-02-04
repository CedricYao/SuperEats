using System;
using System.ComponentModel.DataAnnotations;

namespace SuperEats.Restaurants.Models;

public class Restaurant
{
    [Key]
    public int RestaurantId { get; set; }

    [Required]
    [StringLength(255, MinimumLength = 3)]
    public string Name { get; set; } = string.Empty;
    public int? AddressId { get; set; }
    public string? Cuisine { get; set; }
    public string? OperatingHours { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    public bool IsLastHourDeal {get; set;} = false;
}
