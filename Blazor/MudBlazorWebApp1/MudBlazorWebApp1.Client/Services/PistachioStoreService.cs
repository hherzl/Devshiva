using MudBlazorWebApp1.Client.Services.Models;

namespace MudBlazorWebApp1.Client.Services;

public class PistachioStoreService
{
    public async Task<List<ProductListModel>> GetProductsAsync()
    {
        return await Task.FromResult(new List<ProductListModel>
        {
            new(1, "The King of Fighters XV", "Fighting games", "PlayStation", 49.99m, new DateTime(2022, 2, 17)),
            new(2, "Street Fighter VI", "Fighting games", "PlayStation", 59.99m, new DateTime(2023, 6, 2)),
            new(3, "Tekken 8", "Fighting games", "Xbox", 59.99m, new DateTime(2024, 1, 26)), // Expected release date
            new(4, "Garou: City of the Wolves", "Fighting games", "PC", 59.99m, new DateTime(2024, 12, 15)), // Expected release date
            new(5, "Mortal Kombat 1", "Fighting games", "PlayStation", 69.99m, new DateTime(2023, 9, 14)),
            new(6, "Soulcalibur VI", "Fighting games", "PC", 39.99m, new DateTime(2018, 10, 19)),
            new(7, "Guilty Gear -Strive-", "Fighting games", "PlayStation", 49.99m, new DateTime(2021, 6, 11)),
            new(8, "Dragon Ball FighterZ", "Fighting games", "Xbox", 39.99m, new DateTime(2018, 1, 26)),
            new(9, "BlazBlue: Central Fiction", "Fighting games", "PC", 29.99m, new DateTime(2015, 11, 19)),
            new(10, "Injustice 2", "Fighting games", "Xbox", 49.99m, new DateTime(2017, 5, 16)),
            new(11, "Samurai Shodown", "Fighting games", "PlayStation", 39.99m, new DateTime(2019, 6, 25)),
            new(12, "Dead or Alive 6", "Fighting games", "PC", 29.99m, new DateTime(2019, 3, 1)),
            new(13, "Virtua Fighter 5: Ultimate Showdown", "Fighting games", "PlayStation", 19.99m, new DateTime(2021, 6, 1)),
            new(14, "Power Rangers: Battle for the Grid", "Fighting games", "Xbox", 29.99m, new DateTime(2019, 3, 26)),
            new(15, "Melty Blood: Type Lumina", "Fighting games", "PC", 49.99m, new DateTime(2021, 9, 30)),
            new(16, "Brawlhalla", "Fighting games", "PC", 0.00m, new DateTime(2017, 10, 17)),  // Free-to-play
            new(17, "Super Smash Bros. Ultimate", "Fighting games", "Nintendo Switch", 59.99m, new DateTime(2018, 12, 7)),
            new(18, "Under Night In-Birth Exe:Late[cl-r]", "Fighting games", "PlayStation", 34.99m, new DateTime(2020, 2, 20)),
            new(19, "Granblue Fantasy: Versus", "Fighting games", "PlayStation", 29.99m, new DateTime(2020, 3, 3)),
            new(20, "Rivals of Aether", "Fighting games", "PC", 14.99m, new DateTime(2017, 3, 28)),
        });
    }
}
