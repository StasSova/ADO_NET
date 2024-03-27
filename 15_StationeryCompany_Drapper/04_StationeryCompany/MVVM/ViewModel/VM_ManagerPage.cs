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
    public class VM_ManagerPage : VM_Base
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
            Managers.Sort(keyExtractor, SortDirection);
        }
        private Func<VM_Manager, object> UpdateSortParams(string property, bool reverse)
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
        private static Func<VM_Manager, object> KeyExtractor(string property) => property switch
        {
            "First name" => c => c.FName,
            "Last name" => c => c.LName,
            _ => throw new NotImplementedException()
        };
        public VM_ManagerPage()
        {
            SortDirection = ListSortDirection.Ascending;
            SortedBy = "First name";
            Sort = new RelayCommand<string>(SortObservableCollection);
            RemoveCommand = new AsyncRelayCommand<VM_Manager>(Remove);
        }
        public IAsyncRelayCommand<VM_Manager> RemoveCommand { get; set; }
        private async Task Remove(VM_Manager item)
        {
            try
            {
                MessageBoxResult result = MessageBox.Show($"Вы уверены, что хотите удалить этот элемент?\n{item.FName} {item.LName}", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    // Удаление элемента
                    await StorageCollection.Instance.DeleteManagerById(item.Id);
                    await StorageCollection.FetchAllData();
                }
            }
            catch { }
        }
        public override void Initialize(object parameter)
        {
            base.Initialize(parameter);
            Managers = StorageCollection.Instance.Managers;
        }
        private ObservableCollection<VM_Manager> managers = new();
        public ObservableCollection<VM_Manager> Managers
        {
            get { return managers; }
            set { managers = value; OnPropertyChanged(nameof(Managers)); }
        }
    }
}
