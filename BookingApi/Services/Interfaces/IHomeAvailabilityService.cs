public interface IHomeAvailabilityService
{
    ValueTask<List<AvailableHomeResponse.HomeDto>> GetAvailableHomesAsync(DateOnly startDate, DateOnly endDate, CancellationToken cancellationToken);
}