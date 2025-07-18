public class AvailableHomeResponse
{
    public string Status { get; set; } = "OK";
    public List<HomeDto> Homes { get; set; } = new();

    public class HomeDto
    {
        public string HomeId { get; set; } = default!;
        public string HomeName { get; set; } = default!;
        public List<string> AvailableSlots { get; set; } = new();
    }
}