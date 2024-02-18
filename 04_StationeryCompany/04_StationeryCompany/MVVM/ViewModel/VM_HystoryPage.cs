using _04_StationeryCompany.MVVM.ViewModelResponseDB;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _04_StationeryCompany.MVVM.ViewModel
{
    public class VM_HystoryPage: VM_Base
    {
        public VM_HystoryPage() 
        {
            SortDirection = ListSortDirection.Ascending;
            SortedBy = "Item";
            Sort = new RelayCommand<string>(SortObservableCollection);
            ShowItemsSoldByManagerIdCommand = new AsyncRelayCommand(ShowItemsSoldByManagerId);
            ShowItemsBoughtByCompanyIdCommand = new AsyncRelayCommand(ShowItemsBoughtByCompanyId);
        }
        public RelayCommand<string> Sort { get; set; }
        string sortedBy;
        public string SortedBy
        {
            get { return sortedBy; }
            set { sortedBy = value; OnPropertyChanged(nameof(SortedBy)); }
        }

        ListSortDirection sortDirection;
        public ListSortDirection SortDirection
        {
            get { return sortDirection; }
            set { sortDirection = value; OnPropertyChanged(nameof(SortDirection)); }
        }
        private void SortObservableCollection(string property)
        {
            var keyExtractor = UpdateSortParams(property, reverse: true);
            Sells.Sort(keyExtractor, SortDirection);
        }
        private Func<VM_HistoryOfSells, object> UpdateSortParams(string property, bool reverse)
        {
            var lambda = KeyExtractor(property);

            if (SortedBy == property && reverse)
            {
                SortDirection = SortDirection == ListSortDirection.Ascending ?
                                                 ListSortDirection.Descending :
                                                 ListSortDirection.Ascending;
            }
            else if (reverse)
            {
                SortDirection = ListSortDirection.Descending;
            }
            SortedBy = property;

            return lambda;
        }
        private static Func<VM_HistoryOfSells, object> KeyExtractor(string property) => property switch
        {
            "Item" => c => c.ItemId,
            "Manager" => c => c.ManagerID,
            "Company" => c => c.BuyersCompany,
            "Count" => c => c.Count,
            "Date" => c => c.Date,
            "Information" => c => c.AdditionInformation,
            _ => throw new NotImplementedException()
        };
        public override async void Initialize(object parameter)
        {
            base.Initialize(parameter);
            await StorageCollection.FetchAllData();
            Sells = StorageCollection.Instance.Sells;
            StorageCollectionRef = StorageCollection.Instance;
        }
        private ObservableCollection<VM_HistoryOfSells> sells = new();
        public ObservableCollection<VM_HistoryOfSells> Sells
        {
            get { return sells; }
            set { sells = value; OnPropertyChanged(nameof(Sells)); }
        }

        private VM_Manager selectedManager;
        public VM_Manager SelectedManager
        {
            get { return selectedManager; }
            set
            {
                selectedManager = value; OnPropertyChanged(nameof(SelectedManager));
            }
        }
        private StorageCollection _storeColl;
        public StorageCollection StorageCollectionRef
        {
            get { return _storeColl; }
            set { _storeColl = value; OnPropertyChanged(nameof(StorageCollectionRef)); }
        }
        //Показать канцтовары, которые продал конкретный менеджер по продажам
        public IAsyncRelayCommand ShowItemsSoldByManagerIdCommand { get; }
        private async Task ShowItemsSoldByManagerId()
        {
            try
            {
                if (SelectedManager != null)
                    await StorageCollection.GetItemsSoldByManagerId(SelectedManager.Id);
            }
            catch { }
        }

        private VM_Company selectedCompany;
        public VM_Company SelectedCompany
        {
            get { return selectedCompany; }
            set { selectedCompany = value; OnPropertyChanged(nameof(SelectedCompany)); }
        }
        public IAsyncRelayCommand ShowItemsBoughtByCompanyIdCommand { get; }
        private async Task ShowItemsBoughtByCompanyId()
        {
            try
            {
                if (SelectedCompany != null)
                    await StorageCollection.GetItemsBoughtByCompanyId(SelectedCompany.Id);
            }
            catch { }
        }

    }
}
