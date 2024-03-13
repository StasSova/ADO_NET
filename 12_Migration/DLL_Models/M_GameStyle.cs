using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL_Models
{
    public class M_GameStyle: DbEntity
    {
        public M_GameStyle() { }
        public string Name { get; set; }
        public virtual ICollection<M_Game> Games { get; set; }
    }
}
