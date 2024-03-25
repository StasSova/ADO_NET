using BookStore_DbContext.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _13_Exam_BookShop.ViewModels.DbViewModels;

public partial class VM_BookForSale : VM_Book
{
    [ObservableProperty] M_BookForSale model;
    public VM_BookForSale(M_BookForSale model):base(model)
    {
        Model = model;
    }
    public int Id
    {
        get { return Model.Id; }
        set
        {
            if (Model.Id != value)
            {
                Model.Id = value;
                OnPropertyChanged(nameof(Id));
            }
        }
    }
    
    public decimal CostPrice
    {
        get { return Model.CostPrice; }
        set
        {
            if (Model.CostPrice != value)
            {
                Model.CostPrice = value;
                OnPropertyChanged(nameof(CostPrice));
            }
        }
    }
    public decimal SellingPrice
    {
        get { return Model.SellingPrice; }
        set
        {
            if (Model.SellingPrice != value)
            {
                Model.SellingPrice = value;
                OnPropertyChanged(nameof(SellingPrice));
            }
        }
    }
    public decimal Discount
    {
        get { return Model.Discount; }
        set
        {
            if (Model.Discount != value)
            {
                Model.Discount = value;
                OnPropertyChanged(nameof(Discount));
            }
        }
    }
    public bool IsOnSale
    {
        get { return Model.IsOnSale; }
        set
        {
            if (Model.IsOnSale != value)
            {
                Model.IsOnSale = value;
                OnPropertyChanged(nameof(IsOnSale));
            }
        }
    }

    public VM_WarehouseItem WarehouseItem
    {
        get { return new(Model.WarehouseItem); }
    }
}
