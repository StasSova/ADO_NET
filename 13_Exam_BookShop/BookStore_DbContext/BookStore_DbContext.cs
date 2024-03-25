using BookStore_DbContext.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore_DbContext;

public class BookStore_DbContext : DbContext
{
    public DbSet<M_Author> Authors { get; set; }
    public DbSet<M_Book> Books { get; set; }
    public DbSet<M_BookForSale> BooksForSale { get; set; }
    public DbSet<M_BookRelationship> BookRelationships { get; set; }
    public DbSet<M_Ganre> Genres { get; set; }
    public DbSet<M_PublishingHouse> PublishingHouses { get; set; }
    public DbSet<M_RelationshipType> RelationshipTypes { get; set; }
    public DbSet<M_ShoppingCart> ShoppingCarts { get; set; }
    public DbSet<M_WarehouseItem> WarehouseItems { get; set; }
    public DbSet<M_User> Users { get; set; }



    static DbContextOptions<BookStore_DbContext> options;
    static BookStore_DbContext()
    {
        var builder = new ConfigurationBuilder();
        builder.SetBasePath(Directory.GetCurrentDirectory());
        builder.AddJsonFile("appsettings.json");
        var config = builder.Build();
        string connectionString = config.GetConnectionString("DefaultConnection");

        var optionsBulder = new DbContextOptionsBuilder<BookStore_DbContext>();

        options = optionsBulder.UseLazyLoadingProxies().UseSqlServer(connectionString).Options;
    }
    public BookStore_DbContext() : base(options)
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

        modelBuilder.Entity<M_Ganre>()
            .HasIndex(g => g.Genre)
            .IsUnique();

        modelBuilder.Entity<M_User>()
            .HasIndex(u => u.Username)
            .IsUnique();



        modelBuilder.Entity<M_BookRelationship>()
            .HasKey(br => new { br.FirstBookId, br.SecondBookId });

        modelBuilder.Entity<M_BookRelationship>()
            .HasOne(br => br.FirstBook)
            .WithMany(b => b.RelatedBooks)
            .HasForeignKey(br => br.FirstBookId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<M_BookRelationship>()
            .HasOne(br => br.SecondBook)
            .WithMany()
            .HasForeignKey(br => br.SecondBookId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<M_BookForSale>()
            .HasOne(bfs => bfs.WarehouseItem)
            .WithOne(wi => wi.BookForSale)
            .HasForeignKey<M_WarehouseItem>(wi => wi.BookForSaleId);


        modelBuilder.Entity<M_BookForSale>()
            .Property(bfs => bfs.CostPrice)
            .HasColumnType("decimal(18, 2)");

        modelBuilder.Entity<M_BookForSale>()
            .Property(bfs => bfs.SellingPrice)
            .HasColumnType("decimal(18, 2)");

        modelBuilder.Entity<M_BookForSale>()
            .Property(bfs => bfs.Discount)
            .HasColumnType("decimal(3, 2)")
            .HasDefaultValue(0);

        modelBuilder.Entity<M_BookForSale>()
            .HasMany(bfs => bfs.ShoppingCarts)
            .WithMany(sc => sc.Books)
            .UsingEntity(j => j.ToTable("ShoppingCartBooks"));

        modelBuilder.Entity<M_User>()
            .HasMany(u => u.ShoppingCarts)
            .WithOne(sc => sc.User)
            .HasForeignKey(sc => sc.UserId);

        modelBuilder.Entity<M_WarehouseItem>()
            .HasOne(wi => wi.BookForSale)
            .WithOne(bfs => bfs.WarehouseItem)
            .HasForeignKey<M_WarehouseItem>(wi => wi.BookForSaleId);
    }
}
