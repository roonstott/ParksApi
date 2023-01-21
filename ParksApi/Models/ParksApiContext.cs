using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;



namespace ParksApi.Models;

public class ParksApiContext : IdentityDbContext<ApplicationUser>

{
  public DbSet<Park> Parks { get; set; }
  public DbSet<State> States { get; set; }

  public ParksApiContext(DbContextOptions<ParksApiContext> options) : base(options) 
    {      
    }
  protected override void OnModelCreating(ModelBuilder builder)
    {
      builder.Entity<State>()
        .HasData(
          new State { StateId = 1, Name = "Oregon"},
          new State { StateId = 2, Name = "Washington"},
          new State { StateId = 3, Name = "California"}
        );

      builder.Entity<Park>()
        .HasData(
          new Park { ParkId = 1, Name = "Crater Lake", Description = "A high elevation lake in the crater of an exploded volcano", StateId = 1, StateName = "Oregon"},
          new Park { ParkId = 2, Name = "Yosemite", Description = "A forested valley high in the Sierra Nevada mountains, enclosed by towering granite cliffs with cascading waterfalls", StateId = 3, StateName = "California"},
          new Park { ParkId = 3, Name = "Mount Rainier", Description = "A 14,410 active volcano with extensive (but shrinking) glacier fields. Beloved for alpine meadows carpeted in summer wildflowers", StateId = 2, StateName = "Washington"});
    }

}