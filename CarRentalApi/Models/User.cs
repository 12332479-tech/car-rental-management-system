using System.ComponentModel.DataAnnotations;

namespace CarRentalApi.Models;

public class User
{
    public int UserId { get; set; }
    [Required, MaxLength(100)] public string FullName { get; set; } = string.Empty;
    [Required, MaxLength(120)] public string Email { get; set; } = string.Empty;
    [Required, MaxLength(20)] public string Role { get; set; } = "Customer";
    [MaxLength(30)] public string? Phone { get; set; }
    public List<Reservation> Reservations { get; set; } = new();
}
