/* Mathilda Eriksson, DT191G, VT24 */
namespace EquiMarketApp.Models;
using System.ComponentModel.DataAnnotations;

public class Breed
{
    public int BreedId { get; set; }
    
    [Required]
    public string? Name { get; set; }

    // Navigation property
    public ICollection<Ad>? Ads { get; set; }
}