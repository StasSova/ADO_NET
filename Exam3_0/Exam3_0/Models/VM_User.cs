using BookContext.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam3_0.Models;

public partial class VM_User : VM_Entity
{
    [ObservableProperty] M_User model;
    static VM_User? Instance;
    private ICollection<VM_ShoppingCart>? _shoppingCarts;
    private VM_ShoppingCart? currentShoppingCarts;

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
    public ICollection<VM_ShoppingCart> ShoppingCarts
    {
        get
        {
            return _shoppingCarts ??= Model.ShoppingCarts.Select(cart => new VM_ShoppingCart(cart)).ToList();
        }
        set
        {
            if (_shoppingCarts != value)
            {
                _shoppingCarts = value;
                OnPropertyChanged(nameof(ShoppingCarts));
                Model.ShoppingCarts = value.Select(vm_cart => vm_cart.Model).ToList();
            }
        }
    }

    public void AddShoppingCart(VM_ShoppingCart cart)
    {
        Model.ShoppingCarts.Add(cart.Model);
        _shoppingCarts?.Append(cart);
    }

    public void RemoveShoppingCart(VM_ShoppingCart cart)
    {
        Model.ShoppingCarts.Remove(cart.Model);
        if (_shoppingCarts != null)
        {
            _shoppingCarts = _shoppingCarts.Where(c => c.Id != cart.Id).ToList();
        }
    }

    public void UpdateShoppingCarts(IEnumerable<VM_ShoppingCart> carts)
    {
        Model.ShoppingCarts.Clear();
        foreach (var cart in carts)
        {
            Model.ShoppingCarts.Add(cart.Model);
        }
        _shoppingCarts = null;
    }
}
