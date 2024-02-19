using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _05_Stock.WindowView
{
    public partial class VM_AddType : ObservableObject, INotifyPropertyChanged
    {
        [ObservableProperty] string name;
    }
}
