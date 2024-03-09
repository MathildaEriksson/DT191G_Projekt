/* Mathilda Eriksson, DT191G, VT24 */
namespace EquiMarketApp.Models;
using System.ComponentModel.DataAnnotations;

public class Gender
{
    public int GenderId { get; set; }
    
    [Required]
    public string? Type { get; set; } 

    // Navigation property
    public ICollection<Ad>? Ads { get; set; }
}