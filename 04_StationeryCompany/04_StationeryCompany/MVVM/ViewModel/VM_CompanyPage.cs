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
    public class VM_CompanyPage:VM_Base
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
            Company.Sort(keyExtractor, SortDirection);
        }
        private Func<VM_Company, object> UpdateSortParams(string property, bool reverse)
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
        private static Func<VM_Company, object> KeyExtractor(string property) => property switch
        {
            "Name" => c => c.Name,
            _ => throw new NotImplementedException()
        };

        public VM_CompanyPage()
        {
            SortDirection = ListSortDirection.Ascending;
            SortedBy = "Name";
            Sort = new RelayCommand<string>(SortObservableCollection);
            RemoveCommand = new AsyncRelayCommand<VM_Company>(Remove);
        }
        public override void Initialize(object parameter)
        {
            base.Initialize(parameter);
            Company = StorageCollection.Instance.Company;
        }
        private ObservableCollection<VM_Company> company = new();
        public ObservableCollection<VM_Company> Company
        {
            get { return company; }
            set { company = value; OnPropertyChanged(nameof(Company)); }
        }

        public IAsyncRelayCommand<VM_Company> RemoveCommand { get; set; }
        private async Task Remove(VM_Company item)
        {
            try
            {
                // Запрос подтверждения удаления
                MessageBoxResult result = MessageBox.Show($"Вы уверены, что хотите удалить этот элемент?\n{item.Name}", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);

                // Если пользователь подтвердил удаление
                if (result == MessageBoxResult.Yes)
                {
                    // Удаление элемента
                    await StorageCollection.Instance.DeleteCompanyById(item.Id);
                    await StorageCollection.FetchAllData();
                }
            }
            catch { }
        }
    }
}
