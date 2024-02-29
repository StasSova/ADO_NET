using _08_CodeFirst_DLL.Response;
using _08_CodeFirst_DLL.Service;
using _08_CodeFirst_DLL.View;
using _08_CodeFirst_DLL_Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using MessageBox = System.Windows.MessageBox;

namespace _08_CodeFirst_DLL.ViewModel
{
    public partial class VM_Main: VM_Page
    {
        [ObservableProperty] private ObservableCollection<VM_Country> countries = new();
        [ObservableProperty] private ObservableCollection<VM_PartOfTheWorld> parts = new();

        public static async Task<ObservableCollection<TViewModel>> ConvertModelToViewModel<TModel, TViewModel>(IEnumerable<TModel> models, Func<TModel, TViewModel> converter)
        {
            return new ObservableCollection<TViewModel>(models.Select(converter));
        }
        public static async Task UpdateCollection<T>(ObservableCollection<T> collectionToUpdate, IEnumerable<T> newData)
        {
            collectionToUpdate.Clear();
            foreach (var item in newData)
            {
                collectionToUpdate.Add(item);
            }
        }

        private IDataBaseCrud db;
        public override async void Initialize(object parameter)
        {
            base.Initialize(parameter);
            db = (IDataBaseCrud)parameter;

            if (db == null)
            {
                return;
            }

            var countriesData = await db.GetCountry();
            var viewModelCountries = await ConvertModelToViewModel(countriesData, x => new VM_Country(x));
            await UpdateCollection(Countries, viewModelCountries);

            var partsData = await db.GetParts();
            var viewModelParts = await ConvertModelToViewModel(partsData, x => new VM_PartOfTheWorld(x));
            await UpdateCollection(Parts, viewModelParts);
        }
        [ObservableProperty] VM_PartOfTheWorld selectedPart;
        
        [RelayCommand] private async Task RemovePart(VM_PartOfTheWorld part)
        {
            try
            {
                if (part == null) return; 
                MessageBoxResult result = MessageBox.Show($"Вы уверены, что хотите удалить все страны из {part.Name} и контитент включительно?", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    await db.RemovePart(part.Model);

                    var countriesData = await db.GetCountry();
                    var viewModelCountries = await ConvertModelToViewModel(countriesData, x => new VM_Country(x));
                    await UpdateCollection(Countries, viewModelCountries);
                    
                    var partsData = await db.GetParts();
                    var viewModelParts = await ConvertModelToViewModel(partsData, x => new VM_PartOfTheWorld(x));
                    await UpdateCollection(Parts, viewModelParts);
                }
            }
            catch { }
        }
        [RelayCommand] private async Task UpdatePart(VM_PartOfTheWorld part)
        {
            try
            {


                var countriesData = await db.GetCountry();
                var viewModelCountries = await ConvertModelToViewModel(countriesData, x => new VM_Country(x));
                await UpdateCollection(Countries, viewModelCountries);
            }
            catch { }
        }
        [RelayCommand] private async Task AddPart()
        {
            try
            {
                VM_UP_ADD_Part vm = new();
                V_UP_Add_Part v = new();
                v.DataContext = vm;
                if (v.ShowDialog() == true)
                {
                    await db.AddPart(vm.Part.Model);

                    Parts.Insert(0, vm.Part);
                }
            }
            catch { }
        }

        [RelayCommand] private async void RemoveCountrie(VM_Country country)
        {
            try
            {
                MessageBoxResult result = MessageBox.Show($"Вы уверены, что хотите удалить страну {country.Name}?", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    await db.RemoveCountry(country.Model);
                    Countries.Remove(country);
                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message,"Error");
            }
        }
        [RelayCommand] private async Task UpdateCountrie(VM_Country country)
        {
            try
            {
                VM_Country c = new VM_Country(country);
                VM_UpdateCountrie vm = new(c,Parts);
                V_UpdateCountrie v = new();
                v.DataContext = vm;
                if (v.ShowDialog() == true)
                {
                    await db.UpdateCountry(country.Model, vm.Country.Model);
                    country.Update(vm.Country);
                }
            }
            catch { }
        }
        [RelayCommand] private async Task AddCountrie()
        {
            try
            {
                VM_UpdateCountrie vm = new(new(), Parts);
                V_UpdateCountrie v = new();
                v.DataContext = vm;
                if (v.ShowDialog() == true)
                {
                    await db.AddCountry(vm.Country.Model);

                    Countries.Insert(0,vm.Country);
                }
            }
            catch { }
        }
    }
}
