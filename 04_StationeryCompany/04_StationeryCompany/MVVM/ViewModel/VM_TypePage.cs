using _04_StationeryCompany.DataBaseResponse;
using _04_StationeryCompany.MVVM.ViewModelResponseDB;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace _04_StationeryCompany.MVVM.ViewModel
{
    public class VM_TypePage : VM_Base
    {
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
            TypeOfItems.Sort(keyExtractor, SortDirection);
        }
        private Func<VM_TypeOfItem, object> UpdateSortParams(string property, bool reverse)
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
        private static Func<VM_TypeOfItem, object> KeyExtractor(string property) => property switch
        {
            "Type" => c => c.Type,
            "Count of all product" => c => c.Count,
            _ => throw new NotImplementedException()
        };

        private ObservableCollection<VM_TypeOfItem> typeOfItems = new();
        public ObservableCollection<VM_TypeOfItem> TypeOfItems
        {
            get { return typeOfItems; }
            set { typeOfItems = value; OnPropertyChanged(nameof(TypeOfItems)); }
        }
        public override void Initialize(object parameter)
        {
            base.Initialize(parameter);
            TypeOfItems = StorageCollection.Instance.TypeOfItems;
        }

        public VM_TypePage()
        {
            SortDirection = ListSortDirection.Ascending;
            SortedBy = "Type";
            Sort = new RelayCommand<string>(SortObservableCollection);

            GetMostProfitableItemTypeCommand = new AsyncRelayCommand(GetMostProfitableItemType);
            GetItemTypeWithMostSalesCommand = new AsyncRelayCommand(GetItemTypeWithMostSales);
            RemoveCommand = new AsyncRelayCommand<VM_TypeOfItem>(Remove);
        }
        public IAsyncRelayCommand<VM_TypeOfItem> RemoveCommand { get; set; }
        private async Task Remove(VM_TypeOfItem item)
        {
            try
            {
                // Запрос подтверждения удаления
                MessageBoxResult result = MessageBox.Show($"Вы уверены, что хотите удалить этот элемент?\n{item.Type}", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);

                // Если пользователь подтвердил удаление
                if (result == MessageBoxResult.Yes)
                {
                    // Удаление элемента
                    await StorageCollection.Instance.DeleteItemTypeById(item.Id);
                    await StorageCollection.FetchAllData();
                }
            }
            catch { }
        }
        public IAsyncRelayCommand GetMostProfitableItemTypeCommand { get; }
        private async Task GetMostProfitableItemType()
        {
            try
            {
                await StorageCollection.GetMostProfitableItemType(5);
            }
            catch { }
        }

        public IAsyncRelayCommand GetItemTypeWithMostSalesCommand { get; }
        private async Task GetItemTypeWithMostSales()
        {
            try
            {
                await StorageCollection.GetItemTypeWithMostSales(5);
            }
            catch { }
        }

    }
}
