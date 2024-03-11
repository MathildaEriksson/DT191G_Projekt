/* Mathilda Eriksson, DT191G, VT24 */
namespace EquiMarketApp.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Image
{
    [Key]
    public int ImageId { get; set; }

    public string? ImagePath { get; set; }

    [ForeignKey("Ad")]
    public int AdId { get; set; }

    public Ad? Ad { get; set; }
}
