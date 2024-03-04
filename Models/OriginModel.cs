namespace EquiMarketApp.Models;

public class Origin
{
    public int OriginId { get; set; }
    public string Country { get; set; }

    // Navigation property 
    public ICollection<Ad> Ads { get; set; }
}