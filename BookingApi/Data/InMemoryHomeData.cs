using System.Collections.Concurrent;

public static class InMemoryHomeData
{
    public static ConcurrentDictionary<string, Home> Homes = new();

    static InMemoryHomeData()
    {
        Homes.TryAdd("123", new Home
        {
            HomeId = "123",
            HomeName = "Home 1",
            AvailableSlots = new HashSet<DateOnly>
            {
                DateOnly.Parse("2025-07-15"),
                DateOnly.Parse("2025-07-16"),
                DateOnly.Parse("2025-07-17")
            }
        });

        Homes.TryAdd("456", new Home
        {
            HomeId = "456",
            HomeName = "Home 2",
            AvailableSlots = new HashSet<DateOnly>
            {
                DateOnly.Parse("2025-07-17"),
                DateOnly.Parse("2025-07-18")
            }
        });
    }
}