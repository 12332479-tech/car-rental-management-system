using System.ComponentModel.DataAnnotations;

namespace CarRentalApi.Models;

public class Car
{
    public int CarId { get; set; }
    [Required, MaxLength(50)] public string Brand { get; set; } = string.Empty;
    [Required, MaxLength(50)] public string Model { get; set; } = string.Empty;
    [MaxLength(30)] public string Color { get; set; } = string.Empty;
    [MaxLength(30)] public string CarClass { get; set; } = string.Empty;
    public decimal PricePerDay { get; set; }
    public bool IsAvailable { get; set; } = true;
    public List<Reservation> Reservations { get; set; } = new();
}
