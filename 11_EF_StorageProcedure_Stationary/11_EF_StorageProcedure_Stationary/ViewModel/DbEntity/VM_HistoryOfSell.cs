using CommunityToolkit.Mvvm.ComponentModel;
using DbCntx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _11_EF_StorageProcedure_Stationary.ViewModel.DbEntity;

public partial class VM_HistoryOfSell : ObservableObject
{
    [ObservableProperty] HistoryOfSell model;

    public VM_HistoryOfSell()
    {
        Model = new HistoryOfSell();
    }
    public VM_HistoryOfSell(HistoryOfSell model)
    {
        this.Model = model;
    }

    public VM_HistoryOfSell(VM_HistoryOfSell viewModel)
    {
        this.Model = new HistoryOfSell(viewModel.Model);
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

    public int? ItemId
    {
        get { return Model.ItemId; }
        private set
        {
            Model.ItemId = value;
            OnPropertyChanged(nameof(ItemId));
        }
    }

    public int? ManagerId
    {
        get { return Model.ManagerId; }
        private set
        {
            Model.ManagerId = value;
            OnPropertyChanged(nameof(ManagerId));
        }
    }

    public int? BuyersCompany
    {
        get { return Model.BuyersCompany; }
        private set
        {
            Model.BuyersCompany = value;
            OnPropertyChanged(nameof(BuyersCompany));
        }
    }

    public int Count
    {
        get { return Model.Count; }
        private set
        {
            Model.Count = value;
            OnPropertyChanged(nameof(Count));
        }
    }
    public DateOnly Date
    {
        get { return Model.Date; }
        private set
        {
            Model.Date = value;
            OnPropertyChanged(nameof(Date));
        }
    }

    private VM_Company _buyersCompanyNavigation;
    public VM_Company BuyersCompanyNavigation
    {
        get
        {
            if (_buyersCompanyNavigation == null && Model.BuyersCompanyNavigation != null)
            {
                _buyersCompanyNavigation = new VM_Company(Model.BuyersCompanyNavigation);
            }
            return _buyersCompanyNavigation;
        }
    }

    private VM_Item _item;
    public VM_Item Item
    {
        get
        {
            if (_item == null && Model.Item != null)
            {
                _item = new VM_Item(Model.Item);
            }
            return _item;
        }
    }

    private VM_Manager _manager;
    public VM_Manager Manager
    {
        get
        {
            if (_manager == null && Model.Manager != null)
            {
                _manager = new VM_Manager(Model.Manager);
            }
            return _manager;
        }
    }

    public bool IsValid()
    {
        // Проверка наличия обязательных полей
        if (ItemId == null || ManagerId == null || BuyersCompany == null || Count == default || Date == null)
            return false;

        return true;
    }
}

