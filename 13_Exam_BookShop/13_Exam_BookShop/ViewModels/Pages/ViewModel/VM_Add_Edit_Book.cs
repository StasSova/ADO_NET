using _13_Exam_BookShop.Service;
using _13_Exam_BookShop.ViewModels.DbViewModels;
using BookStore_DbContext.Interface;
using BookStore_DbContext.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _13_Exam_BookShop.ViewModels.Pages.ViewModel;

public partial class VM_Add_Edit_Book : VM_Page
{
    public delegate void InitializedEventHandler();
    public event InitializedEventHandler Initialized;
    protected virtual void OnInitialized()
    {
        Initialized?.Invoke();
    }
    [ObservableProperty] VM_BookForSale book;



    private IDataBaseAPI API { get; set; }
    [ObservableProperty] ObservableCollection<VM_PublishingHouse> publishingHouses;
    [ObservableProperty] ObservableCollection<VM_Author> authors;
    [ObservableProperty] ObservableCollection<VM_Genre> allGenres;
    [ObservableProperty] string title;
    [ObservableProperty] string image;
    [ObservableProperty] string descrtiption;
    [ObservableProperty] int numberOfPage;
    [ObservableProperty] DateTime dateOfPress;
    [ObservableProperty] VM_PublishingHouse publishingHouse;
    [ObservableProperty] VM_Author author;
    [ObservableProperty] ObservableCollection<VM_Genre> genres;
    [ObservableProperty] decimal costPrice;
    [ObservableProperty] decimal sellingPrice;
    [ObservableProperty] decimal discount;

    private bool isUpdate = false;

    [ObservableProperty] VM_BookForSale selectedBook;
    public VM_Add_Edit_Book()
    {
        API = StorageCollection.API;
        var houses = API.Get<M_PublishingHouse>().Result;
        PublishingHouses = new ObservableCollection<VM_PublishingHouse>(houses.Select(x => new VM_PublishingHouse(x)));

        var authors = API.Get<M_Author>().Result;
        Authors = new ObservableCollection<VM_Author>(authors.Select(x => new VM_Author(x)));

        var genres = API.Get<M_Ganre>().Result;
        AllGenres = new ObservableCollection<VM_Genre>(genres.Select(x => new VM_Genre(x)));
        Genres = new ObservableCollection<VM_Genre>();

        Book = new VM_BookForSale(new M_BookForSale());
    }
    public override async void Initialize(object parameter)
    {
        base.Initialize(parameter);
        if (parameter != null)
        {
            isUpdate = true;
            int idBook = (int)parameter;
            await Init(idBook);
        }
    }

    private async Task Init(int idBook)
    {
        API = StorageCollection.API;

        var book = await API.Context.BooksForSale.Where(x => x.Id == idBook).FirstOrDefaultAsync();
        SelectedBook = new(book);
        Title = SelectedBook.Title;
        Image = SelectedBook.Image;
        Descrtiption = SelectedBook.Description;
        NumberOfPage = SelectedBook.NumberOfPage;
        DateOfPress = SelectedBook.DateOfPress;
        Genres = SelectedBook.Ganres;
        CostPrice = SelectedBook.CostPrice;
        SellingPrice = SelectedBook.SellingPrice;
        Discount = SelectedBook.Discount;
        PublishingHouse = PublishingHouses.FirstOrDefault(x => x.Id == SelectedBook.PublishingHouseId)!;
        Author = Authors.FirstOrDefault(x => x.Id == SelectedBook.AuthorId)!;
        OnInitialized();
    }

    [RelayCommand]
    public async Task onOkey()
    {
        if (string.IsNullOrWhiteSpace(Title) ||
        string.IsNullOrWhiteSpace(Image) ||
        string.IsNullOrWhiteSpace(Descrtiption) ||
        NumberOfPage <= 0 ||
        DateOfPress == null ||
        PublishingHouse == null ||
        Author == null ||
        Genres == null ||
        Genres.Count == 0 ||
        CostPrice <= 0 ||
        SellingPrice <= 0 ||
        Discount < 0)
        {
            return;
        }

        var obj = new VM_BookForSale(new M_BookForSale()
        {
            Title = Title,
            Image = Image,
            Description = Descrtiption,
            NumberOfPage = NumberOfPage,
            DateOfPress = DateOfPress,

            CostPrice = CostPrice,
            SellingPrice = SellingPrice,
            Discount = Discount,
            Ganres = Genres.Select(x => x.Model).ToList(),
        })
        {
            PublishingHouse = PublishingHouse,
            Author = Author,
        };

        if (isUpdate)
        {
            await API.Update(SelectedBook.Id, obj.Model);
        }
        else
        {
            await API.Add(obj.Model);
        }
        API.Context.SaveChanges();
        NavigationPageService.Instance.NavigateTo<VM_BookPage>();
    }
    [RelayCommand]
    public async Task onCancle()
    {
        try
        {
            NavigationPageService.Instance.NavigateTo<VM_BookPage>();
        }
        catch
        {

        }
    }

    [RelayCommand]
    public async Task AddGenre()
    {

    }

    [RelayCommand]
    public async Task RemoveGenre()
    {

    }
}
