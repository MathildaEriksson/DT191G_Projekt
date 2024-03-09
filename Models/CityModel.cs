/* Mathilda Eriksson, DT191G, VT24 */
namespace EquiMarketApp.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class City
{
    [Key]
    public int CityId { get; set; }

    [Required]
    [StringLength(100)]
    public string? Name { get; set; }

    // Foreign Key
    public int CountyId { get; set; }

    // Navigation property
    [ForeignKey("CountyId")]
    public County? County { get; set; }
}