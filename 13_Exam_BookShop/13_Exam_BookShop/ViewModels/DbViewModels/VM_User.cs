using BookStore_DbContext.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _13_Exam_BookShop.ViewModels.DbViewModels;

public partial class VM_User : VM_Entity
{
    [ObservableProperty] M_User model;
    static VM_User? Instance;
    public static VM_User GetInstance()
    {
        return Instance;
    }
    public static void SetInstance(M_User user)
    {
        if (Instance == null)
        {
            Instance = new VM_User();
        }
        Instance.Model = user;
    }
    private VM_User() { }
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
    public string UserName
    {
        get { return Model.Username; }
        set
        {
            if (Model.Username != value)
            {
                Model.Username = value;
                OnPropertyChanged(nameof(UserName));
            }
        }
    }
    public string Password
    {
        get { return Model.Password; }
        set
        {
            if (Model.Password != value)
            {
                Model.Password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
    }

    VM_ShoppingCart currentShoppingCarts;
    public VM_ShoppingCart CurrentShoppingCarts
    {
        get
        {
            if (currentShoppingCarts == null)
            {
                currentShoppingCarts = new VM_ShoppingCart(Model.ShoppingCarts.Last());
            }
            return currentShoppingCarts;
        }
        set
        {
            if (value != null)
            {
                currentShoppingCarts = value;
                OnPropertyChanged(nameof(CurrentShoppingCarts));
            }
        }
    }
    public ObservableCollection<VM_ShoppingCart> ShoppingCarts
    {
        get
        {
            var collection = new ObservableCollection<VM_ShoppingCart>();
            try
            {
                var db = Model.ShoppingCarts;
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
