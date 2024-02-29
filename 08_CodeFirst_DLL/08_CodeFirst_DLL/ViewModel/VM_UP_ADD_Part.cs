using _08_CodeFirst_DLL.Response;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _08_CodeFirst_DLL.ViewModel
{
    public partial class VM_UP_ADD_Part : VM_Base
    {
        [ObservableProperty] VM_PartOfTheWorld part;
        public VM_UP_ADD_Part() { part = new(); }
        public VM_UP_ADD_Part(VM_PartOfTheWorld part)
        {
            Part = part;
        }

    }
}
