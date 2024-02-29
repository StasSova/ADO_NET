using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _08_CodeFirst_DLL_Models
{
    public class PartOfTheWorld
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Country>? Countrys { get; set; }
    }
}
