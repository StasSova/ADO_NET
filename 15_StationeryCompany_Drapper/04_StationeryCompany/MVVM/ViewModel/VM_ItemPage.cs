using _04_StationeryCompany.Base;
using _04_StationeryCompany.Interface;
using _04_StationeryCompany.MVVM.Insert.View;
using _04_StationeryCompany.MVVM.Insert.ViewModel;
using _04_StationeryCompany.MVVM.Update.View;
using _04_StationeryCompany.MVVM.Update.ViewModel;
using _04_StationeryCompany.MVVM.ViewModelResponseDB;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace _04_StationeryCompany.MVVM.ViewModel
{

    public class VM_ItemPage : VM_Base
    {
        public VM_ItemPage()
        {
            SortDirection = ListSortDirection.Ascending;
            SortedBy = "Name";
            Sort = new RelayCommand<string>(SortObservableCollection);
            RemoveCommand = new AsyncRelayCommand<VM_Item>(Remove);
            UpdateCommand = new AsyncRelayCommand<VM_Item>(Update);
            InsertCommand = new AsyncRelayCommand(Insert);
        }
        public RelayCommand<string> Sort { get; set; }
        public IAsyncRelayCommand<VM_Item> RemoveCommand { get; set; }
        private async Task Remove(VM_Item item)
        {
            try
            {
                // Запрос подтверждения удаления
                MessageBoxResult result = MessageBox.Show($"Вы уверены, что хотите удалить этот элемент?\n{item.Name} {item.Count} ед.", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);

                // Если пользователь подтвердил удаление
                if (result == MessageBoxResult.Yes)
                {
                    // Удаление элемента
                    Items.Remove(item);
                    await StorageCollection.Instance.DeleteItemById(item.Id);
                }
            }
            catch { }
        }

        public IAsyncRelayCommand<VM_Item> UpdateCommand { get; set; }
        private async Task Update(VM_Item item)
        {
            try
            {
                var VM = new VM_UpdateItem(item);
                var dialog = new V_UpdateItem();
                dialog.DataContext = VM;
                
                bool? result = dialog.ShowDialog();
                if (result == true)
                {
                    VM_Item a = VM.Item;
                    a.Type_ID = VM.SelectedTypeItem.Id;
                    await StorageCollection.Instance.UpdateItem(a);
                }
            }
            catch { }
        }

        public IAsyncRelayCommand InsertCommand { get; set; }
        private async Task Insert()
        {
            try
            {
                var VM = new VM_InsertItem();
                var dialog = new V_InsertItem();
                dialog.DataContext = VM;

                bool? result = dialog.ShowDialog();
                if (result == true)
                {
                    VM.Item.Type_ID = VM.SelectedTypeItem.Id;
                    await StorageCollection.Instance.InsertItem(VM.Item);
                }
            }
            catch { }
        }

        string sortedBy;
        public string SortedBy
        { 
            get { return sortedBy; } 
            set {  sortedBy = value; OnPropertyChanged(nameof(SortedBy)); }
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
            Items.Sort(keyExtractor, SortDirection);
        }
        private Func<VM_Item, object> UpdateSortParams(string property, bool reverse)
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
        private static Func<VM_Item, object> KeyExtractor(string property) => property switch
        {
            "Name" => c => c.Name,
            "Type" => c => c.Type_ID,
            "Count" => c => c.Count,
            "Cost Price" => c => c.CostPrice,
            "Price" => c => c.Price,
            _ => throw new NotImplementedException()
        };

        public override async void Initialize(object parameter)
        {
            base.Initialize(parameter);
            await StorageCollection.FetchAllData();
            Items = StorageCollection.Instance.Items;
        }
        private ObservableCollection<VM_Item> items = new();
        public ObservableCollection<VM_Item> Items
        {
            get { return items; }
            set { items = value; OnPropertyChanged(nameof(Items)); }
        }

    }
}
