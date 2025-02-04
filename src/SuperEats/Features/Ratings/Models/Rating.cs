using System;
using System.ComponentModel.DataAnnotations;

namespace SuperEats.Features.Ratings.Models;

public class Rating
{
    [Key]
    public int RatingId { get; set; }
    public int UserId { get; set; }
    public int RestaurantId { get; set; }
    public int RatingValue { get; set; }
    public string? Comment { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}
