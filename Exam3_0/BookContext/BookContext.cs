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
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<M_Book>()
            .Property(b => b.Image)
            .HasDefaultValueSql("'https://images.unsplash.com/photo-1541963463532-d68292c34b19?q=80&w=1000&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8NHx8Ym9va3xlbnwwfHwwfHx8MA%3D%3D'");
    }
}
