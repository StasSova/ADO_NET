using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam3_0.Models;

public class VM_Entity : ObservableObject
{
    public int Id { get; set; }

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        return Id == ((VM_Entity)obj).Id;
    }

    public override int GetHashCode()
    {
        return Id;
    }
}