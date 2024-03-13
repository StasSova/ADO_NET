using CommunityToolkit.Mvvm.ComponentModel;
using DLL_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _12_Migration.ViewModels;
public partial class VM_GameStyle : ObservableObject
{
    [ObservableProperty] M_GameStyle model;

    public VM_GameStyle(M_GameStyle model)
    {
        Model = model;
    }

    public string Name
    {
        get { return Model.Name; }
        set
        {
            if (value != Model.Name)
            {
                Model.Name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
    }
}