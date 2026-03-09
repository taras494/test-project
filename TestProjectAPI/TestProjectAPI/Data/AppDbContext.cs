using Microsoft.EntityFrameworkCore;
using TestProjectAPI.Models;

namespace TestProjectAPI.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Article> Articles => Set<Article>();
    public DbSet<Product> Products => Set<Product>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Article>(entity =>
        {
            entity.HasKey(a => a.Id);
            entity.Property(a => a.Title).IsRequired().HasMaxLength(500);
            entity.Property(a => a.Text).IsRequired();
            entity.Property(a => a.Tag).HasMaxLength(200);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Title).IsRequired().HasMaxLength(500);
            entity.Property(p => p.Price).HasColumnType("decimal(18,2)");
            entity.Property(p => p.Category).HasMaxLength(200);
            entity.Property(p => p.Image).HasMaxLength(1000);
            entity.OwnsOne(p => p.Rating, r =>
            {
                r.Property(x => x.Rate).HasColumnName("Rating_Rate");
                r.Property(x => x.Count).HasColumnName("Rating_Count");
            });
        });
    }
}
