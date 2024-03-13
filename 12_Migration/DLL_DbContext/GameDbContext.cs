using DLL_Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL_DbContext;

public class GameDbContext: DbContext
{
    public DbSet<M_Game> Games { get; set; }
    public DbSet<M_GameStudio> Studios { get; set; }
    public DbSet<M_GameStyle> Styles { get; set; }
    
    // added new field
    public DbSet<M_FeaturesOfGame> FeaturesOfGames { get; set; }

    static DbContextOptions<GameDbContext> options;
    static GameDbContext()
    {
        var builder = new ConfigurationBuilder();
        builder.SetBasePath(Directory.GetCurrentDirectory());
        builder.AddJsonFile("appsettings.json");
        var config = builder.Build();
        string connectionString = config.GetConnectionString("DefaultConnection");

        var optionsBulder = new DbContextOptionsBuilder<GameDbContext>();
        options = optionsBulder.UseLazyLoadingProxies().UseSqlServer(connectionString).Options;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<M_Game>()
            .HasMany(g => g.Styles)
            .WithMany(s => s.Games)
            .UsingEntity(j => j.ToTable("GameStyles"));

        modelBuilder.Entity<M_Game>()
            .HasMany(g => g.Studios)
            .WithMany(s => s.Games)
            .UsingEntity(j => j.ToTable("GameStudios"));

        modelBuilder.Entity<M_Game>()
            .HasMany(g => g.FeaturesOfGame)
            .WithMany(f => f.Games)
            .UsingEntity(j => j.ToTable("GameFeatures"));

        modelBuilder.Entity<M_GameStyle>()
            .HasKey(gs => gs.Id);

        modelBuilder.Entity<M_GameStudio>()
            .HasKey(gs => gs.Id);

        // Ограничение на уникальность названия жанра
        modelBuilder.Entity<M_GameStyle>()
            .HasIndex(gs => gs.Name)
            .IsUnique();

        // Ограничение на минимальную длину названия игры
        modelBuilder.Entity<M_Game>()
            .Property(g => g.Name)
            .IsRequired();

        // Ограничение на минимальную длину названия студии
        modelBuilder.Entity<M_GameStudio>()
            .Property(s => s.Name)
            .IsRequired();


        modelBuilder.Entity<M_FeaturesOfGame>()
            .HasKey(f => f.Id);

        modelBuilder.Entity<M_FeaturesOfGame>()
            .HasIndex(f => f.Name)
            .IsUnique();
    }

    public GameDbContext() : base(options)
    {

    }
}
