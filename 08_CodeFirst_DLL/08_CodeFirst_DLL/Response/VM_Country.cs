using _08_CodeFirst_DLL.Service;
using _08_CodeFirst_DLL.ViewModel;
using _08_CodeFirst_DLL_Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _08_CodeFirst_DLL.Response
{
    public partial class VM_Country : VM_Base
    {
        public VM_Country() 
        {
            Model = new Country();
        }
        public VM_Country(Country country) 
        {
            this.Model = country;
            PartOfTheWorld = new(country.PartOfTheWorld);
        }
        public VM_Country(VM_Country country)
        {
            this.Model = new Country(country.Model);
            PartOfTheWorld = country.PartOfTheWorld;
        }

        public void Update(VM_Country country)
        {
            Name = country.Name;
            Capital = country.Capital;
            NumberOfInhabitants = country.NumberOfInhabitants;
            CountryArea = country.CountryArea;
            PartOfTheWorld = country.PartOfTheWorld;
        }
        [ObservableProperty]private Country model;
        public int Id 
        {
            get => Model.Id;
            set { Model.Id = value; OnPropertyChanged(nameof(Id)); }
        }
        public string Name
        {
            get => Model.Name;
            set { Model.Name = value; OnPropertyChanged(nameof(Name)); }
        }
        public string Capital
        {
            get => Model.Capital;
            set { Model.Capital = value; OnPropertyChanged(nameof(Capital)); }
        }
        public int? NumberOfInhabitants
        {
            get => Model.NumberOfInhabitants;
            set { Model.NumberOfInhabitants = value; OnPropertyChanged(nameof(NumberOfInhabitants)); }
        }
        public double? CountryArea
        {
            get => Model.CountryArea;
            set { Model.CountryArea = value; OnPropertyChanged(nameof(CountryArea)); }
        }
        private VM_PartOfTheWorld? part;
        public VM_PartOfTheWorld? PartOfTheWorld
        {
            get => part;
            set { part = value; Model.PartOfTheWorld = value?.Model; OnPropertyChanged(nameof(PartOfTheWorld)); }
        }
    }
}
