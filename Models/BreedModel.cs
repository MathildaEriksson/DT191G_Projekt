namespace EquiMarketApp.Models;

public class Breed
{
    public int BreedId { get; set; }
    public string Name { get; set; }

    // Navigation property
    public ICollection<Ad> Ads { get; set; }
}