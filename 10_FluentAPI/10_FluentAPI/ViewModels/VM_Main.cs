using _10_FluentAPI.ViewModels.Entity;
using CommunityToolkit.Mvvm.ComponentModel;
using DataBaseAPI;
using DataBaseModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _10_FluentAPI.ViewModels
{
    public class VM_Main : ObservableObject
    {
        public ObservableCollection<VM_Employee> Employees { get; set; } = new();
        public ObservableCollection<VM_Position> Positions { get; set; } = new();
        public IDataBaseAPI DbAPI { get; private set; }
        public VM_Main(IDataBaseAPI dataBaseAPI)
        {
            this.DbAPI = dataBaseAPI;
            FetchData().Wait();
        }
        async Task FetchData()
        {
            try
            {
                List<VM_Position> pos = (await DbAPI.Get<M_Position>()).Select(e => new VM_Position(e)).ToList();

                List<VM_Employee> test = (await DbAPI
                            .Get<M_Employee>())
                            //.Get<M_Employee>(2, 5))
                            //.Get<M_Employee,M_Position>(nameof(M_Employee.Position), pos[0].Position))
                            .Select(e => new VM_Employee(e))
                            .ToList();



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
