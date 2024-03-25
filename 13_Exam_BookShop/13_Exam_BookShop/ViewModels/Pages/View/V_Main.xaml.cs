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
using System.Windows.Shapes;

namespace _13_Exam_BookShop.ViewModels.Pages.View;

/// <summary>
/// Interaction logic for V_Main.xaml
/// </summary>
public partial class V_Main : Window
{
    private VM_Main ViewModel;
    public V_Main(VM_Main vM_Main)
    {
        InitializeComponent();
        this.ViewModel = vM_Main;
        AddGenresToMenu();
    }


    private async void AddGenresToMenu()
    {
        // Получение списка жанров из ViewModel
        var genres = await GetGenresFromViewModel();

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

    private async Task<List<VM_Genre>> GetGenresFromViewModel()
    {
        return await ViewModel.GetGenres();
    }

    private async void GetBooksByGenre(int genreId)
    {
        await ViewModel.GetBooksByGenre(genreId);
    }
}
