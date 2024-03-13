using CommunityToolkit.Mvvm.ComponentModel;
using DLL_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _12_Migration.ViewModels;
public partial class VM_FeaturesOfGame : ObservableObject
{
    [ObservableProperty] M_FeaturesOfGame model;

    public VM_FeaturesOfGame(M_FeaturesOfGame model)
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
