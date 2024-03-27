using _04_StationeryCompany.Interface;
using _04_StationeryCompany.MVVM.ViewModelResponseDB;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _04_StationeryCompany.MVVM
{
    public class StorageCollection: VM_Base
    {
        private static StorageCollection instance;
        public static StorageCollection Instance
        {
            get 
            {
                return instance = instance == null ? new StorageCollection() : instance;
            }
        }
        private StorageCollection() { }
        public static IDataBaseCrud DataBaseCrud { get; set; }

        private ObservableCollection<VM_TypeOfItem> typeOfItems = new();
        public ObservableCollection<VM_TypeOfItem> TypeOfItems
        {
            get { return typeOfItems; }
            set { typeOfItems = value; OnPropertyChanged(nameof(TypeOfItems)); }
        }

        private ObservableCollection<VM_Manager> managers = new();
        public ObservableCollection<VM_Manager> Managers
        {
            get { return managers; }
            set { managers = value; OnPropertyChanged(nameof(Managers)); }
        }

        private ObservableCollection<VM_Company> company = new();
        public ObservableCollection<VM_Company> Company
        {
            get { return company; }
            set { company = value; OnPropertyChanged(nameof(Company)); }
        }

        private ObservableCollection<VM_Item> items = new();
        public ObservableCollection<VM_Item> Items
        {
            get { return items; }
            set { items = value; OnPropertyChanged(nameof(Items)); }
        }

        private ObservableCollection<VM_HistoryOfSells> sells = new();
        public ObservableCollection<VM_HistoryOfSells> Sells
        {
            get { return sells; }
            set { sells = value; OnPropertyChanged(nameof(Sells)); }    
        }

        public static async Task<ObservableCollection<TViewModel>> ConvertModelToViewModel<TModel, TViewModel>(IEnumerable<TModel> models, Func<TModel, TViewModel> converter)
        {
            ObservableCollection<TViewModel> viewModels = new ObservableCollection<TViewModel>();

            try
            {
                foreach (var model in models)
                {
                    viewModels.Add(converter(model));
                }
            }
            catch { }

            return viewModels;
        }
        public static async Task UpdateCollection<T>(ObservableCollection<T> collectionToUpdate, IEnumerable<T> newData)
        {
            try
            {
                collectionToUpdate.Clear();

                ObservableCollection<T> convertedData = await ConvertModelToViewModel(
                    newData,
                    x => x
                );

                foreach (var item in convertedData)
                {
                    collectionToUpdate.Add(item);
                }
            }
            catch { }
        }
        public static async Task FetchAllData()
        {
            try
            {
                await UpdateCollection<VM_TypeOfItem>(StorageCollection.Instance.TypeOfItems, await ConvertModelToViewModel(
                    await DataBaseCrud.GetAllItemTypesWithID(),
                    x => new VM_TypeOfItem(x)
                ));
                await UpdateCollection<VM_Manager>(StorageCollection.Instance.Managers, await ConvertModelToViewModel(
                    await DataBaseCrud.GetManagers(),
                    x => new VM_Manager(x)
                ));
                await UpdateCollection<VM_Company>(StorageCollection.Instance.Company, await ConvertModelToViewModel(
                    await DataBaseCrud.GetCompanyInfo(),
                    x => new VM_Company(x)
                ));
                await UpdateCollection<VM_Item>(StorageCollection.Instance.Items, await ConvertModelToViewModel(
                    await DataBaseCrud.GetAllItemsInformationWithID(),
                    x => new VM_Item(x)
                ));
                await UpdateCollection<VM_HistoryOfSells>(StorageCollection.Instance.Sells, await ConvertModelToViewModel(
                    await DataBaseCrud.GetAllHystorySells(),
                    x => new VM_HistoryOfSells(x)
                ));
            }
            catch { }
        }
        public static async Task GetMostProfitableItemType(int count = 15)
        {
            try
            {
                await UpdateCollection<VM_TypeOfItem>(StorageCollection.Instance.TypeOfItems, await ConvertModelToViewModel(
                    await DataBaseCrud.GetMostProfitableItemType(count),
                    x => new VM_TypeOfItem(x)
                ));
            }
            catch { }
        }
        public static async Task GetItemTypeWithMostSales(int count = 15)
        {
            try
            {
                await UpdateCollection<VM_TypeOfItem>(StorageCollection.Instance.TypeOfItems, await ConvertModelToViewModel(
                    await DataBaseCrud.GetItemTypeWithMostSales(count),
                    x => new VM_TypeOfItem(x)
                ));
            }
            catch { }
        }
        public static async Task GetItemsSoldByManagerId(int managerId)
        {
            try
            {
                await UpdateCollection<VM_HistoryOfSells>(StorageCollection.Instance.Sells, await ConvertModelToViewModel(
                    await DataBaseCrud.GetItemsSoldByManagerId(managerId),
                    x => new VM_HistoryOfSells(x)
                ));
            }
            catch { }
        }
        public static async Task GetItemsBoughtByCompanyId(int companyId)
        {
            try
            {
                await UpdateCollection<VM_HistoryOfSells>(StorageCollection.Instance.Sells, await ConvertModelToViewModel(
                    await DataBaseCrud.GetItemsBoughtByCompanyId(companyId),
                    x => new VM_HistoryOfSells(x)
                ));
            }
            catch { }
        }

        public async Task DeleteItemById(int ItemId)
        {
            try
            {
                await DataBaseCrud.DeleteItemById(ItemId);
            }
            catch { throw; }
        }
        public async Task DeleteManagerById(int ManagerId)
        {
            try
            {
                await DataBaseCrud.DeleteManagerById(ManagerId);
            }
            catch { throw; }
        }
        public async Task DeleteItemTypeById(int TypeId)
        {
            try
            {
                await DataBaseCrud.DeleteItemTypeById(TypeId);
            }
            catch { throw; }
        }
        public async Task DeleteCompanyById(int CompanyId)
        {
            try
            {
                await DataBaseCrud.DeleteCompanyById(CompanyId);
            }
            catch { throw; }
        }
        public async Task UpdateItem(VM_Item item)
        {
            try
            {
                await DataBaseCrud.UpdateItemById(item.Id, item.Model);
                await UpdateCollection<VM_Item>(StorageCollection.Instance.Items, await ConvertModelToViewModel(
                    await DataBaseCrud.GetAllItemsInformationWithID(),
                    x => new VM_Item(x)
                ));
            }
            catch { throw; }
        }
        public async Task InsertItem(VM_Item item)
        {
            try
            {
                await DataBaseCrud.InsertItem(item.Model);
                Items.Insert(0, item);
            }
            catch { throw; }
        }
    }
}
