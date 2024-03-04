using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _09_AuthorsAndBooks_DataBaseFirst.ViewModels
{
    public partial class VM_Author_Detail:ObservableObject
    {
        [ObservableProperty] VM_Author author;
        [ObservableProperty] string title;
        public VM_Author_Detail() { }
        public VM_Author_Detail(string title, VM_Author Author = null)
        {
            Title = title;
            if (Author == null) 
            {
                this.Author = new VM_Author(new())
                {
                    FullName = "Author Name"
                };
            }
            else
            {
                this.Author = Author;
            }
        }

    }
}
