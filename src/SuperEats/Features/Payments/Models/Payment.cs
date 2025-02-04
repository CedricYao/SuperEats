using System;
using System.ComponentModel.DataAnnotations;

namespace SuperEats.Features.Payments.Models;

public class Payment
{
    [Key]
    public int PaymentId { get; set; }
    public int OrderId { get; set; }
    public string? PaymentMethod { get; set; }
    public decimal Amount { get; set; }
    public string? TransactionId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}
