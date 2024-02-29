using _08_CodeFirst_DLL_Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _08_CodeFirst_DLL.Response
{
    public partial class VM_PartOfTheWorld : VM_Base
    {
        [ObservableProperty] PartOfTheWorld model;
        public VM_PartOfTheWorld() { this.Model = new(); }
        public VM_PartOfTheWorld(PartOfTheWorld partOfTheWorld)
        {
            this.Model = partOfTheWorld;
        }
        public int Id
        {
            get => Model.Id;
            set { Model.Id = value; OnPropertyChanged(nameof(Id)); }
        }
        public string Name
        {
            get => Model.Name;
            set { Model.Name = value; OnPropertyChanged(nameof(Name)); }
        }
        public virtual ICollection<Country>? Countrys
        {
            get => Model.Countrys;
            set { Model.Countrys = value; OnPropertyChanged(nameof(Countrys)); }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
