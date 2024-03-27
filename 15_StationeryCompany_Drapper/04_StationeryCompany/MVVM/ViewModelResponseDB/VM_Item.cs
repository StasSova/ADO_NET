using _04_StationeryCompany.DataBaseResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _04_StationeryCompany.MVVM.ViewModelResponseDB
{
    public class VM_Item : VM_Base
    {
        private M_Item model;
        public M_Item Model
        {
            get { return model; }
            set {  model = value; OnPropertyChanged(nameof(Model)); }
        }
        public VM_Item(M_Item model) { this.model = model; }
        // Конструктор копирования
        public VM_Item(VM_Item other)
        {
            this.model = new M_Item
            {
                Id = other.Id,
                Name = other.Name,
                Type_ID = other.Type_ID,
                Count = other.Count,
                CostPrice = other.CostPrice,
                Price = other.Price
            };
        }

        public int Id
        {
            get { return model.Id; }
            set
            {
                model.Id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        public string Name
        {
            get { return model.Name; }
            set
            {
                model.Name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public int Type_ID
        {
            get { return model.Type_ID; }
            set
            {
                model.Type_ID = value;
                OnPropertyChanged(nameof(Type_ID));
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

        public decimal CostPrice
        {
            get { return model.CostPrice; }
            set
            {
                model.CostPrice = value;
                OnPropertyChanged(nameof(CostPrice));
            }
        }

        public decimal Price
        {
            get { return model.Price; }
            set
            {
                model.Price = value;
                OnPropertyChanged(nameof(Price));
            }
        }
    }

}
