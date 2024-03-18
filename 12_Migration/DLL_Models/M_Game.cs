using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL_Models
{
    public class M_Game: DbEntity
    {
        public M_Game() { }
        public string Name { get; set; }
        public DateTime Release {  get; set; }
        public virtual ICollection<M_GameStyle> Styles { get; set; }
        public virtual ICollection<M_GameStudio> Studios { get; set; }


        // added new fields
        public int CopiesSold {  get; set; }
        public virtual ICollection<M_FeaturesOfGame> FeaturesOfGame { get; set; }
    }
}
