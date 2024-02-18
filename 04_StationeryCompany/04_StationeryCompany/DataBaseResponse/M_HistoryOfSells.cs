using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _04_StationeryCompany.DataBaseResponse
{
    public class M_HistoryOfSells
    {
        public int Id;
        public int? ItemId;
        public int? ManagerID;
        public int? BuyersCompany;
        public int Count;
        public DateTime Date;
        public float AdditionInformation;
    }
}
