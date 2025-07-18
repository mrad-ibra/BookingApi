using System.Diagnostics;

public class HomeAvailabilityService : IHomeAvailabilityService
{
    private readonly ILogger<HomeAvailabilityService> _logger;

    public HomeAvailabilityService(ILogger<HomeAvailabilityService> logger)
    {
        _logger = logger;
    }

    public ValueTask<List<AvailableHomeResponse.HomeDto>> GetAvailableHomesAsync(DateOnly startDate, DateOnly endDate, CancellationToken cancellationToken)
    {
        var stopwatch = Stopwatch.StartNew();

        var dateRange = Enumerable.Range(0, endDate.DayNumber - startDate.DayNumber + 1)
                                  .Select(i => startDate.AddDays(i))
                                  .ToHashSet();

        var availableHomes = InMemoryHomeData.Homes.Values
            .AsParallel()
            .WithCancellation(cancellationToken)
            .Where(home => dateRange.IsSubsetOf(home.AvailableSlots))
            .Select(home => new AvailableHomeResponse.HomeDto
            {
                HomeId = home.HomeId,
                HomeName = home.HomeName,
                AvailableSlots = dateRange.Select(d => d.ToString("yyyy-MM-dd")).ToList()
            })
            .ToList();

        stopwatch.Stop();
        _logger.LogInformation("Filtered {Count} homes in {Ms}ms", availableHomes.Count, stopwatch.ElapsedMilliseconds);

        return ValueTask.FromResult(availableHomes);
    }
}