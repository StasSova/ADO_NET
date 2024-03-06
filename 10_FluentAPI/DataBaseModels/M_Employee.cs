using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseModels
{
    public class M_Employee : DbEntity,  IEquatable<M_Employee>
    {
        public string Name {  get; set; }
        public string Surname { get; set; }
        public virtual M_Position Position { get; set; }
        public M_Employee() 
        {
            Name = "Name";
            Surname = "Surname";
            Position = new M_Position();
        }
        public M_Employee(M_Employee employee) 
        {
            this.Name = employee.Name;
            this.Surname = employee.Surname;
            this.Position = new M_Position(employee.Position);
        }

        public new object Clone()
        {
            return new M_Employee(this);
        }
        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            M_Employee other = (M_Employee)obj;
            return Name == other.Name && Surname == other.Surname && Position == other.Position;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Surname, Position);
        }
        public bool Equals(M_Employee? other)
        {
            return Name == other?.Name && Surname == other.Surname && Position == other.Position;
        }
    }
}
