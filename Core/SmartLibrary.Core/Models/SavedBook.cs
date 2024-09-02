namespace SmartLibrary.Core.Models;

public class SavedBook
{
    public Guid Id { get; set; } = new();
    public string BookId { get; set; }
    public string Title { get; set; }
    public string UserName { get; set; }
    public DateTimeOffset SaveDate { get; set; }
    public string Notes { get; set; }
    public Location Location { get; set; }
}
