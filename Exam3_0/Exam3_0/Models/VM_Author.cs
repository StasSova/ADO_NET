using BookContext.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam3_0.Models;

public partial class VM_Author : VM_Entity
{
    [ObservableProperty] M_Author model;
    private IEnumerable<VM_Book>? _books;
    public VM_Author(M_Author author)
    {
        Model = author;
    }
    public int Id
    {
        get => Model.Id;
    }

    public string FullName
    {
        get => Model.FullName;
        set
        {
            if (Model.FullName != value)
            {
                Model.FullName = value;
                OnPropertyChanged(nameof(FullName));
            }
        }
    }

    public DateTime DateOfBirth
    {
        get => Model.DateOfBirth;
        set
        {
            if (Model.DateOfBirth != value)
            {
                Model.DateOfBirth = value;
                OnPropertyChanged(nameof(DateOfBirth));
            }
        }
    }

    public IEnumerable<VM_Book> Books
    {
        get
        {
            if (_books == null)
            {
                _books = Model.Books.Select(book => new VM_Book(book)).ToList();
            }
            return _books;
        }
    }
    public override string ToString() => FullName;


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
            _books = _books.Where(b => b.Id != book.Id);
        }
    }
    public void UpdateBooks(IEnumerable<VM_Book> books)
    {
        Model.Books.Clear();
        foreach (var book in books)
        {
            Model.Books.Add(book.Model);
        }
        _books = null;
    }
}
