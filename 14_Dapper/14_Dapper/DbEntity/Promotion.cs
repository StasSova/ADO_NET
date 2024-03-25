using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _14_Dapper.DbEntity
{
    public class Promotion
    {
        public int Id { get; set; }
        public int CountrieId { get; set; }
        public int ProductSectionId { get; set; }
        public float Discount { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
