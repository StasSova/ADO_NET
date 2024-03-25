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

namespace _13_Exam_BookShop.ViewModels.Pages.ViewModel;

public partial class VM_Main: VM_Page
{
    public IDataBaseAPI API { get; set; }
    public VM_Main()
    {
        API = new DataBaseAPI_D();
        Page = 1;
        Books = new();
        FetchData().Wait();
    }
    public int BookPerPage = 10;
    public int Page;

    [ObservableProperty] ObservableCollection<VM_BookForSale> books;
    [ObservableProperty] ObservableCollection<VM_Genre> genres;

    [ObservableProperty] VM_BookForSale selectedBook;
    async Task FetchData()
    {
        var res = (await API
            .Get<M_BookForSale>(Page, BookPerPage))
            .Select(x => new VM_BookForSale(x));
        Books.Clear();
        foreach (var item in res)
        {
            Books.Add(item);
        }
    }

    public async Task<List<VM_Genre>> GetGenres()
    {
        Genres = new ObservableCollection<VM_Genre>((await API.Get<M_Ganre>()).Select(x => new VM_Genre(x)));
        return Genres.ToList();
    }

    public async Task GetBooksByGenre(int genreId)
    {
        var res = (await API .Get<M_BookForSale>());

        var dict = new Dictionary<string, (object value, bool isExactMatch)>();
        dict.Add("Id", (genreId, true));

        M_Ganre genre = (await API.Get<M_Ganre>(dict)).First();

        var res2 = res.Where(book => book.Ganres.Contains(genre));
        Books.Clear();
        foreach (var item in res2)
        {
            Books.Add(new(item));
        }
    }

    [RelayCommand] void AddToCart(int idBook)
    {

    }

    [RelayCommand] void AddToLike(int idBook)
    {

    }

}
