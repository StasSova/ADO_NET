using BookContext.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Exam3_0.Models;
using Exam3_0.Service;
using Exam3_0.View.Book;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Exam3_0.View.Books;
public partial class VM_Books_Page : VM_Page
{
    [ObservableProperty] ObservableCollection<VM_Book> books;
    private IDataBaseAPI API { get; set; }
    public VM_Books_Page()
    {
        this.API = APIProvider.API;
    }

    public override async void Initialize(object parameter)
    {
        base.Initialize(parameter);

        var books = await API.Context.Books.Select(x => new VM_Book(x)).ToListAsync();
        Books = new ObservableCollection<VM_Book>(books);

        foreach (var book in books)
        {
            book.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(VM_Book.IsSelected))
                    OnPropertyChanged(nameof(IsAllItemsSelected));
            };
            BooksName.Add(book.Title);
            _original_booksName.Add(book.Title);
        }
    }
    public bool? IsAllItemsSelected
    {
        get
        {
            var selected = Books?.Select(x => x.IsSelected).Distinct().ToList();
            return selected?.Count == 1 ? selected.Single() : (bool?)null;
        }
        set
        {
            if (value.HasValue)
            {
                SelectAll(value.Value, Books);
                OnPropertyChanged();
            }
        }
    }
    private static void SelectAll(bool select, IEnumerable<VM_Book> models)
    {
        foreach (var model in models)
        {
            model.IsSelected = select;
        }
    }

    [RelayCommand]
    async Task AddBook()
    {
        try
        {
            NavigationPageService.Instance.NavigateTo<VM_Book_Page>("Create");
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }
    [RelayCommand]
    async Task EditBook(VM_Book book)
    {
        try
        {
            NavigationPageService.Instance.NavigateTo<VM_Book_Page>("Edit", book.Id);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }
    [RelayCommand]
    async Task DeleteBook(VM_Book book)
    {
        try
        {
            var index = Books.IndexOf(book);
            Books.RemoveAt(index + 1);

            API.Context.Remove(book.Model);
            await API.Context.SaveChangesAsync();

        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }
    [RelayCommand]
    async Task DetailBook(VM_Book book)
    {
        try
        {
            NavigationPageService.Instance.NavigateTo<VM_Book_Page>("Details", book.Id);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private string searchString;
    public string SearchString
    {
        get => searchString;
        set
        {
            if (SetProperty(ref searchString, value) &&
                _original_booksName != null && value != null)
            {
                var searchResult = _original_booksName.Where(x => x.Contains(value)).ToList();
                BooksName = new ObservableCollection<string>(searchResult);
            }
        }
    }
    [ObservableProperty] ObservableCollection<string> booksName = new ObservableCollection<string>();
    private List<string> _original_booksName = new List<string>();

    [RelayCommand]
    async Task Search()
    {
        try
        {
            List<VM_Book> books = new List<VM_Book>();
            if (SearchString.IsNullOrEmpty())
            {
                books = await API.Context.Books.Select(x => new VM_Book(x)).ToListAsync();
            }
            else
            {
                books = await API.Context.Books.Where(x => x.Title.Contains(SearchString)).Select(x => new VM_Book(x)).ToListAsync();
            }
            Books = new ObservableCollection<VM_Book>(books);
            foreach (var book in books)
            {
                book.PropertyChanged += (sender, args) =>
                {
                    if (args.PropertyName == nameof(VM_Book.IsSelected))
                        OnPropertyChanged(nameof(IsAllItemsSelected));
                };
                BooksName.Add(book.Title);
            }
        }
        catch
        {

        }
    }
    public IEnumerable<DataGridSelectionUnit> SelectionUnits => new[] { DataGridSelectionUnit.FullRow, DataGridSelectionUnit.Cell, DataGridSelectionUnit.CellOrRowHeader };
}
