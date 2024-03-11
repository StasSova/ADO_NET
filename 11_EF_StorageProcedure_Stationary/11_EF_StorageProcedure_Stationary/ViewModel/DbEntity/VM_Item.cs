using CommunityToolkit.Mvvm.ComponentModel;
using DbCntx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _11_EF_StorageProcedure_Stationary.ViewModel.DbEntity;

public partial class VM_Item: ObservableObject
{
    [ObservableProperty] Item model;
    public VM_Item() 
    { 
        Model = new();
        Type = new();
    }
    public VM_Item(Item model) 
    { 
        this.Model = model;
        Type = new(model.Type);
    }
    public VM_Item(VM_Item model)
    {
        this.Model = new Item(model.Model);
        Type = new(model.Type);
    }
    public int Id 
    {
        get { return model.Id; }
        set
        {
            model.Id = value;
            OnPropertyChanged(nameof(Id));
        }
    }
    public string Name
    {
        get { return model.Name; }
        set
        {
            model.Name = value;
            OnPropertyChanged(nameof(Name));
        }
    }
    public int? TypeId
    {
        get { return model.TypeId; }
        set
        {
            model.TypeId = value;
            OnPropertyChanged(nameof(TypeId));
        }
    }
    public int? Count
    {
        get { return model.Count; }
        set
        {
            model.Count = value;
            OnPropertyChanged(nameof(Count));
        }
    }
    public decimal? CostPrice
    {
        get { return model.CostPrice; }
        set
        {
            model.CostPrice = value;
            OnPropertyChanged(nameof(CostPrice));
        }
    }
    public decimal? Price
    {
        get { return Model.Price; }
        set
        {
            Model.Price = value;
            OnPropertyChanged(nameof(Price));
        }
    }
    private VM_TypeOfItem? _type;
    public virtual VM_TypeOfItem? Type 
    { 
        get { return _type; }
        set
        {
            _type = value;
            Model.Type = value?.Model;
            OnPropertyChanged(nameof(Type));
        } 
    }
    public bool IsValid()
    {
        // Проверка всех обязательных полей на null или пустую строку
        if (string.IsNullOrEmpty(Name))
            return false;

        if (!TypeId.HasValue)
            return false;

        if (!Count.HasValue)
            return false;

        if (!CostPrice.HasValue)
            return false;

        if (!Price.HasValue)
            return false;

        if (Type != null && !Type.IsValid())
            return false;

        return true;
    }
}
