/* Mathilda Eriksson, DT191G, VT24 */
namespace EquiMarketApp.Models;
using System.ComponentModel.DataAnnotations;

public class County
{
    [Key]
    public int CountyId { get; set; }

    [Required]
    [StringLength(100)]
    public string? Name { get; set; }

    // A County has many cities
    public ICollection<City>? Cities { get; set; }
}