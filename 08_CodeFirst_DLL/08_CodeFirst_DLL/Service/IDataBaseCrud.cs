using _08_CodeFirst_DLL_Context;
using _08_CodeFirst_DLL_Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _08_CodeFirst_DLL.Service
{
    public interface IDataBaseCrud
    {
        CountryDbContext Context { get; set; }
        Task<List<Country>> GetCountry();
        Task<List<PartOfTheWorld>> GetParts();

        Task UpdateCountry(Country oldCountrie, Country newCountrie);
        Task RemoveCountry(Country country);
        Task AddCountry(Country country);


        Task RemovePart(PartOfTheWorld part);
        Task AddPart(PartOfTheWorld part);
        Task UpdatePart(PartOfTheWorld oldCountrie, PartOfTheWorld newCountrie);
    }

    public class DataBaseCrud : IDataBaseCrud
    {
        public DataBaseCrud(CountryDbContext context)
        {
            Context = context;
        }

        public CountryDbContext Context { get; set; }
        public async Task<List<Country>> GetCountry()
        {
            return (from c in Context.Countries select c).ToList<Country>();
        }
        public async Task<List<PartOfTheWorld>> GetParts()
        {
            return (from c in Context.PartsOfTheWorld select c).ToList<PartOfTheWorld>();
        }


        public async Task RemoveCountry(Country country)
        {
            try
            {
                Context.Countries.Remove(country);
                await Context.SaveChangesAsync();
            }
            catch { throw; }
        }
        public async Task UpdateCountry(Country oldCountrie, Country newCountrie)
        {
            try
            {
                Country? s = Context.Countries.FirstOrDefault(x=> x.Id == oldCountrie.Id);
                if (s == null) throw new Exception("Information is absent.");

                s.Name = newCountrie.Name;
                s.Capital = newCountrie.Capital;
                s.NumberOfInhabitants = newCountrie.NumberOfInhabitants;
                s.CountryArea = newCountrie.CountryArea;
                s.PartOfTheWorld = newCountrie.PartOfTheWorld;

                await Context.SaveChangesAsync();
            }
            catch { throw; }
        }
        public async Task AddCountry(Country country)
        {
            try
            {
                Context.Countries.Add(country);
                await Context.SaveChangesAsync();
            }
            catch { }
        }

        public async Task RemovePart(PartOfTheWorld part)
        {
            try
            {
                Context.PartsOfTheWorld.Remove(part);
                await Context.SaveChangesAsync();
            }
            catch { throw; }
        }
        public async Task UpdatePart(PartOfTheWorld oldPart, PartOfTheWorld newPart)
        {
            try
            {
                PartOfTheWorld? s = Context.PartsOfTheWorld.FirstOrDefault(x => x.Id == oldPart.Id);
                if (s == null) throw new Exception("Information is absent.");

                s.Name = newPart.Name;
                s.Countrys = newPart.Countrys;
                await Context.SaveChangesAsync();
            }
            catch { throw; }
        }
        public async Task AddPart(PartOfTheWorld part)
        {
            try
            {
                Context.PartsOfTheWorld.Add(part);
                await Context.SaveChangesAsync();
            }
            catch { }
        }
    }
}
