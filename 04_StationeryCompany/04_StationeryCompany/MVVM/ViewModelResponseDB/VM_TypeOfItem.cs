using _04_StationeryCompany.DataBaseResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _04_StationeryCompany.MVVM.ViewModelResponseDB
{
    public class VM_TypeOfItem : VM_Base
    {
        private M_TypeOfItem model;
        public VM_TypeOfItem(M_TypeOfItem model) { this.model = model; }
        public int Id
        {
            get { return model.Id; }
            set
            {
                model.Id = value;
                OnPropertyChanged(nameof(Id));
            }
        }
        public string Type
        {
            get { return model.Type; }
            set
            {
                model.Type = value;
                OnPropertyChanged(nameof(Type));
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

        public override string ToString()
        {
            return $"{Type}";
        }
    }

}
