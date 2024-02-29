using _08_CodeFirst_DLL.Response;
using _08_CodeFirst_DLL_Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _08_CodeFirst_DLL.ViewModel
{
    public partial class VM_UpdateCountrie : VM_Page
    {
        public VM_UpdateCountrie() { }
        [ObservableProperty] VM_Country country;
        [ObservableProperty] ObservableCollection<VM_PartOfTheWorld> partOfTheWorld;
        public VM_UpdateCountrie(VM_Country country, ObservableCollection<VM_PartOfTheWorld> partOfTheWorld)
        {
            PartOfTheWorld = partOfTheWorld;
            Country = country;
            Country.PartOfTheWorld = Country.PartOfTheWorld == null
                ? PartOfTheWorld[0]
                : PartOfTheWorld.First(x => x.Id == Country.PartOfTheWorld.Id);
        }
    }
}
