/* Mathilda Eriksson, DT191G, VT24 
    utefter Mattias Dahlgrens genomgång 2024-03-05*/
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EquiMarketApp;

public class ApplicationUser : IdentityUser
{
    [StringLength(100, ErrorMessage = "Namnet får inte vara längre än 100 tecken.")]
    public string? Name { get; set;}
}