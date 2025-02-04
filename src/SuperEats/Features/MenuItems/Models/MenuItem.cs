using System;
using System.ComponentModel.DataAnnotations;

namespace SuperEats.Features.MenuItems.Models;

public class MenuItem
{
    [Key]
    public int ItemId { get; set; }

    public int RestaurantId { get; set; }

    [Required]
    [StringLength(255, MinimumLength = 3)]
    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    [Required]
    [Range(0.01, double.MaxValue)]
    public decimal Price { get; set; }

    public string? Category { get; set; }
    public string? Ingredients { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}
