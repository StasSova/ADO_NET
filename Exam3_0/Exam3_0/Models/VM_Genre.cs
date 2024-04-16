using BookContext.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam3_0.Models;

public partial class VM_Genre : VM_Entity
{
    [ObservableProperty] bool isSelected;
    [ObservableProperty] M_Genre model;
    private ICollection<VM_Book>? _books;
    public VM_Genre(M_Genre ganre)
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
    public ICollection<VM_Book> Books
    {
        get
        {
            return _books ??= Model.Books.Select(book => new VM_Book(book)).ToList();
        }
        set
        {
            if (_books != value)
            {
                _books = value;
                OnPropertyChanged(nameof(Books));
                Model.Books = value.Select(vm_book => vm_book.Model).ToList();
            }
        }
    }

    public void AddBook(VM_Book book)
    {
        Model.Books.Add(book.Model);
        _books?.Append(book);
    }
    public void RemoveBook(VM_Book book)
    {
        Model.Books.Remove(book.Model);
        if (_books != null)
        {
            _books = _books.Where(b => b.Id != book.Id).ToList();
        }
    }
    public void UpdateBooks(IEnumerable<VM_Book> books)
    {
        Model.Books.Clear();
        foreach (var book in books)
        {
            Model.Books.Add(book.Model);
        }
        _books = null; // Сбросить кэш, чтобы он обновился при следующем обращении к Books
    }
}
