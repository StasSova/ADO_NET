using _04_StationeryCompany.DataBaseResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _04_StationeryCompany.MVVM.ViewModelResponseDB
{
    public class VM_Manager : VM_Base
    {
        private M_Manager model;
        public VM_Manager(M_Manager model) { this.model = model; }

        public int Id
        {
            get { return model.Id; }
            set
            {
                model.Id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        public string FName
        {
            get { return model.FName; }
            set
            {
                model.FName = value;
                OnPropertyChanged(nameof(FName));
            }
        }

        public string LName
        {
            get { return model.LName; }
            set
            {
                model.LName = value;
                OnPropertyChanged(nameof(LName));
            }
        }
        public override string ToString()
        {
            return $"{LName[0]}. {FName}";
        }
    }

}
