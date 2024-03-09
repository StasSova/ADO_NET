using DataBaseModels;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DataBase;

public class DbCntx : DbContext
{
    public DbSet<M_Employee> Employees { get; set; }
    public DbSet<M_Position> Positions { get; set; }

    public DbCntx(DbContextOptions<DbCntx> options) : base(options)
    {
        // Ensure that the database exists and initialize data if needed
        //Database.EnsureDeleted();
        if (Database.EnsureCreated())
        {
            // Add positions
            string[] positionNames = { "Software Engineer", "Data Scientist", "Project Manager", "UI/UX Designer", "QA Engineer", "System Administrator", "DevOps Engineer", "Business Analyst", "Technical Writer", "Network Engineer" };

            foreach (var positionName in positionNames)
            {
                Positions?.Add(new M_Position { PositionName = positionName });
            }
            SaveChanges();

            // Add employees
            for (int i = 1; i <= 50; i++)
            {
                string name = GetRandomFirstName();
                string surname = GetRandomLastName();

                // For simplicity, let's assign each employee to a random position
                int randomPositionId = new Random().Next(1, 11); // Random position ID between 1 and 10
                var position = Positions?.Find(randomPositionId);

                Employees?.Add(new M_Employee() 
                {
                    Name = name,
                    Surname = surname,
                    Position = position!,
                });
            }
            SaveChanges();
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure Employee entity
        modelBuilder.Entity<M_Employee>()
            .HasKey(e => e.Id);

        modelBuilder.Entity<M_Employee>()
            .Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(30);

        modelBuilder.Entity<M_Employee>()
            .Property(e => e.Surname)
            .IsRequired()
            .HasMaxLength(30);

        modelBuilder.Entity<M_Employee>()
            .HasOne(e => e.Position)
            .WithMany(p => p.Employees)
            .HasForeignKey(e => e.PositionId);

        // Configure Position entity
        modelBuilder.Entity<M_Position>()
            .HasKey(p => p.Id);

        modelBuilder.Entity<M_Position>()
            .Property(p => p.PositionName)
            .IsRequired()
            .HasMaxLength(30);

        modelBuilder.Entity<M_Position>()
            .HasIndex(p => p.PositionName)
            .IsUnique();
    }

    private string GetRandomFirstName()
    {
        // You can replace this with your logic to get real first names
        string[] firstNames = { "John", "Jane", "Alice", "Bob", "Mary", "Michael", "Emma", "James", "Sophia", "William" };
        return firstNames[new Random().Next(firstNames.Length)];
    }
    private string GetRandomLastName()
    {
        // You can replace this with your logic to get real last names
        string[] lastNames = { "Smith", "Johnson", "Williams", "Brown", "Jones", "Garcia", "Miller", "Davis", "Martinez", "Taylor" };
        return lastNames[new Random().Next(lastNames.Length)];
    }
}
