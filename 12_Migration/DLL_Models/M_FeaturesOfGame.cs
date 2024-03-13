using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL_Models
{
    // added new model
    public class M_FeaturesOfGame:DbEntity
    {
        public M_FeaturesOfGame() { }
        public string Name { get; set; }
        public virtual ICollection<M_Game> Games { get; set; }
    }
}
