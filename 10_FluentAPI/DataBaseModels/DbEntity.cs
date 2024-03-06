using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseModels;

public abstract class DbEntity: ICloneable
{
    public int Id { get; set; }

    public object Clone()
    {
        throw new NotImplementedException();
    }
}
