using _04_StationeryCompany.DataBaseResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _04_StationeryCompany.MVVM.ViewModelResponseDB
{
    public class VM_HistoryOfSells : VM_Base
    {
        private M_HistoryOfSells model;
        public VM_HistoryOfSells(M_HistoryOfSells model) { this.model = model; }

        public int Id
        {
            get { return model.Id; }
            set
            {
                model.Id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        public int? ItemId
        {
            get { return model.ItemId; }
            set
            {
                model.ItemId = value;
                OnPropertyChanged(nameof(ItemId));
            }
        }

        public int? ManagerID
        {
            get { return model.ManagerID; }
            set
            {
                model.ManagerID = value;
                OnPropertyChanged(nameof(ManagerID));
            }
        }

        public int? BuyersCompany
        {
            get { return model.BuyersCompany; }
            set
            {
                model.BuyersCompany = value;
                OnPropertyChanged(nameof(BuyersCompany));
            }
        }

        public int Count
        {
            get { return model.Count; }
            set
            {
                model.Count = value;
                OnPropertyChanged(nameof(Count));
            }
        }

        public DateTime Date
        {
            get { return model.Date; }
            set
            {
                model.Date = value;
                OnPropertyChanged(nameof(Date));
            }
        }

        public float AdditionInformation
        {
            get { return model.AdditionInformation; }
            set
            {
                model.AdditionInformation = value;
                OnPropertyChanged(nameof(AdditionInformation));
            }
        }
    }

}
