using CommunityToolkit.Mvvm.ComponentModel;
using DataBaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _10_FluentAPI.ViewModels.Entity
{
    public partial class VM_Employee : ObservableObject
    {
        [ObservableProperty] M_Employee employee;
        public VM_Employee() 
        {
            this.Employee = new M_Employee();
        }
        public VM_Employee(VM_Employee employee) 
        {
            this.Employee = new M_Employee(employee.Employee);
        }
        public VM_Employee(M_Employee employee)
        {
            this.Employee = employee;
        }
        public string Name 
        { 
            get { return Employee.Name; }
            set { Employee.Name = value; OnPropertyChanged(nameof(Name)); }
        }
        public string Surname
        {
            get { return Employee.Surname; }
            set { Employee.Surname = value; OnPropertyChanged(nameof(Surname)); }
        }
        public M_Position Position 
        { 
            get { return Employee.Position; }
            set { Employee.Position = value; OnPropertyChanged( nameof(Position));}
        }
    }
}
