using BookStore_DbContext.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _13_Exam_BookShop.ViewModels.DbViewModels;

public partial class VM_Author: VM_Entity
{
    [ObservableProperty] M_Author model;
    public VM_Author(M_Author author)
    {
        Model = author;
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
    public string FirstName
    {
        get { return Model.FirstName; }
        set
        {
            if (Model.FirstName != value)
            {
                Model.FirstName = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }
    }
    public string LastName
    {
        get { return Model.LastName; }
        set
        {
            if (Model.LastName != value)
            {
                Model.LastName = value;
                OnPropertyChanged(nameof(LastName));
            }
        }
    }
    public DateTime DateOfBirth
    {
        get { return Model.DateOfBirth; }
        set
        {
            if (Model.DateOfBirth != value)
            {
                Model.DateOfBirth = value;
                OnPropertyChanged(nameof(DateOfBirth));
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
