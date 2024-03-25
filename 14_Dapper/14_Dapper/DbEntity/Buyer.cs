using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _14_Dapper.DbEntity
{
    public class Buyer
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public char Gender { get; set; }
        public string Email { get; set; }
        public int CityId { get; set; }
        public override string ToString()
        {
            return $"{FullName} ({Email})";
        }
    }
}
