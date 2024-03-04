using _09_AuthorsAndBooks_DataBaseFirst.View;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DB_Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _09_AuthorsAndBooks_DataBaseFirst.ViewModels
{
    public partial class VM_Main : ObservableObject
    {
        [ObservableProperty] ObservableCollection<VM_Book>? books;
        [ObservableProperty] ObservableCollection<VM_Author>? authors;
        AuthorAndBookContext _dbContext;


        int selAuthorIndex = -1;
        public int SelAuthorIndex
        {
            get => selAuthorIndex;
            set
            {
                if (!global::System.Collections.Generic.EqualityComparer<int>.Default.Equals(selAuthorIndex, value))
                {
                    SetProperty(ref selAuthorIndex, value);
                    if (Filter) changeVisibility();
                    RemoveAuthorMenuCommand.NotifyCanExecuteChanged();
                    EditAuthorCommand.NotifyCanExecuteChanged();
                }
            }
        }

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(RemoveBookMenuCommand))]
        [NotifyCanExecuteChangedFor(nameof(EditBookMenuCommand))]
        int selBookIndex = -1;


        bool filter = false;
        public bool Filter
        {
            get { return filter; }
            set
            {
                filter = value;
                changeVisibility();
                OnPropertyChanged(nameof(Filter));
            }
        }
        public VM_Main()
        {
            _dbContext = new AuthorAndBookContext();
            Refresh();
        }
        public VM_Main(AuthorAndBookContext context)
        {
            this._dbContext = context;
            Refresh();
        }
        void Refresh()
        {
            var book = from g in _dbContext.Books select g;
            Books = new ObservableCollection<VM_Book>(book.Select(b => new VM_Book(b)));

            var aut = from a in _dbContext.Authors select a;
            Authors = new(aut.Select(x => new VM_Author(x)));
        }
        void changeVisibility()
        {
            if (SelAuthorIndex == null || SelAuthorIndex < 0) return;

            var book = Filter == true
                ? from g in _dbContext.Books where g.AuthorId == Authors[SelAuthorIndex].Id select g
                : from g in _dbContext.Books select g;
            Books = new ObservableCollection<VM_Book>(book.Select(b => new VM_Book(b)));
        }


        // УДАЛЕНИЕ КНИГИ
        [RelayCommand(CanExecute = nameof(canRemoveBookMenu))]
        void RemoveBookMenu()
        {
            RemoveBook(Books[SelBookIndex]);
        }
        bool canRemoveBookMenu() { return SelBookIndex >= 0; }
        [RelayCommand] public void RemoveBook(VM_Book book)
        {
            if (book == null) return;
            try
            {
                DialogResult result = MessageBox.Show("Вы действительно желаете удалить книгу " + book.Name +
                    " ?", "Удаление книги", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (result == DialogResult.Cancel) return;

                var s = (from b in _dbContext.Books where b.Id == book.Id select b).Single();
                if (s == null) return;
                _dbContext.Books.Remove(s);
                _dbContext.SaveChanges();

                Refresh();
                MessageBox.Show("Книга удалена");
            }
            catch (Exception ex) { }
        }


        // УДАЛЕНИЕ АВТОРА
        [RelayCommand(CanExecute = nameof(canRemoveAuthorMenu))]
        void RemoveAuthorMenu()
        {
            RemoveAuthor(Authors[SelAuthorIndex]);
        }
        bool canRemoveAuthorMenu() { return SelAuthorIndex >= 0; }
        [RelayCommand] public void RemoveAuthor(VM_Author author)
        {
            if (author == null) return;
            try
            {
                DialogResult result = MessageBox.Show("Вы действительно желаете удалить автора " + author.FullName +
                    $" и все его  {(from b in _dbContext.Books where b.AuthorId == author.Id select b).Count()} книг(и) ?", "Удаление книги", MessageBoxButtons.OKCancel, MessageBoxIcon.Question); ;
                if (result == DialogResult.Cancel) return;

                var s = (from b in _dbContext.Authors where b.Id == author.Id select b).Single();
                if (s == null) return;
                _dbContext.Authors.Remove(s);
                _dbContext.SaveChanges();

                Refresh();
                MessageBox.Show("Автор и его книги удалены");
            }
            catch (Exception ex) { }
        }


        // ДОБАВЛЕНИЯ КНИГИ
        [RelayCommand] void AddBook()
        {
            VM_Book res = ShowWindow("Add new Book");
            if (res == null) return;

            _dbContext.Books.Add(res.Book);
            _dbContext.SaveChanges();

            Books.Insert(0, res);
        }
        // РЕДАКТИРОВАНИЕ КНИГИ
        [RelayCommand(CanExecute = nameof(canEditBookmenu))]
        void EditBookMenu()
        {
            EditBook(Books[SelBookIndex]);
        }
        bool canEditBookmenu() { return SelBookIndex >= 0; }
        [RelayCommand] public void EditBook(VM_Book Book)
        {
            VM_Book res = ShowWindow("Edit Book", Book);
            if (res == null) return;

            Book old = _dbContext.Books.FirstOrDefault(x => x.Id == res.Id);
            old.Author = res.Author.Author;
            old.AuthorId = res.Author.Author.Id;
            old.Name = res.Name;
            _dbContext.SaveChanges();

            Book.Author = res.Author ?? Book.Author;
            Book.Name = res.Name ?? Book.Name;
        }
        private VM_Book ShowWindow(string title, VM_Book book = null)
        {
            VM_Book res = null;
            if (book != null)
            {
                res = new VM_Book(new Book()
                {
                    Author = book.Author.Author,
                    Name = book.Name,
                    Id = book.Id,
                });
            }
            VM_Book_Detail m = new VM_Book_Detail(title, Authors, res);
            BookDetail v = new BookDetail();
            v.DataContext = m;
            if (true == v.ShowDialog())
            {
                return m.Book;
            }
            return null;
        }

        // ДОБАВЛЕНИЯ АВТОРА
        [RelayCommand] void AddAuthor()
        {
            VM_Author res = ShowAuthorWindow("Add new Author");
            if (res == null) return;
            _dbContext.Authors.Add(res.Author);
            _dbContext.SaveChanges();

            Authors.Add(res);
        }
        // РЕДАКТИРОВАНИЯ АВТОРА
        [RelayCommand(CanExecute = nameof(canEditAuthor))]
        void EditAuthor()
        {
            VM_Author toEdit = Authors[SelAuthorIndex];
            VM_Author res = ShowAuthorWindow("Edit Author", toEdit);
            if (res == null) return;

            Author old = _dbContext.Authors.FirstOrDefault(x => x.Id == res.Id);
            old.FullName = res.FullName;
            _dbContext.SaveChanges();

            Refresh();
        }
        bool canEditAuthor() { return SelAuthorIndex >= 0; }
        private VM_Author ShowAuthorWindow(string title, VM_Author author = null)
        {
            VM_Author res = null;
            if (author != null)
            {
                res = new VM_Author(new Author()
                {
                    FullName = author.FullName,
                    Id = author.Id,
                });
            }
            VM_Author_Detail m = new(title, res);
            AuthorDetail v = new();
            v.DataContext = m;
            if (true == v.ShowDialog())
            {
                return m.Author;
            }
            return null;
        }

    }
}
