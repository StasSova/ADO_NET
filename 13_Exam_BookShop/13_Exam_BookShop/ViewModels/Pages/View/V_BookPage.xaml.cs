using _13_Exam_BookShop.Service;
using _13_Exam_BookShop.ViewModels.DbViewModels;
using _13_Exam_BookShop.ViewModels.Pages.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _13_Exam_BookShop.ViewModels.Pages.View
{
    /// <summary>
    /// Interaction logic for V_BookPage.xaml
    /// </summary>
    public partial class V_BookPage : Page
    {
        public VM_BookPage ViewModel;
        public V_BookPage()
        {
            ViewModel = new VM_BookPage();
            InitializeComponent();
            this.GridCollection.DataContext = ViewModel;
            AddGenresToMenu();
            AddAuothorsMenu();
        }

        private async void AddGenresToMenu()
        {
            // Получение списка жанров из ViewModel
            var genres = await StorageCollection.Instance.GetGenres();

            // Добавление MenuItem для каждого жанра
            foreach (var genre in genres)
            {
                var menuItem = new MenuItem()
                {
                    Header = genre.Genre
                };
                menuItem.Click += (sender, e) => GetBooksByGenre(genre.Id);
                MenuGanre.Items.Add(menuItem);
            }
        }
        private async void AddAuothorsMenu()
        {
            // Получение списка жанров из ViewModel
            var authors = await StorageCollection.Instance.GetAuthors();

            // Добавление MenuItem для каждого жанра
            foreach (var author in authors)
            {
                var menuItem = new MenuItem()
                {
                    Header = author.FirstName + " " + author.LastName
                };
                menuItem.Click += (sender, e) => GetBooksByAuthor(author.Id);
                MenuAuthor.Items.Add(menuItem);
            }
        }


        private void GetBooksByGenre(int genreId)
        {
            _ = ViewModel.GetBooksByGenre(genreId);
        }
        private void GetBooksByAuthor(int auhorId)
        {
            _ = ViewModel.GetBooksByAuthor(auhorId);
        }
        private void GetPromotionBooks(object sender, RoutedEventArgs e)
        {
            _ = ViewModel.GetPromotionBooks();
        }



        private void AddToCart_Click(object sender, RoutedEventArgs e)
        {
            // Получаем Id из DataContext кнопки
            if (sender is Button button && button.DataContext is VM_BookForSale item)
            {
                int id = item.Id;
                ViewModel.AddToCartCommand.Execute(id);
            }
        }
        private void EditBook_Click(object sender, RoutedEventArgs e)
        {
            // Получаем Id из DataContext кнопки
            if (sender is Button button && button.DataContext is VM_BookForSale item)
            {
                int id = item.Id;
                ViewModel.EditBookCommand.Execute(id);
            }
        }

        private void RemoveBook_Click(object sender, RoutedEventArgs e)
        {
            // Получаем Id из DataContext кнопки
            if (sender is Button button && button.DataContext is VM_BookForSale item)
            {
                int id = item.Id;
                ViewModel.RemoveBookCommand.Execute(id);
            }
        }





        private void GetAllBooks(object sender, RoutedEventArgs e)
        {
            try
            {
                _ = ViewModel.GetBook();
            }
            catch (Exception ex)
            {

            }
        }

        private void SearchBook(object sender, RoutedEventArgs e)
        {
            try
            {
                _ = ViewModel.Seacrh(this.searchTextBox.Text);
            }
            catch
            {

            }
        }
    }
}
