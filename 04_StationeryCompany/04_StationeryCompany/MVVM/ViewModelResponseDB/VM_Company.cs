using _04_StationeryCompany.DataBaseResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _04_StationeryCompany.MVVM.ViewModelResponseDB
{
    public class VM_Company : VM_Base
    {
        private M_Company model;
        public VM_Company(M_Company model) { this.model = model; }

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
        public override string ToString() { return $"{Name}"; }
    }

}
