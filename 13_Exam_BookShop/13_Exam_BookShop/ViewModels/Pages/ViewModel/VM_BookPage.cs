using _13_Exam_BookShop.Service;
using _13_Exam_BookShop.ViewModels.DbViewModels;
using BookStore_DbContext.Interface;
using BookStore_DbContext.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;
using static System.Reflection.Metadata.BlobBuilder;

namespace _13_Exam_BookShop.ViewModels.Pages.ViewModel;

public partial class VM_BookPage : VM_Page
{
    private int page = 1;
    private int pageSize = 10;

    IDataBaseAPI API;
    [ObservableProperty] VM_User user;
    [ObservableProperty] ObservableCollection<VM_BookForSale> books = new();
    public VM_BookPage()
    {
        API = StorageCollection.API;
        User = VM_User.GetInstance();
        Books = new ObservableCollection<VM_BookForSale>();
        Initialize();
    }

    // Метод инициализации
    private void Initialize()
    {
        _ = GetBook();
    }

    public async Task GetBook()
    {
        var booksList = await API.Get<M_BookForSale>();
        Books = new ObservableCollection<VM_BookForSale>(booksList.Select(book => new VM_BookForSale(book)));
    }

    public async Task GetBooksByGenre(int genreId)
    {

        var books = await API.GetBooksForSaleByGenre(genreId);
        Books = new ObservableCollection<VM_BookForSale>(books.Select(book => new VM_BookForSale(book)));


    }

    public async Task GetBooksByAuthor(int authorId)
    {
        var books = await API.GetBooksForSaleByAuthor(authorId);
        Books = new ObservableCollection<VM_BookForSale>(books.Select(book => new VM_BookForSale(book)));
    }

    public async Task GetPromotionBooks()
    {
        var books = await API.GetPromotionBooks();
        Books = new ObservableCollection<VM_BookForSale>(books.Select(book => new VM_BookForSale(book)));
    }




    public async Task Seacrh(string searchValue)
    {
        try
        {
            ICollection<M_BookForSale> result;
            if (!string.IsNullOrWhiteSpace(searchValue))
            {
                Dictionary<string, (object value, bool isExactMatch)> propertyFilters = new Dictionary<string, (object value, bool isExactMatch)>
                {
                    { "Title", (searchValue, false) },
                };

                result = await API.Get<M_BookForSale>(propertyFilters);
            }
            else
            {
                result = await API.Get<M_BookForSale>();
            }

            Books = new ObservableCollection<VM_BookForSale>(result.Select(book => new VM_BookForSale(book)));
        }
        catch
        {

        }
    }

    [RelayCommand]
    async Task AddToCart(int idBook)
    {
        try
        {
            await API.AddBookToUserCart(User.Id, idBook);
            MessageBox.Show("Книга успешно добавлена в корзину");
        }
        catch
        {

        }
    }
    [RelayCommand]
    async void RemoveBook(int idBook)
    {
        try
        {
            await API.Delete<M_BookForSale>(idBook);
            var booksList = await API.Get<M_BookForSale>();
            Books = new ObservableCollection<VM_BookForSale>(booksList.Select(book => new VM_BookForSale(book)));

            MessageBox.Show("Book is removed");
        }
        catch
        {

        }
    }
    [RelayCommand]
    void EditBook(int idBook)
    {
        try
        {
            NavigationPageService.Instance.NavigateTo<VM_Add_Edit_Book>(idBook);
        }
        catch { }
    }
    [RelayCommand]
    public async Task AddBook()
    {
        try
        {
            NavigationPageService.Instance.NavigateTo<VM_Add_Edit_Book>();
        }
        catch (Exception ex) { }
    }
}

