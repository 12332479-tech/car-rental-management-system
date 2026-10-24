namespace CarRentalApi.Services;

// Interface requirement: API logging service contract.
public interface IFileLogService
{
    Task WriteAsync(string message);
}
