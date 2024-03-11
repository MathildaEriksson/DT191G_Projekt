using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using EquiMarketApp.Models;

namespace EquiMarketApp.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }

    public DbSet<Ad> Ads { get; set; }
    public DbSet<Breed> Breeds { get; set; }
    public DbSet<Origin> Origins { get; set; }
    public DbSet<AdType> AdTypes { get; set; }
    public DbSet<County> Counties { get; set; }
    public DbSet<City> Cities { get; set; }
    public DbSet<Image> Images { get; set; }

}
