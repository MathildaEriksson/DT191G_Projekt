namespace EquiMarketApp.Models;
using Microsoft.AspNetCore.Identity;

public class Ad
{
    public int AdId { get; set; }  // Primärnyckel
    public string UserId { get; set; }  // Foreign key till User
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int BirthYear { get; set; }
    public int Height { get; set; }
    public string Name { get; set; }

    // Foreign keys och navigation properties för Breed, Origin, Gender
    public int BreedId { get; set; }
    public Breed Breed { get; set; }
    public int OriginId { get; set; }
    public Origin Origin { get; set; }
    public int GenderId { get; set; }
    public Gender Gender { get; set; }

    // Navigation property till User
    public IdentityUser User { get; set; }
}
