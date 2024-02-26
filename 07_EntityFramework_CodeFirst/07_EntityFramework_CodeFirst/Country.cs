using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _07_EntityFramework_CodeFirst
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Capital { get; set; }
        public int NumberOfInhabitants { get; set; }
        public double CountryArea { get; set; }
        public virtual PartOfTheWorld? PartOfTheWorld { get; set; }
    }
    public class PartOfTheWorld 
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Country>? Countrys { get; set; }
    }
}
