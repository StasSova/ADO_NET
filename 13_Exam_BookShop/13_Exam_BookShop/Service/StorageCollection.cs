using _13_Exam_BookShop.ViewModels;
using _13_Exam_BookShop.ViewModels.DbViewModels;
using BookStore_DbContext.Interface;
using BookStore_DbContext.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace _13_Exam_BookShop.Service;

public partial class StorageCollection : VM_Base
{
    private static StorageCollection instance;
    public static StorageCollection Instance => instance ??= new StorageCollection();
    public static IDataBaseAPI API { get; set; }

    [ObservableProperty] ObservableCollection<VM_BookForSale> books = new();
    [ObservableProperty] ObservableCollection<VM_Genre> genres = new();
    [ObservableProperty] ObservableCollection<VM_Author> authors = new();

    private StorageCollection()
    {
        InitializeAsync();
    }
    public async Task InitializeAsync()
    {
        API = new DataBaseAPI_D();
        await FetchData();
    }
    private async Task FetchData()
    {
        //var booksRes = (await API.Get<M_BookForSale>()).Select(x => new VM_BookForSale(x));
        //var genresRes = (await API.Get<M_Ganre>()).Select(x => new VM_Genre(x));
        //var authorsRes = (await API.Get<M_Author>()).Select(x => new VM_Author(x));

        //Books = new ObservableCollection<VM_BookForSale>(booksRes);
        //Genres = new ObservableCollection<VM_Genre>(genresRes);
        //Authors = new ObservableCollection<VM_Author>(authorsRes);
    }

    public async Task<List<VM_BookForSale>> GetBooks()
    {
        var booksRes = (await API.Get<M_BookForSale>()).Select(x => new VM_BookForSale(x));
        Books = new ObservableCollection<VM_BookForSale>(booksRes);
        return Books.ToList();
    }
    public async Task<List<VM_Genre>> GetGenres()
    {
        var genresRes = (await API.Get<M_Ganre>()).Select(x => new VM_Genre(x));
        Genres = new ObservableCollection<VM_Genre>(genresRes);
        return Genres.ToList();
    }
    public async Task<List<VM_Author>> GetAuthors()
    {
        var authorsRes = (await API.Get<M_Author>()).Select(x => new VM_Author(x));
        Authors = new ObservableCollection<VM_Author>(authorsRes);
        return Authors.ToList();
    }

    public async Task<ICollection<VM_BookForSale>> GetBooksByGenre(int genreId)
    {
        var books = await API.Get<M_BookForSale>();
        var filteredBooks = books.Where(b => b.Ganres.Any(g => g.Id == genreId));
        return filteredBooks.Select(b => new VM_BookForSale(b)).ToList();
    }

    public async Task<ICollection<VM_BookForSale>> GetBooksByAuthor(int authorId)
    {
        var books = await API.Get<M_BookForSale>();
        var filteredBooks = books.Where(b => b.AuthorId == authorId);
        return filteredBooks.Select(b => new VM_BookForSale(b)).ToList();
    }

    public async Task<ICollection<VM_BookForSale>> GetBooksOnSale()
    {
        var books = await API.Get<M_BookForSale>();
        var onSaleBooks = books.Where(b => b.Discount > 0);
        return onSaleBooks.Select(b => new VM_BookForSale(b)).ToList();
    }

    public async Task<ICollection<VM_BookForSale>> GetBooksByPage(int page, int perPage)
    {
        var books = await API.Get<M_BookForSale>();
        var pagedBooks = books.Skip((page - 1) * perPage).Take(perPage);
        return pagedBooks.Select(b => new VM_BookForSale(b)).ToList();
    }

    public async Task<ICollection<VM_BookForSale>> GetBooksByTitle(string title, bool exactMatch)
    {
        var books = await API.Get<M_BookForSale>();
        var filteredBooks = exactMatch ?
            books.Where(b => b.Title == title) :
            books.Where(b => b.Title.Contains(title));
        return filteredBooks.Select(b => new VM_BookForSale(b)).ToList();
    }

    public async Task<ICollection<VM_BookForSale>> GetRecentAddedBooks(int topCount)
    {
        var books = await API.Get<M_BookForSale>();
        var recentBooks = books.OrderByDescending(b => b.DateOfPress).Take(topCount);
        return recentBooks.Select(b => new VM_BookForSale(b)).ToList();
    }

    public async Task<ICollection<VM_BookForSale>> GetPopularBooks(int topCount)
    {
        var books = await API.Get<M_BookForSale>();
        var popularBooks = books.OrderByDescending(b => b.ShoppingCarts.Count).Take(topCount);
        return popularBooks.Select(b => new VM_BookForSale(b)).ToList();
    }
    public async Task<ICollection<VM_Genre>> GetPopularGenresByPeriod(DateTime startDate, DateTime endDate, int topCount)
    {
        var books = await API.Get<M_BookForSale>();
        var filteredBooks = books.Where(b => b.DateOfPress >= startDate && b.DateOfPress <= endDate);

        var popularGenres = filteredBooks
            .SelectMany(b => b.Ganres)
            .GroupBy(g => g.Id)
            .OrderByDescending(g => g.Count())
            .Take(topCount)
            .Select(g => g.Key)
            .ToList();

        var genres = await API.Get<M_Ganre>();
        List<M_Ganre> popularGenresDetails = genres
            .Where(g => popularGenres.Contains(g.Id))
            .ToList();


        return popularGenresDetails.Select(g => new VM_Genre(g)).ToList();
    }

}