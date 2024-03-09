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
        public VM_Employee(VM_Employee employee, bool withId = false) 
        {
            this.Employee = new M_Employee(employee.Employee, withId);
            this.Position = employee.Position;
        }
        public VM_Employee(M_Employee employee)
        {
            this.Employee = employee;
            this.Position = new(employee.Position);
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

        private VM_Position? pos;
        public VM_Position Position 
        { 
            get { return pos!; }
            set 
            { 
                pos = value; 
                Employee.Position = value.Position;
                Employee.PositionId = value.Position.Id;
                OnPropertyChanged( nameof(Position));
            }
        }
    }
}
