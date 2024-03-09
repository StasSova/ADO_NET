using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseModels
{
    public class M_Position : DbEntity, IEquatable<M_Position>
    {
        public M_Position() 
        {
            PositionName = "New position";
        }
        public M_Position(M_Position position, bool withId = false)
        {
            if (withId)
                this.Id = position.Id;
            this.PositionName = position.PositionName;
            this.Employees = position.Employees;
        }
        public string PositionName {  get; set; }
        public virtual ICollection<M_Employee> Employees { get; set; } = new List<M_Employee>();
        public new object Clone()
        {
            return new M_Position(this);
        }

        public bool Equals(M_Position? other)
        {
            return PositionName == other?.PositionName;
        }
    }
}
