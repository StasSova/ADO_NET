using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _09_AuthorsAndBooks_DataBaseFirst.ViewModels
{
    public partial class VM_Book_Detail:ObservableObject
    {
        [ObservableProperty] VM_Book book;
        public ObservableCollection<VM_Author> Authors {  get; set; }
        [ObservableProperty] string title;
        public VM_Book_Detail() { }
        public VM_Book_Detail(string title,ObservableCollection<VM_Author> authors, VM_Book book = null)
        {
            Title = title;
            Authors = authors;
            if (book != null)
            {
                Book = book;
                Book.Author = Authors.FirstOrDefault(x => x.Author.Id == Book.Author.Id);
            }
            else
            {
                Book = new VM_Book(new DB_Context.Book())
                {
                    Name = "Book name",
                };
            }
        }

    }
}
