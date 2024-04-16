using BookContext.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam3_0.Models;

public partial class VM_Book : VM_Entity
{
    [ObservableProperty] M_Book model;
    private VM_PublishingHouse? _publishingHouse;
    private VM_Author? _author;
    private ICollection<VM_Genre>? _genres;
    public VM_Book(M_Book book)
    {
        Model = book;
    }
    [ObservableProperty] bool isSelected;
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
    public string Image
    {
        get { return Model.Image; }
        set
        {
            if (Model.Image != value)
            {
                Model.Image = value;
                OnPropertyChanged(nameof(Image));
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
        set
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
        get
        {
            if (_publishingHouse == null || _publishingHouse.Model != Model.PublishingHouse)
            {
                _publishingHouse = new VM_PublishingHouse(Model.PublishingHouse);
            }
            return _publishingHouse;
        }
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
        set
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
        get
        {
            if (_author == null || _author.Model != Model.Author)
            {
                _author = new VM_Author(Model.Author);
            }
            return _author;
        }
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
    public decimal CostPrice
    {
        get { return Model.CostPrice; }
        set
        {
            if (Model.CostPrice != value)
            {
                Model.CostPrice = value;
                OnPropertyChanged(nameof(CostPrice));
            }
        }
    }
    public decimal SellingPrice
    {
        get { return Model.SellingPrice; }
        set
        {
            if (Model.SellingPrice != value)
            {
                Model.SellingPrice = value;
                OnPropertyChanged(nameof(SellingPrice));
            }
        }
    }
    public decimal Discount
    {
        get { return Model.Discount; }
        set
        {
            if (Model.Discount != value)
            {
                Model.Discount = value;
                OnPropertyChanged(nameof(Discount));
            }
        }
    }
    public ICollection<VM_Genre> Genres
    {
        get
        {
            if (Model.Genres == null)
            {
                return new List<VM_Genre>();
            }
            return _genres ??= Model.Genres.Select(genre => new VM_Genre(genre)).ToList();
        }
        set
        {
            if (_genres != value)
            {
                _genres = value;
                OnPropertyChanged(nameof(Genres));
                Model.Genres = value.Select(vm_genre => vm_genre.Model).ToList();
            }
        }
    }


    public void AddGenre(VM_Genre genre)
    {
        Model.Genres.Add(genre.Model);
        _genres?.Append(genre);
    }
    public void RemoveGenre(VM_Genre genre)
    {
        Model.Genres.Remove(genre.Model);
        if (_genres != null)
        {
            _genres = _genres.Where(g => g.Id != genre.Id).ToList();
        }
    }
    public void UpdateGenres(IEnumerable<VM_Genre> genres)
    {
        Model.Genres.Clear();
        foreach (var genre in genres)
        {
            Model.Genres.Add(genre.Model);
        }
        _genres = null;
    }
}

