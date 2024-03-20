using BookStore_DbContext.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _13_Exam_BookShop.ViewModels.DbViewModels;

public partial class VM_Genre : VM_Entity
{
    [ObservableProperty] M_Ganre model;
    public VM_Genre(M_Ganre ganre)
    {
        Model = ganre;
    }
    public int Id
    {
        get { return Model.Id; }
        set
        {
            if (Model.Id != value)
            {
                Model.Id = value;
                OnPropertyChanged(nameof(Id));
            }
        }
    }
    public string Genre
    {
        get { return Model.Genre; }
        set
        {
            if (Model.Genre != value)
            {
                Model.Genre = value;
                OnPropertyChanged(nameof(Genre));
            }
        }
    }

    public ObservableCollection<VM_Book> Books
    {
        get
        {
            var collection = new ObservableCollection<VM_Book>();
            try
            {
                var db = Model.Books;
                foreach (var book in db)
                {
                    collection.Add(new(book));
                }
            }
            catch { }
            return collection;
        }
    }
}
