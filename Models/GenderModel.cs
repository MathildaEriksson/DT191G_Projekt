namespace EquiMarketApp.Models;

public class Gender
{
    public int GenderId { get; set; }
    public string Type { get; set; } 

    // Navigation property
    public ICollection<Ad> Ads { get; set; }
}