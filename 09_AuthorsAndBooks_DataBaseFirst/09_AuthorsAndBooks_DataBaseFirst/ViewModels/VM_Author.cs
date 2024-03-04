using CommunityToolkit.Mvvm.ComponentModel;
using DB_Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _09_AuthorsAndBooks_DataBaseFirst.ViewModels
{
    public partial class VM_Author: ObservableObject
    {
        [ObservableProperty] Author author;
        public VM_Author(Author author) => Author = author;
        public int Id
        {
            get { return Author.Id; }
        }
        public string FullName
        {
            get { return Author.FullName; }
            set { Author.FullName = value; OnPropertyChanged(nameof(FullName)); }
        }

        public override string ToString()
        {
            return FullName;
        }
    }
}
