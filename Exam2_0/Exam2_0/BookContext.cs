using BookContext.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookContext;

public class BookContext : DbContext
{
    public DbSet<M_Author> Authors { get; set; }
    public DbSet<M_Book> Books { get; set; }
    public DbSet<M_Genre> Genres { get; set; }
    public DbSet<M_PublishingHouse> PublishingHouses { get; set; }
    public DbSet<M_ShoppingCart> ShoppingCarts { get; set; }
    public DbSet<M_User> Users { get; set; }


    static DbContextOptions<BookContext> options;
    static BookContext()
    {
        var builder = new ConfigurationBuilder();
        builder.SetBasePath(Directory.GetCurrentDirectory());
        builder.AddJsonFile("appsettings.json");
        var config = builder.Build();
        string connectionString = config.GetConnectionString("DefaultConnection");

        var optionsBulder = new DbContextOptionsBuilder<BookContext>();

        options = optionsBulder.UseLazyLoadingProxies().UseSqlServer(connectionString).Options;
    }
    public BookContext() : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<M_Book>()
            .HasOne(b => b.PublishingHouse)
            .WithMany(ph => ph.Books)
            .HasForeignKey(b => b.PublishingHouseId);

        modelBuilder.Entity<M_Book>()
            .HasOne(b => b.Author)
            .WithMany(a => a.Books)
            .HasForeignKey(b => b.AuthorId);

        modelBuilder.Entity<M_Book>()
            .HasIndex(b => b.Title)
            .IsUnique();

        modelBuilder.Entity<M_Book>()
            .HasIndex(b => b.NumberOfPage);

        modelBuilder.Entity<M_Book>()
            .HasIndex(b => b.DateOfPress);

        modelBuilder.Entity<M_Book>()
            .UseTpcMappingStrategy();

        modelBuilder.Entity<M_Genre>()
            .HasIndex(g => g.Genre)
            .IsUnique();

        modelBuilder.Entity<M_User>()
            .HasIndex(u => u.Username)
            .IsUnique();


        modelBuilder.Entity<M_Book>()
            .Property(bfs => bfs.CostPrice)
            .HasColumnType("decimal(18, 2)");

        modelBuilder.Entity<M_Book>()
            .Property(bfs => bfs.SellingPrice)
            .HasColumnType("decimal(18, 2)");

        modelBuilder.Entity<M_Book>()
            .Property(bfs => bfs.Discount)
            .HasColumnType("decimal(4, 2)")
            .HasDefaultValue(0);

        modelBuilder.Entity<M_Book>()
            .HasMany(bfs => bfs.ShoppingCarts)
            .WithMany(sc => sc.Books)
            .UsingEntity(j => j.ToTable("ShoppingCartBooks"));

        modelBuilder.Entity<M_User>()
            .HasMany(u => u.ShoppingCarts)
            .WithOne(sc => sc.User)
            .HasForeignKey(sc => sc.UserId);
    }
}
