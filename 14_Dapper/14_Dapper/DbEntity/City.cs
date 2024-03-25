using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _14_Dapper.DbEntity
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CountrieId { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
