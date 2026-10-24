namespace CarRentalApi.Dtos;
public record LoginDto(string Email);
public record RegisterDto(string FullName, string Email, string? Phone);
public record UserDto(int UserId, string FullName, string Email, string Role, string? Phone);
