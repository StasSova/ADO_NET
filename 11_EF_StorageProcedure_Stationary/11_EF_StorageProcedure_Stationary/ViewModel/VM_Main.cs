using _11_EF_StorageProcedure_Stationary.ViewModel.DbEntity;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DbCommandAPI;
using DbCommandAPI.Implementation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace _11_EF_StorageProcedure_Stationary.ViewModel;

public partial class VM_Main : ObservableObject
{
    ObservableCollection<VM_Item> items;
    public ObservableCollection<VM_Item> Items 
    { 
        get { return items; }
        set { items = value; OnPropertyChanged(nameof(Items)); }
    }
    private ObservableCollection<VM_Company> companies;
    public ObservableCollection<VM_Company> Companies
    {
        get { return companies; }
        set { companies = value; OnPropertyChanged(nameof(Companies)); }
    }

    private ObservableCollection<VM_Manager> managers;
    public ObservableCollection<VM_Manager> Managers
    {
        get { return managers; }
        set { managers = value; OnPropertyChanged(nameof(Managers)); }
    }

    private ObservableCollection<VM_TypeOfItem> types;
    public ObservableCollection<VM_TypeOfItem> Types
    {
        get { return types; }
        set { types = value; OnPropertyChanged(nameof(Types)); }
    }

    VM_Item selectedItem;
    public VM_Item SelectedItem
    {
        get { return selectedItem; }
        set
        {
            if (value != null && value.TypeId != null && value.TypeId != default)
            {
                selectedItem = new VM_Item(value);
                int r = value.TypeId == 0 ? 0 : value.TypeId.GetHashCode();
                SelectedItem.Type = Types?.FirstOrDefault(x => x.Id == r);
            }
            SelectedItemPropertyChanged();
            OnPropertyChanged(nameof(SelectedItem));
        }
    }

    VM_Company selectedCompany;
    public VM_Company SelectedCompany
    {
        get { return selectedCompany; }
        set
        {
            if (value != null)
            {
                selectedCompany = new VM_Company(value);
                SelectedCompaniesPropertyChanged();
                OnPropertyChanged(nameof(SelectedCompany));
            }
        }
    }
    
    VM_Manager selectedManager;
    public VM_Manager SelectedManager
    {
        get { return selectedManager; }
        set
        {
            if (value != null)
            {
                selectedManager = new VM_Manager(value);
                SelectedmanagerPropertyChanged();
                OnPropertyChanged(nameof(SelectedManager));
            }
        }
    }
    
    VM_TypeOfItem selectedType;
    public VM_TypeOfItem SelectedType
    {
        get { return selectedType; }
        set
        {
            if (value != null)
            {
                selectedType = new(value);
                SelectedTypePropertyChanged();
                OnPropertyChanged(nameof(SelectedType));
            }
        }
    }

    // -----------------------------
    //              NOTIFY PROPERTY CHANGED
    // ----------------------------- 
    public void SelectedItemPropertyChanged()
    {
        AddItemCommand.NotifyCanExecuteChanged();
        UpdateItemCommand.NotifyCanExecuteChanged();
        RemoveItemCommand.NotifyCanExecuteChanged();
    }

    public void SelectedTypePropertyChanged()
    {
        AddTypeCommand.NotifyCanExecuteChanged();
        UpdateTypeCommand.NotifyCanExecuteChanged();
        RemoveTypeCommand.NotifyCanExecuteChanged();
    }

    public void SelectedmanagerPropertyChanged()
    {
        AddManagerCommand.NotifyCanExecuteChanged();
        UpdateManagerCommand.NotifyCanExecuteChanged();
        RemoveManagerCommand.NotifyCanExecuteChanged();
    }

    public void SelectedCompaniesPropertyChanged()
    {
        AddCompanyCommand.NotifyCanExecuteChanged();
        UpdateCompanyCommand.NotifyCanExecuteChanged();
        RemoveCompanyCommand.NotifyCanExecuteChanged();
    }


    // -----------------------------
    //              ADD
    // ----------------------------- 
    [RelayCommand(CanExecute = nameof(IsValidItem))] void AddItem() {
        try
        {
            DbApi.InsertNewItem(SelectedItem.Model);
            Items = new(DbApi.GetItems().Select(x => new VM_Item(x)));
            MessageBox.Show("Item successfully added");
        }
        catch(Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }
    [RelayCommand(CanExecute = nameof(IsValidCompany))] void AddCompany() {
        try
        {
            DbApi.InsertNewCompany(SelectedCompany.Model);
            SelectedCompany = new();
            
            Companies = new ObservableCollection<VM_Company>(DbApi.GetCompanys().Select(x => new VM_Company(x)));
            MessageBox.Show("Company successfully added");
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }
    [RelayCommand(CanExecute = nameof(IsValidManager))] void AddManager() {
        try
        {
            DbApi.InsertNewManager(SelectedManager.Model);
            SelectedManager = new();

            Managers = new ObservableCollection<VM_Manager>(DbApi.GetManagers().Select(x => new VM_Manager(x)));
            MessageBox.Show("Manager successfully added");
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }
    [RelayCommand(CanExecute = nameof(IsValidType))] void AddType() {
        try
        {
            DbApi.InsertNewType(SelectedType.Model);

            SelectedType = new();
            Types = new ObservableCollection<VM_TypeOfItem>(DbApi.GetTypes().Select(x => new VM_TypeOfItem(x)));
            MessageBox.Show("Type successfully added");
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }
    // -----------------------------
    //              IS VALID
    // -----------------------------
    bool IsValidItem() { return SelectedItem.IsValid(); }
    bool IsValidCompany() { return SelectedCompany.IsValid(); }
    bool IsValidManager() { return SelectedManager.IsValid(); }
    bool IsValidType() { return SelectedType.IsValid(); }

    bool IsValidItemToChange() { return SelectedItem.IsValid() && SelectedItem.Id != default; }
    bool IsValidCompanyToChange() { return SelectedCompany.IsValid() && SelectedCompany.Id != default; }
    bool IsValidManagerToChange() { return SelectedManager.IsValid() && SelectedManager.Id != default; }
    bool IsValidTypeToChange() { return SelectedType.IsValid() && SelectedType.Id != default; }

    // -----------------------------
    //              REMOVE
    // -----------------------------
    [RelayCommand(CanExecute = nameof(IsValidItemToChange))] void RemoveItem() {
        try
        {
            var rem = Items.FirstOrDefault(x=> x.Id == SelectedItem.Id);
            MessageBoxResult result = MessageBox.Show($"Вы уверены, что хотите удалить элемент '{rem.Name}'?", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                DbApi.DeleteItem(rem.Id);
                Items.Remove(rem);
                MessageBox.Show("Элемент успешно удален");
            }

            SelectedItem = new();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }
    [RelayCommand(CanExecute = nameof(IsValidCompanyToChange))] void RemoveCompany() {
        try
        {
            var rem = Companies.FirstOrDefault(x => x.Id == selectedCompany.Id);
            MessageBoxResult result = MessageBox.Show($"Вы уверены, что хотите удалить фирму '{rem.Name}'?", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                DbApi.DeleteCompany(rem.Id);
                Companies.Remove(rem);
                MessageBox.Show("Фирма успешно удалена");
            }

            SelectedCompany = new();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }
    [RelayCommand(CanExecute = nameof(IsValidManagerToChange))] void RemoveManager() {
        try
        {
            var rem = Managers.FirstOrDefault(x => x.Id == selectedManager.Id);
            MessageBoxResult result = MessageBox.Show($"Вы уверены, что хотите удалить менеджера '{rem.Fname} {rem.Lname}'?", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                DbApi.DeleteManager(rem.Id);
                Managers.Remove(rem);
                MessageBox.Show("Менеджер успешно удален");
            }

            SelectedManager = new();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }
    [RelayCommand(CanExecute = nameof(IsValidTypeToChange))] void RemoveType() {
        try
        {
            var rem = Types.FirstOrDefault(x => x.Id == SelectedType.Id);
            MessageBoxResult result = MessageBox.Show($"Вы уверены, что хотите удалить тип канцтоваров '{rem.Type}'?", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                DbApi.DeleteType(SelectedType.Id);
                SelectedType = new();
                Types.Remove(rem);
                MessageBox.Show("Тип канцтоваров успешно удален");
            }

            Items = new(DbApi.GetItems().Select(x => new VM_Item(x)));
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    // -----------------------------
    //              UPDATE
    // -----------------------------
    [RelayCommand(CanExecute = nameof(IsValidItemToChange))] void UpdateItem() {
        try
        {
            int updatedItemId = SelectedItem.Id;
            DbApi.UpdateItemInformation(updatedItemId, SelectedItem.Model);
            MessageBox.Show("Информация о канцтоваре успешно обновлена");

            var updatedItem = Items.FirstOrDefault(x => x.Id == updatedItemId);
            if (updatedItem != null)
            {
                int index = Items.IndexOf(updatedItem);
                Items[index] = new VM_Item(SelectedItem.Model) { Id = updatedItemId }; // Присваиваем старый Id
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }
    [RelayCommand(CanExecute = nameof(IsValidCompanyToChange))] void UpdateCompany() {
        try
        {
            int updatedCompanyId = SelectedCompany.Id; // Сохраняем Id перед обновлением
            DbApi.UpdateCompanyInformation(updatedCompanyId, SelectedCompany.Model);
            MessageBox.Show("Информация о фирме успешно обновлена");

            // Найдем элемент в коллекции по Id и заменим его на новый
            var updatedCompany = Companies.FirstOrDefault(x => x.Id == updatedCompanyId);
            if (updatedCompany != null)
            {
                int index = Companies.IndexOf(updatedCompany);
                Companies[index] = new VM_Company(SelectedCompany.Model) { Id = updatedCompanyId }; // Присваиваем старый Id
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }
    [RelayCommand(CanExecute = nameof(IsValidManagerToChange))] void UpdateManager() {
        try
        {
            int updatedManagerId = SelectedManager.Id; // Сохраняем Id перед обновлением
            DbApi.UpdateManagerInformation(updatedManagerId, SelectedManager.Model);
            MessageBox.Show("Информация о менеджере успешно обновлена");

            // Найдем элемент в коллекции по Id и заменим его на новый
            var updatedManager = Managers.FirstOrDefault(x => x.Id == updatedManagerId);
            if (updatedManager != null)
            {
                int index = Managers.IndexOf(updatedManager);
                Managers[index] = new VM_Manager(SelectedManager.Model) { Id = updatedManagerId }; // Присваиваем старый Id
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }
    [RelayCommand(CanExecute = nameof(IsValidTypeToChange))] void UpdateType() {
        try
        {
            int updatedTypeId = SelectedType.Id; // Сохраняем Id перед обновлением
            DbApi.UpdateTypeInformation(updatedTypeId, SelectedType.Model);
            MessageBox.Show("Информация о типе канцтоваров успешно обновлена");
            // Найдем элемент в коллекции по Id и заменим его на новый
            var updatedType = Types.FirstOrDefault(x => x.Id == updatedTypeId);
            if (updatedType != null)
            {
                int index = Types.IndexOf(updatedType);
                Types[index] = new VM_TypeOfItem(SelectedType.Model) { Id = updatedTypeId }; // Присваиваем старый Id

                var r = Items.Where(x => x.TypeId == updatedType.Id);
                foreach (var item in r) 
                {
                    item.Type = Types[index];
                    item.TypeId = updatedTypeId;
                }
                SelectedType = new();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }


    // -----------------------------
    //              MENU FUNCTION
    // -----------------------------
    [RelayCommand] void GetItems() {
        Items = new(DbApi.GetItems().Select(x => new VM_Item(x)));
    }
    [RelayCommand] void GetItemsWithMaxCostPrice(){
        Items = new(DbApi.GetItemsWithMaxCostPrice().Select(x => new VM_Item(x)));
    }
    [RelayCommand] void GetItemsWithMinCostPrice() {
        Items = new(DbApi.GetItemsWithMinCostPrice().Select(x => new VM_Item(x)));
    }
    [RelayCommand] void GetItemsWithMaxNumbers() {
        Items = new(DbApi.GetItemsWithMaxNumbers().Select(x => new VM_Item(x)));
    }
    [RelayCommand] void GetItemsWithMinNumbers() {
        Items = new(DbApi.GetItemsWithMinNumbers().Select(x => new VM_Item(x)));
    }


    public VM_Main() {

    }
    [ObservableProperty] IDbAPI dbApi;
    public VM_Main(IDbAPI Api)
    {
        dbApi = Api;
        FetchData();
    }
    private void FetchData()
    {
        SelectedCompany = new();
        SelectedItem = new();
        SelectedManager = new();
        SelectedType = new();

        Items = new (DbApi.GetItems().Select(x => new VM_Item(x)));
        Companies = new(DbApi.GetCompanys().Select(x => new VM_Company(x)));
        Managers = new(DbApi.GetManagers().Select(x => new VM_Manager(x)));
        Types = new(DbApi.GetTypes().Select(x => new VM_TypeOfItem(x)));
    }

}
