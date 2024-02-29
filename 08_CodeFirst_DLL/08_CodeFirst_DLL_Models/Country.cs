using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _08_CodeFirst_DLL_Models
{
    public class Country
    {
        public Country() 
        {
            Name = "Country Name";
            Capital = "Capital of the country";
        }
        public Country(Country country) 
        {
            Id = country.Id;
            Name = country.Name;
            Capital = country.Capital;
            NumberOfInhabitants = country.NumberOfInhabitants;
            CountryArea = country.CountryArea;
            PartOfTheWorld = country.PartOfTheWorld;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Capital { get; set; }
        public int? NumberOfInhabitants {  get; set; }
        public double? CountryArea { get; set; }
        public virtual PartOfTheWorld? PartOfTheWorld { get; set; }
    }
}
