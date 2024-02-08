using _02_ConnectionToDB_RequestToDB.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_ConnectionToDB_RequestToDB.MVVM
{
    public class VM_Product:VM_Base
    {
        private Product model;
        public VM_Product(Product product) { model = product; }
        public int? ID 
        { 
            get { return model.ID; }
            set
            {
                model.ID = value;
                OnPropertyChanged(nameof(ID));
            }
        }
        public string? Name
        {
            get { return model.Name; }
            set
            {
                model.Name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public int? TypeID
        {
            get { return model.TypeID; }
            set
            {
                model.TypeID = value;
                OnPropertyChanged(nameof(TypeID));
            }
        }

        public string? Color
        {
            get { return model.Color; }
            set
            {
                model.Color = value;
                OnPropertyChanged(nameof(Color));
            }
        }

        public int? Calories
        {
            get { return model.Calories; }
            set
            {
                model.Calories = value;
                OnPropertyChanged(nameof(Calories));
            }
        }
    }
}
