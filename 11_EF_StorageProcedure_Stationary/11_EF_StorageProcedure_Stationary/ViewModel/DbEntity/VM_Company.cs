using CommunityToolkit.Mvvm.ComponentModel;
using DbCntx;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _11_EF_StorageProcedure_Stationary.ViewModel.DbEntity;

public partial class VM_Company : ObservableObject
{
    [ObservableProperty] Company model;

    public VM_Company()
    {
        Model = new Company();
    }
    public VM_Company(Company model)
    {
        this.Model = model;
    }

    public VM_Company(VM_Company viewModel)
    {
        this.Model = new Company(viewModel.Model);
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

    public string Name
    {
        get { return Model.Name; }
        set
        {
            Model.Name = value;
            OnPropertyChanged(nameof(Name));
        }
    }

    public bool IsValid()
    {
        // Проверка обязательного поля на null или пустую строку
        if (string.IsNullOrEmpty(Name))
            return false;

        return true;
    }
}

