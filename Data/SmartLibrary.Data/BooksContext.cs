using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SmartLibrary.Core.Models;

namespace SmartLibrary.Data;

public class BooksContext : DbContext
{
    public DbSet<SavedBook> SavedBooks { get; set; }

    public BooksContext(DbContextOptions<BooksContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    public BooksContext()
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Filename=Books.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<SavedBook>()
            .HasKey(b => b.Id);

        modelBuilder.Entity<SavedBook>()
            .Property(b => b.Location)
            .HasConversion(v => JsonConvert.SerializeObject(v), v => JsonConvert.DeserializeObject<Location>(v));
    }
}
