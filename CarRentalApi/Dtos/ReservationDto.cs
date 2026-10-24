namespace CarRentalApi.Dtos;
public record CreateReservationDto(int UserId, int CarId, DateTime StartDate, DateTime EndDate);
