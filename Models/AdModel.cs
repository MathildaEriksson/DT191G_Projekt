/* Mathilda Eriksson, DT191G, VT24 */
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace EquiMarketApp.Models;

public class Ad
{
    public Ad()
    {
        // Sets CreatedAt to current date and time when the Ad is created
        CreatedAt = DateTime.UtcNow;
    }

    [Key]
    public int AdId { get; set; }

    public string? UserId { get; set; }

    [Required(ErrorMessage = "Titel är obligatoriskt")]
    [StringLength(100, ErrorMessage = "Titeln får inte vara längre än 100 tecken")]
    [Display(Name = "Titel")]
    public string? Title { get; set; }

    [Required(ErrorMessage = "Beskrivning är obligatoriskt")]
    [StringLength(3000, ErrorMessage = "Beskrivningen får inte vara längre än 3000 tecken")]
    [Display(Name = "Beskrivning")]
    public string? Description { get; set; }

    [Required(ErrorMessage = "Pris är obligatoriskt")]
    [Range(1, double.MaxValue, ErrorMessage = "Priset måste vara högre än 0 kr")]
    [Display(Name = "Pris")]
    public decimal Price { get; set; }

    [Display(Name = "Skapad")]
    public DateTime CreatedAt { get; set; }

    [Required(ErrorMessage = "Födelseår är obligatoriskt")]
    [Range(1970, 2100, ErrorMessage = "Ogiltigt födelseår, format: (YYYY)")]
    [Display(Name = "Födelseår")]
    public int BirthYear { get; set; }

    [Required(ErrorMessage = "Mankhöjd är obligatoriskt")]
    [Range(1, 250, ErrorMessage = "Ogiltig höjd")]
    [Display(Name = "Mankhöjd")]
    public int Height { get; set; }

    [Required(ErrorMessage = "Namn är obligatoriskt")]
    [StringLength(100, ErrorMessage = "Namnet får inte vara längre än 100 tecken")]
    [Display(Name = "Namn")]
    public string? Name { get; set; }

    [Display(Name = "Kön")]
    public Gender Gender { get; set; }

    [Display(Name = "Är godkänd")]
    public bool IsApproved { get; set; } = false;

    [ForeignKey("Breed")]
    [Display(Name = "Ras")]
    public int BreedId { get; set; }
    public Breed? Breed { get; set; }

    [ForeignKey("Origin")]
    [Display(Name = "Ursprung")]
    public int OriginId { get; set; }
    public Origin? Origin { get; set; }

    [Required]
    [Display(Name = "Annonstyp")]
    public int AdTypeId { get; set; }
    public AdType? AdType { get; set; }

    [Display(Name = "Plats")]
    public int CityId { get; set; }
    public City? Location { get; set; }

    [ForeignKey("UserId")]
    public ApplicationUser? User { get; set; }

    [Display(Name = "Bilder")]
    public ICollection<Image> Images { get; set; } = new List<Image>();
}