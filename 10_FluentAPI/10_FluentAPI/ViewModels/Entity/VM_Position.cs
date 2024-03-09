using CommunityToolkit.Mvvm.ComponentModel;
using DataBaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _10_FluentAPI.ViewModels.Entity
{
    public partial class VM_Position : ObservableObject
    {
        [ObservableProperty] M_Position position;
        public VM_Position() 
        {
            this.Position = new M_Position();
        }
        public VM_Position(M_Position position) 
        {
            this.Position = position;
        }
        public VM_Position(VM_Position position, bool withId = false) 
        {
            this.Position = new M_Position(position.Position, withId);
        }
        public string PositionName
        {
            get { return Position.PositionName; }
            set { Position.PositionName = value; OnPropertyChanged(nameof(PositionName)); }
        }
        public override string ToString() 
        {
            return PositionName;
        }
    }
}
