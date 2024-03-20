using BookStore_DbContext.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _13_Exam_BookShop.ViewModels.DbViewModels;

public partial class VM_ShoppingCart : VM_Entity
{
    [ObservableProperty] M_ShoppingCart model;
    public VM_ShoppingCart(M_ShoppingCart model)
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
    
    public int UserId
    {
        get { return Model.UserId; }
        protected set
        {
            if (Model.UserId != value)
            {
                Model.UserId = value;
                OnPropertyChanged(nameof(UserId));
            }
        }
    }
    public VM_User User
    {
        get { return new (Model.User); }
        set
        {
            if (Model.User != value.Model)
            {
                Model.User = value.Model;
                this.UserId = value.Id;
                OnPropertyChanged(nameof(User));
            }
        }
    }
    
    public ObservableCollection<VM_BookForSale> Books
    {
        get
        {
            var collection = new ObservableCollection<VM_BookForSale>();
            try
            {
                var db = Model.Books;
                foreach (var book in db)
                {
                    collection.Add(new(book));
                }
            }
            catch { }
            return collection;
        }
    }
}
