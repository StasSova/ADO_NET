using BookStore_DbContext.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _13_Exam_BookShop.ViewModels.DbViewModels;

public partial class VM_Book : VM_Entity
{
    [ObservableProperty] M_Book model;
    public VM_Book(M_Book bookForSale)
    {
        Model = bookForSale;
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
    
    public string Title
    {
        get { return Model.Title; }
        set
        {
            if (Model.Title != value)
            {
                Model.Title = value;
                OnPropertyChanged(nameof(Title));
            }
        }
    }
    public string Description
    {
        get { return Model.Description; }
        set
        {
            if (Model.Description != value)
            {
                Model.Description = value;
                OnPropertyChanged(nameof(Description));
            }
        }
    }
    public int NumberOfPage
    {
        get { return Model.NumberOfPage; }
        set
        {
            if (Model.NumberOfPage != value)
            {
                Model.NumberOfPage = value;
                OnPropertyChanged(nameof(NumberOfPage));
            }
        }
    }
    public DateTime DateOfPress
    {
        get { return Model.DateOfPress; }
        set
        {
            if (Model.DateOfPress != value)
            {
                Model.DateOfPress = value;
                OnPropertyChanged(nameof(DateOfPress));
            }
        }
    }

    public int? PublishingHouseId
    {
        get { return Model.PublishingHouseId; }
        protected set
        {
            if (Model.PublishingHouseId != value)
            {
                Model.PublishingHouseId = value;
                OnPropertyChanged(nameof(PublishingHouseId));
            }
        }
    }
    public VM_PublishingHouse PublishingHouse
    {
        get { return new (Model.PublishingHouse); }
        set
        {
            if (Model.PublishingHouse != value.Model)
            {
                Model.PublishingHouse = value.Model;
                this.PublishingHouseId = value.Model.Id;
                OnPropertyChanged(nameof(PublishingHouse));
            }
        }
    }

    public int? AuthorId
    {
        get { return Model.AuthorId; }
        protected set
        {
            if (Model.AuthorId != value)
            {
                Model.AuthorId = value;
                OnPropertyChanged(nameof(AuthorId));
            }
        }
    }
    public VM_Author Author
    {
        get { return new(Model.Author); }
        set
        {
            if (Model.Author != value.Model)
            {
                Model.Author = value.Model;
                this.AuthorId = value.Model.Id;
                OnPropertyChanged(nameof(Author));
            }
        }
    }

    public VM_BookForSale BookForSale 
    {
        get { return new(Model.BookForSale); }
    }
    public ObservableCollection<VM_Genre> Ganres
    {
        get
        {
            var collection = new ObservableCollection<VM_Genre>();
            try
            {
                var db = Model.Ganres;
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
