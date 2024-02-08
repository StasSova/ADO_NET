using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_ConnectionToDB_RequestToDB.Response
{
    public class Product
    {
        public int? ID { get; set; }
        public string? Name { get; set; }
        public int? TypeID { get; set; }
        public string? Color { get; set; }
        public int? Calories { get; set; }
    }
}
