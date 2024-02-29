using _08_CodeFirst_DLL_Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _08_CodeFirst_DLL_Context
{
    public class CountryDbContext : DbContext
    {
        static DbContextOptions<CountryDbContext> _options;
        static CountryDbContext()
        {
            string connectionString = LoadConnectionString();
            var optionsBuilder = new DbContextOptionsBuilder<CountryDbContext>();
            _options = optionsBuilder.UseSqlServer(connectionString).Options;
        }
        private static string LoadConnectionString()
        {
            try
            {
                string jsonFilePath = "appsettings.json";

                if (jsonFilePath != null && File.Exists(jsonFilePath))
                {
                    string jsonContent = File.ReadAllText(jsonFilePath);
                    JObject jsonObject = JObject.Parse(jsonContent);
                    return jsonObject["ConnectionStrings"]["DefaultConnection"].ToString();
                }
                else
                {
                    throw new FileNotFoundException("JSON file not found or invalid.");
                }
            }
            catch
            {
                throw;
            }
        }
        public CountryDbContext(): base(_options)
        {
            // если ранее не была создана
            if (Database.EnsureCreated())
            {
                var partOfTheWorlds = new List<PartOfTheWorld>
                {
                    new PartOfTheWorld { Name = "Europe" },
                    new PartOfTheWorld { Name = "Asia" },
                    new PartOfTheWorld { Name = "North America" },
                    new PartOfTheWorld { Name = "South America" },
                    new PartOfTheWorld { Name = "Africa" },
                    new PartOfTheWorld { Name = "Oceania" },
                    new PartOfTheWorld { Name = "Eurasia" }
                };
                var countries = new List<Country>
                {
                    new Country { Name = "Canada", Capital = "Ottawa", NumberOfInhabitants = 38005238, CountryArea = 9984670, PartOfTheWorld = partOfTheWorlds.FirstOrDefault(p => p.Name == "North America") },
                    new Country { Name = "China", Capital = "Beijing", NumberOfInhabitants = 1403500365, CountryArea = 9596960, PartOfTheWorld = partOfTheWorlds.FirstOrDefault(p => p.Name == "Asia") },
                    new Country { Name = "USA", Capital = "Washington, D.C.", NumberOfInhabitants = 331002651, CountryArea = 9833520, PartOfTheWorld = partOfTheWorlds.FirstOrDefault(p => p.Name == "North America") },
                    new Country { Name = "Brazil", Capital = "Brasília", NumberOfInhabitants = 212559417, CountryArea = 8515767, PartOfTheWorld = partOfTheWorlds.FirstOrDefault(p => p.Name == "South America") },
                    new Country { Name = "Australia", Capital = "Canberra", NumberOfInhabitants = 25499884, CountryArea = 7692024, PartOfTheWorld = partOfTheWorlds.FirstOrDefault(p => p.Name == "Oceania") },
                    new Country { Name = "India", Capital = "New Delhi", NumberOfInhabitants = 1380004385, CountryArea = 3287263, PartOfTheWorld = partOfTheWorlds.FirstOrDefault(p => p.Name == "Asia") },
                    new Country { Name = "Argentina", Capital = "Buenos Aires", NumberOfInhabitants = 45195777, CountryArea = 2780400, PartOfTheWorld = partOfTheWorlds.FirstOrDefault(p => p.Name == "South America") },
                    new Country { Name = "Kazakhstan", Capital = "Nur-Sultan", NumberOfInhabitants = 18776707, CountryArea = 2724900, PartOfTheWorld = partOfTheWorlds.FirstOrDefault(p => p.Name == "Eurasia") },
                    new Country { Name = "France", Capital = "Paris", NumberOfInhabitants = 65273511, CountryArea = 551695, PartOfTheWorld = partOfTheWorlds.FirstOrDefault(p => p.Name == "Europe") },
                    new Country { Name = "Germany", Capital = "Berlin", NumberOfInhabitants = 83783942, CountryArea = 357386, PartOfTheWorld = partOfTheWorlds.FirstOrDefault(p => p.Name == "Europe") },
                    new Country { Name = "Italy", Capital = "Rome", NumberOfInhabitants = 60461826, CountryArea = 301340, PartOfTheWorld = partOfTheWorlds.FirstOrDefault(p => p.Name == "Europe") },
                    new Country { Name = "Spain", Capital = "Madrid", NumberOfInhabitants = 46754778, CountryArea = 505990, PartOfTheWorld = partOfTheWorlds.FirstOrDefault(p => p.Name == "Europe") },
                    new Country { Name = "United Kingdom", Capital = "London", NumberOfInhabitants = 67886011, CountryArea = 242495, PartOfTheWorld = partOfTheWorlds.FirstOrDefault(p => p.Name == "Europe") },
                    new Country { Name = "Algeria", Capital = "Algiers", NumberOfInhabitants = 43851044, CountryArea = 2381741, PartOfTheWorld = partOfTheWorlds.FirstOrDefault(p => p.Name == "Africa") },
                    new Country { Name = "Democratic Republic of the Congo", Capital = "Kinshasa", NumberOfInhabitants = 89561403, CountryArea = 2344858, PartOfTheWorld = partOfTheWorlds.FirstOrDefault(p => p.Name == "Africa") },
                    new Country { Name = "Greenland", Capital = "Nuuk", NumberOfInhabitants = 56770, CountryArea = 2166086, PartOfTheWorld = partOfTheWorlds.FirstOrDefault(p => p.Name == "North America") },
                    new Country { Name = "Saudi Arabia", Capital = "Riyadh", NumberOfInhabitants = 34813871, CountryArea = 2149690, PartOfTheWorld = partOfTheWorlds.FirstOrDefault(p => p.Name == "Asia") },
                    new Country { Name = "Mexico", Capital = "Mexico City", NumberOfInhabitants = 128932753, CountryArea = 1964375, PartOfTheWorld = partOfTheWorlds.FirstOrDefault(p => p.Name == "North America") },
                    new Country { Name = "Indonesia", Capital = "Jakarta", NumberOfInhabitants = 273523615, CountryArea = 1904569, PartOfTheWorld = partOfTheWorlds.FirstOrDefault(p => p.Name == "Asia") },
                    new Country { Name = "Sudan", Capital = "Khartoum", NumberOfInhabitants = 43849260, CountryArea = 1861484, PartOfTheWorld = partOfTheWorlds.FirstOrDefault(p => p.Name == "Africa") },
                    new Country { Name = "Libya", Capital = "Tripoli", NumberOfInhabitants = 6871292, CountryArea = 1759540, PartOfTheWorld = partOfTheWorlds.FirstOrDefault(p => p.Name == "Africa") },
                    new Country { Name = "Iran", Capital = "Tehran", NumberOfInhabitants = 83992949, CountryArea = 1648195, PartOfTheWorld = partOfTheWorlds.FirstOrDefault(p => p.Name == "Asia") },
                    new Country { Name = "Mongolia", Capital = "Ulaanbaatar", NumberOfInhabitants = 3278290, CountryArea = 1564116, PartOfTheWorld = partOfTheWorlds.FirstOrDefault(p => p.Name == "Asia") },
                    new Country { Name = "Peru", Capital = "Lima", NumberOfInhabitants = 32971854, CountryArea = 1285216, PartOfTheWorld = partOfTheWorlds.FirstOrDefault(p => p.Name == "South America") }
                };

                PartsOfTheWorld?.AddRange(partOfTheWorlds);
                Countries?.AddRange(countries);

                SaveChanges();
            }
        }
        public DbSet<Country> Countries { get; set; }
        public DbSet<PartOfTheWorld> PartsOfTheWorld { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // метод UseLazyLoadingProxies() делает доступной ленивую загрузку.
            optionsBuilder.UseLazyLoadingProxies();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Настраиваем каскадное удаление для PartsOfTheWorld
            modelBuilder.Entity<PartOfTheWorld>()
                .HasMany(p => p.Countrys) // Указываем, что у PartOfTheWorld есть много стран
                .WithOne(c => c.PartOfTheWorld) // Указываем, что каждая страна принадлежит одной PartOfTheWorld
                .OnDelete(DeleteBehavior.Cascade); // Устанавливаем каскадное удаление
        }
    }
}
