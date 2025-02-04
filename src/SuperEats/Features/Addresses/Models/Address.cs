using System;
using System.ComponentModel.DataAnnotations;

namespace SuperEats.Features.Addresses.Models;

public class Address
{
    [Key]
    public int AddressId { get; set; }
    public string? StreetAddress { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? ZipCode { get; set; }

    [Range(-90.000000, 90.000000)]
    public decimal? Latitude { get; set; }
    [Range(-180.000000, 180.000000)]
    public decimal? Longitude { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}
