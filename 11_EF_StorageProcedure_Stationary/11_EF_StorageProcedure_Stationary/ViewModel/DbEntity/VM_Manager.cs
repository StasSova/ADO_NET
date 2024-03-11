using CommunityToolkit.Mvvm.ComponentModel;
using DbCntx;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _11_EF_StorageProcedure_Stationary.ViewModel.DbEntity;
public partial class VM_Manager : ObservableObject
{
    [ObservableProperty] Manager model;

    public VM_Manager() { Model = new(); }
    public VM_Manager(Manager model)
    {
        this.Model = model;
    }

    public VM_Manager(VM_Manager viewModel)
    {
        this.Model = new Manager(viewModel.Model);
    }

    public int Id
    {
        get { return Model.Id; }
        set
        {
            Model.Id = value;
            OnPropertyChanged(nameof(Id));
        }
    }

    public string Fname
    {
        get { return Model.Fname; }
        set
        {
            Model.Fname = value;
            OnPropertyChanged(nameof(Fname));
        }
    }

    public string Lname
    {
        get { return Model.Lname; }
        set
        {
            Model.Lname = value;
            OnPropertyChanged(nameof(Lname));
        }
    }

    public bool IsValid()
    {
        // Проверка обязательных полей на null или пустую строку
        if (string.IsNullOrEmpty(Fname) || string.IsNullOrEmpty(Lname))
            return false;

        return true;
    }
}

