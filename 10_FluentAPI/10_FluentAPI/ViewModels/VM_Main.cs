using _10_FluentAPI.ViewModels.Entity;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DataBaseAPI;
using DataBaseModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace _10_FluentAPI.ViewModels
{
    public partial class VM_Main : ObservableObject
    {
        public ObservableCollection<VM_Employee> Employees { get; set; } = new();
        public ObservableCollection<VM_Position> Positions { get; set; } = new();

        [ObservableProperty] string filterName;
        [ObservableProperty] bool filterNameFull;

        [ObservableProperty] string filterSurname;
        [ObservableProperty] bool filterSurnameFull;

        [ObservableProperty] bool filterByPositionCheck;
        VM_Position filterPosition;
        public VM_Position FilterPosition
        {
            get { return filterPosition; }
            set
            {
                if (value == null) return;
                SelectedPosition = value;
                filterPosition = new (value,true);
                if (value.Position.Id != default)
                {
                    DeletePositionCommand.NotifyCanExecuteChanged();
                }
                AddPositionCommand.NotifyCanExecuteChanged();
                UpdatePositionCommand.NotifyCanExecuteChanged();

                OnPropertyChanged(nameof(FilterPosition));
            }
        }


        //[ObservableProperty] 
        VM_Employee selectedEmployee = new();
        public VM_Employee SelectedEmployee
        {
            get { return selectedEmployee; }
            set
            {
                if (value == null) { return; }
                selectedEmployee = value;
                AddEmployeeCommand.NotifyCanExecuteChanged();
                UpdateEmployeeCommand.NotifyCanExecuteChanged();
                OnPropertyChanged(nameof(SelectedEmployee));
            }
        }
        [RelayCommand] void ClearFilters()
        {
            FilterNameFull = false;
            FilterSurnameFull = false;
            FilterName = string.Empty;
            FilterSurname = string.Empty;
            FilterPosition = null;
        }
        [RelayCommand] async Task Search()
        {
            try
            {
                var propertyFilters = new Dictionary<string, (object value, bool isExactMatch)>();
                if (!string.IsNullOrWhiteSpace(FilterName)){
                    propertyFilters.Add(nameof(SelectedEmployee.Employee.Name), (FilterName, FilterNameFull));
                }
                if (!string.IsNullOrWhiteSpace(FilterSurname))
                {
                    propertyFilters.Add(nameof(SelectedEmployee.Employee.Surname), (FilterSurname, FilterSurnameFull));
                }
                if (FilterPosition != null && FilterByPositionCheck)
                {
                    propertyFilters.Add(nameof(SelectedEmployee.Employee.PositionId), (SelectedPosition.Position.Id, true));
                }
                if (propertyFilters.Count == 0)
                {
                    List<VM_Employee> empl = (await DbAPI.Get<M_Employee>()).Select(e => new VM_Employee(e)).ToList();
                    Employees.Clear();
                    foreach (VM_Employee e in empl)
                        Employees.Add(e);
                    return;
                }
                List<VM_Employee> result = (await DbAPI.Get<M_Employee>(propertyFilters)).Select(x => new VM_Employee(x)).ToList();
                Employees.Clear();

                foreach (VM_Employee e in result)
                    Employees.Add(e);
            }
            catch { }
        }
        


        [RelayCommand] void NotifySelectedEmployeePropertyChanged()
        {
            AddEmployeeCommand.NotifyCanExecuteChanged();
        }
        [RelayCommand(CanExecute = nameof(CanAddEmployee))] 
        async Task AddEmployee()
        {
            try
            {
                if (!CanAddEmployee()) return;
                //if (SelectedEmployee.Employee.Id != default)
                //    SelectedEmployee = new VM_Employee(SelectedEmployee);
                await DbAPI.Add<M_Employee>(SelectedEmployee.Employee);
                MessageBox.Show("New employee added");
                Employees.Insert(0,SelectedEmployee);
                SelectedEmployee = new();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        bool CanAddEmployee()
        {
            return  !string.IsNullOrWhiteSpace(SelectedEmployee.Name) &&
                    !string.IsNullOrWhiteSpace(SelectedEmployee.Surname) &&
                    SelectedEmployee.Position != null;
        }
        [RelayCommand] async Task DeleteEmployee(VM_Employee employee)
        {
            try
            {
                // Показать диалоговое окно подтверждения
                MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this employee?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

                // Проверить, было ли нажато "Yes"
                if (result == MessageBoxResult.Yes)
                {
                    // Удалить сотрудника из базы данных
                    await DbAPI.Delete<M_Employee>(employee.Employee);
                    MessageBox.Show("Employee removed");

                    // Удалить сотрудника из коллекции
                    Employees.Remove(Employees.First(x => x.Employee.Id == employee.Employee.Id));
                    SelectedEmployee = new();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        [RelayCommand] async Task ChangeStateEmployeeToEditing(VM_Employee employee)
        {
            try
            {
                SelectedEmployee = new VM_Employee(employee, true);
                SelectedEmployee.Position = Positions.FirstOrDefault(x => x.Position.Id == SelectedEmployee.Position.Position.Id);
            }
            catch { }
        }
        [RelayCommand (CanExecute = nameof(CanUpdateEmployee))] async Task UpdateEmployee()
        {
            try
            {
                await DbAPI.Update<M_Employee>(SelectedEmployee.Employee.Id, SelectedEmployee.Employee);
                VM_Employee update = Employees.FirstOrDefault(x => x.Employee.Id == SelectedEmployee.Employee.Id)!;
                update.Name = SelectedEmployee.Name;
                update.Surname = SelectedEmployee.Surname;
                update.Position = SelectedEmployee.Position;

                SelectedEmployee = new();
            }
            catch { }
        }
        bool CanUpdateEmployee()
        {
            return SelectedEmployee.Employee.Id != default;
        }




        VM_Position selectedPosition = new();
        public VM_Position SelectedPosition
        {
            get { return selectedPosition; }
            set
            {
                selectedPosition = new (value,true);
                if (value.Position.Id != default)
                {
                    DeletePositionCommand.NotifyCanExecuteChanged();
                }
                AddPositionCommand.NotifyCanExecuteChanged();
                UpdatePositionCommand.NotifyCanExecuteChanged();

                OnPropertyChanged(nameof(SelectedPosition));
            }
        }

        [RelayCommand] void NotifySelectedPositionPropertyChanged()
        {
            AddPositionCommand.NotifyCanExecuteChanged();
            UpdatePositionCommand.NotifyCanExecuteChanged();
            DeletePositionCommand.NotifyCanExecuteChanged();
        }

        [RelayCommand(CanExecute = nameof(CanAddPosition))] async Task AddPosition()
        {
            try
            {
                if (!CanAddPosition()) return;

                if (Positions.Select(x => x.PositionName).Contains(SelectedPosition.PositionName))
                    throw new Exception("Position already exists.");

                if (SelectedPosition.Position.Id != default)
                    SelectedPosition = new VM_Position(SelectedPosition);


                await DbAPI.Add<M_Position>(SelectedPosition.Position);
                MessageBox.Show("New position added");
                Positions.Insert(0, SelectedPosition);
                SelectedPosition = new();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        bool CanAddPosition()
        {
            return !string.IsNullOrWhiteSpace(SelectedPosition.PositionName);
        }

        [RelayCommand(CanExecute = nameof(CanUpdatePostion))] async Task UpdatePosition()
        {
            if (!CanUpdatePostion()) return;
            try
            {
                await DbAPI.Update<M_Position>(SelectedPosition.Position.Id, SelectedPosition.Position);
                VM_Position update = Positions.FirstOrDefault(x => x.Position.Id == SelectedPosition.Position.Id)!;

                update.Position = SelectedPosition.Position;
                SelectedPosition = new();

                await FetchData();
            }
            catch { }
        }
        bool CanUpdatePostion()
        {
            return !string.IsNullOrWhiteSpace(SelectedPosition.PositionName) &&
                    SelectedPosition.Position.Id != default;
        }

        [RelayCommand(CanExecute = nameof(CanDeletePosition))]
        async Task DeletePosition()
        {
            if (!CanDeletePosition()) return;
            try
            {
                // Показать диалоговое окно подтверждения
                MessageBoxResult result = MessageBox.Show($"Are you sure you want to delete this position and { Employees.Where(e => e.Position.Position.Id == SelectedPosition.Position.Id).Count()} employees?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

                // Проверить, было ли нажато "Yes"
                if (result == MessageBoxResult.Yes)
                {
                    // Удалить должность из базы данных
                    await DbAPI.Delete<M_Position>(SelectedPosition.Position.Id);
                    MessageBox.Show("Position removed");

                    // Удалить должность из коллекции
                    SelectedPosition = new();
                    await FetchData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        bool CanDeletePosition() {  return SelectedPosition.Position.Id != default; }



        public IDataBaseAPI DbAPI { get; private set; }
        public VM_Main()
        {
        }
        public VM_Main(IDataBaseAPI dataBaseAPI) :base()
        {
            this.DbAPI = dataBaseAPI;
            FetchData().Wait();
        }
        async Task FetchData()
        {
            try
            {
                //List<VM_Employee> test = (await DbAPI
                //            .Get<M_Employee>())
                //            //.Get<M_Employee>(2, 5))
                //            //.Get<M_Employee,M_Position>(nameof(M_Employee.Position), pos[0].Position))
                //            .Select(e => new VM_Employee(e))
                //            .ToList();

                List<M_Position> model = await DbAPI.Get<M_Position>();
                List<VM_Position> pos = model.Select(e => new VM_Position(e)).ToList();
                FilterPosition = null;
                Positions.Clear();
                foreach (VM_Position p in pos)
                    Positions.Add(p);

                List<VM_Employee> empl = (await DbAPI.Get<M_Employee>()).Select(e => new VM_Employee(e)).ToList();
                Employees.Clear();
                foreach (VM_Employee e in empl)
                    Employees.Add(e);
            }
            catch (Exception)
            {
                
            } 
        }
    }
}
