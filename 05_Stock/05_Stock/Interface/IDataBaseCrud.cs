using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _05_Stock.Interface
{
    public interface IDataBaseCrud
    {
        string ConnectionString { get; set; }
    }
}
