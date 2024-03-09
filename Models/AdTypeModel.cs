/* Mathilda Eriksson, DT191G, VT24 */
namespace EquiMarketApp.Models;
using System.ComponentModel.DataAnnotations;

public class AdType
{
    [Key]
    public int AdTypeId { get; set; }

    [Required]
    [StringLength(50, ErrorMessage = "Annonstypens namn får inte vara längre än 50 tecken.")]
    public string? Name { get; set; } 

    // Navigation property
    public ICollection<Ad>? Ads { get; set; }
}