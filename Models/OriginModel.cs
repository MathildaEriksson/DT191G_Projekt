/* Mathilda Eriksson, DT191G, VT24 */
namespace EquiMarketApp.Models;
using System.ComponentModel.DataAnnotations;

public class Origin
{
    public int OriginId { get; set; }

    [Required]
    public string? Country { get; set; }

    // Navigation property 
    public ICollection<Ad>? Ads { get; set; }
}