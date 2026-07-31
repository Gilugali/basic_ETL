


using Microsoft.EntityFrameworkCore;

public class Context : DbContext {


  public DbSet<PriceData> Prices {get; set;}

  // configuring d

  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    optionsBuilder.UseSqlite("Data Source=prices.db");
  }

  // setting primary composite key
  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<PriceData>().HasKey(p => new {p.Symbol, p.Date});
  }
}
