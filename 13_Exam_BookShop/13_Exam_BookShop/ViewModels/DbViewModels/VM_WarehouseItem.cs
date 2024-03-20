using BookStore_DbContext.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _13_Exam_BookShop.ViewModels.DbViewModels;

public partial class VM_WarehouseItem : VM_Entity
{
    [ObservableProperty] M_WarehouseItem model;
    public VM_WarehouseItem(M_WarehouseItem model)
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
    public int BookForSaleId
    {
        get { return Model.BookForSaleId; }
        set
        {
            if (Model.BookForSaleId != value)
            {
                Model.BookForSaleId = value;
            }
        }
    }
    public VM_BookForSale BookForSale
    {
        get { return new(Model.BookForSale); }
        set
        {
            if (Model.BookForSale != value.Model)
            {
                Model.BookForSale = value.Model;
                this.BookForSaleId = value.Model.Id;
            }
        }
    }
    public int Quantity
    {
        get { return Model.Quantity; }
        set
        {
            if (Model.Quantity != value)
            {
                Model.Quantity = value;
            }
        }
    }
}
