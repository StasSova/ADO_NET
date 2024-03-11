using CommunityToolkit.Mvvm.ComponentModel;
using DbCntx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _11_EF_StorageProcedure_Stationary.ViewModel.DbEntity;

public partial class VM_TypeOfItem: ObservableObject
{
    [ObservableProperty] TypeOfItem model;
    public VM_TypeOfItem() { Model = new TypeOfItem(); }
    public VM_TypeOfItem(TypeOfItem model)
    {
        this.Model = model;
    }
    public VM_TypeOfItem(VM_TypeOfItem model)
    {
        this.Model = new (model.Model);
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
    public string Type
    {
        get { return Model.Type; }
        set
        {
            Model.Type = value;
            OnPropertyChanged(nameof(Type));
        }
    }
    public int? Count
    {
        get { return Model.Count; }
        set
        {
            Model.Count = value;
            OnPropertyChanged(nameof(Count));
        }
    }

    public bool IsValid()
    {
        // Проверка обязательных полей на null или пустую строку
        if (string.IsNullOrEmpty(Type))
            return false;

        if (!Count.HasValue)
            return false;

        return true;
    }

    public override string ToString()
    {
        return Type;
    }
}
