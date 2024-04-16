using BookContext.Interfaces;
using BookContext.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Exam3_0.Models;
using Exam3_0.Service;
using Exam3_0.View.Books;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Exam3_0.View.Book;

public partial class VM_Book_Page : VM_Page
{
    private int originId;
    [ObservableProperty] VM_Book book;
    [ObservableProperty] VM_PublishingHouse house;
    [ObservableProperty] VM_Author author;
    [ObservableProperty] ObservableCollection<VM_Genre> genres;
    [ObservableProperty] List<VM_PublishingHouse> houses;
    [ObservableProperty] List<VM_Author> authors;
    private IDataBaseAPI API { get; set; }
    public VM_Book_Page()
    {
        this.API = APIProvider.API;
    }

    // parameter contains id of Book
    public override async void Initialize(object parameter)
    {
        base.Initialize(parameter);
        Book = new VM_Book(new BookContext.Models.M_Book());
        if (parameter != null)
        {
            originId = (int)parameter;
            var dbB = new VM_Book(await API.Context.Books.Where(x => x.Id == (int)parameter).FirstAsync());
            Book.Title = dbB.Title;
            Book.Image = dbB.Image;
            Book.Description = dbB.Description;
            Book.NumberOfPage = dbB.NumberOfPage;
            Book.DateOfPress = dbB.DateOfPress;
            Book.PublishingHouseId = dbB.PublishingHouseId;
            Book.AuthorId = dbB.AuthorId;
            Book.SellingPrice = dbB.CostPrice;
            Book.SellingPrice = dbB.SellingPrice;
            Book.Discount = dbB.Discount;

            Book.Genres = dbB.Genres;
            Book.Author = dbB.Author;
        }

        // Заполняем словарь жанров
        var genreList = await API.Context.Genres.Select(x => new VM_Genre(x)).ToListAsync();
        Genres = new ObservableCollection<VM_Genre>(genreList);
        var bookGenres = Book.Genres.Select(x => x.Id);
        foreach (var genre in genreList)
        {
            genre.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(VM_Genre.IsSelected))
                    OnPropertyChanged(nameof(IsAllItemsSelected));
            };
            if (bookGenres.Contains(genre.Id))
            {
                genre.IsSelected = true;
            }
        }

        // Заполняем список авторов
        Authors = await API.Context.Authors.Select(x => new VM_Author(x)).ToListAsync();
        Author = parameter == null
            ? Authors[0]
            : Authors.FirstOrDefault(x => x.Id == Book.AuthorId) ?? Authors[0];

        // Заполняем список издательств
        Houses = await API.Context.PublishingHouses.Select(x => new VM_PublishingHouse(x)).ToListAsync();
        House = parameter == null
            ? Houses[0]
            : Houses.FirstOrDefault(x => x.Id == Book.PublishingHouseId) ?? Houses[0];
    }
    public bool? IsAllItemsSelected
    {
        get
        {
            var selected = Genres?.Select(x => x.IsSelected).Distinct().ToList();
            return selected?.Count == 1 ? selected.Single() : (bool?)null;
        }
        set
        {
            if (value.HasValue)
            {
                SelectAll(value.Value, Genres);
                OnPropertyChanged();
            }
        }
    }
    private static void SelectAll(bool select, IEnumerable<VM_Genre> models)
    {
        foreach (var model in models)
        {
            model.IsSelected = select;
        }
    }

    [RelayCommand]
    async Task EditBook()
    {
        try
        {
            var selected = Genres?.Where(x => x.IsSelected).ToList();
            var dbB = new VM_Book(await API.Context.Books.Where(x => x.Id == originId).FirstAsync());
            dbB.UpdateGenres(selected);
            dbB.Title = Book.Title;
            dbB.Image = Book.Image;
            dbB.Description = Book.Description;
            dbB.NumberOfPage = Book.NumberOfPage;
            dbB.DateOfPress = Book.DateOfPress;
            dbB.SellingPrice = Book.CostPrice;
            dbB.SellingPrice = Book.SellingPrice;
            dbB.Discount = Book.Discount;

            dbB.PublishingHouseId = House.Id;
            dbB.AuthorId = Author.Id;
            API.Context.Update(dbB.Model);
            await API.Context.SaveChangesAsync();

            NavigationPageService.Instance.NavigateTo<VM_Books_Page>("Index");
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    [RelayCommand]
    async Task CreateBook()
    {
        try
        {
            var selected = Genres?.Where(x => x.IsSelected).ToList();
            Book.UpdateGenres(selected);
            Book.Author = Author;
            Book.AuthorId = Author.Id;
            Book.PublishingHouseId = House.Id;
            Book.PublishingHouse = House;

            await API.Context.Books.AddAsync(Book.Model);
            await API.Context.SaveChangesAsync();
        }
        catch
        {

        }
    }

    [RelayCommand]
    Task GoBack()
    {
        try
        {
            NavigationPageService.Instance.NavigateTo<VM_Books_Page>("Index");
        }
        catch
        {

        }
        return Task.CompletedTask;
    }
}
