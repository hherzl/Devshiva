namespace MudBlazorWebApp1.Client.Services.Models;

public record ProductListModel
{
    public ProductListModel()
    {
    }

    public ProductListModel(int id, string? name, string? category, string? platform, decimal unitPrice, DateTime releaseDate)
    {
        Id = id;
        Name = name;
        Category = category;
        Platform = platform;
        UnitPrice = unitPrice;
        ReleaseDate = releaseDate;
    }

    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Category { get; set; }
    public string? Platform { get; set; }
    public decimal UnitPrice { get; set; }
    public DateTime ReleaseDate { get; set; }
}
