using System;
using System.ComponentModel.DataAnnotations;

namespace SuperEats.Features.Users.Models;

public class User
{
    [Key]
    public int UserId { get; set; }

    [Required]
    [StringLength(255, MinimumLength = 3)]
    public string Username { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    
    [Required]
    public string Password { get; set; } = string.Empty;

    [Phone]
    public string? PhoneNumber { get; set; }
    public int? AddressId { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}
