using CommunityToolkit.Mvvm.ComponentModel;
using DB_Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _09_AuthorsAndBooks_DataBaseFirst.ViewModels
{
    public partial class VM_Book: ObservableObject
    {
        [ObservableProperty] Book book;
        public VM_Book(Book book) => Book = book;
        public int Id
        {
            get { return Book.Id; }
        }
        public string Name
        {
            get { return Book.Name; }
            set { Book.Name = value; OnPropertyChanged(nameof(Name)); }
        }


        private VM_Author author;
        public VM_Author Author 
        {
            get 
            {
                if (author == null)
                    author = new VM_Author(Book.Author);
                return author; 
            }
            set
            {
                SetProperty(ref author, value);
                Book.Author = value.Author;
            }
        }

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}
