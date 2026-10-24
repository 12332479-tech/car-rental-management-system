using System.ComponentModel.DataAnnotations;

namespace CarRentalApi.Models;

public class Reservation
{
    public int ReservationId { get; set; }
    public int UserId { get; set; }
    public User? User { get; set; }
    public int CarId { get; set; }
    public Car? Car { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal TotalPrice { get; set; }
    [MaxLength(20)] public string Status { get; set; } = "Booked";
}
