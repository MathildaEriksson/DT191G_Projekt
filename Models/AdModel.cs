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

    [Required]
    public string? UserId { get; set; }

    [Required]
    [StringLength(100, ErrorMessage = "Titeln får inte vara längre än 100 tecken.")]
    public string? Title { get; set; }

    [Required]
    [StringLength(3000, ErrorMessage = "Beskrivningen får inte vara längre än 3000 tecken.")]
    public string? Description { get; set; }

    [Required]
    [Range(0, double.MaxValue, ErrorMessage = "Priset måste vara ett positivt tal.")]
    public decimal Price { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; }

    [Required]
    [Range(1900, 2100, ErrorMessage = "Ogiltigt födelseår.")]
    public int BirthYear { get; set; }

    [Required]
    [Range(0, 300, ErrorMessage = "Ogiltig höjd.")]
    public int Height { get; set; }

    [Required]
    [StringLength(100, ErrorMessage = "Namnet får inte vara längre än 100 tecken.")]
    public string? Name { get; set; }

    public bool IsApproved { get; set; } = false;

    [ForeignKey("Breed")]
    public int BreedId { get; set; }
    public Breed? Breed { get; set; }

    [ForeignKey("Origin")]
    public int OriginId { get; set; }
    public Origin? Origin { get; set; }

    [ForeignKey("Gender")]
    public int GenderId { get; set; }
    public Gender? Gender { get; set; }

    public IdentityUser? User { get; set; }
}
