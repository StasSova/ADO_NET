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
    /// Interaction logic for V_Add_Edit_Book.xaml
    /// </summary>
    public partial class V_Add_Edit_Book : Page
    {

        VM_Add_Edit_Book vm;
        public V_Add_Edit_Book()
        {
            InitializeComponent();

        }

        public override void BeginInit()
        {
            base.BeginInit();
            if (this.DataContext != null)
            {
                this.vm = (VM_Add_Edit_Book)this.DataContext;
                vm.Initialized += OnViewModelInit;
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox checkBox && checkBox.DataContext is VM_Genre genre)
            {
                if (vm == null)
                    vm = (VM_Add_Edit_Book)this.DataContext;
                vm.Genres.Add(genre);
            }
        }
        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox checkBox && checkBox.DataContext is VM_Genre genre)
            {
                if (vm == null)
                    vm = (VM_Add_Edit_Book)this.DataContext;
                vm.Genres.Remove(genre);
            }
        }

        private void OnViewModelInit()
        {
            var selectedGenres = vm.Genres;
            var allGenres = vm.AllGenres;

            List<string> sel = selectedGenres.Select(x => x.Genre).ToList();

            // Создаем коллекцию CheckBox, которые будем добавлять
            var checkBoxes = new List<CheckBox>();
            genresCollection.Items.Clear();

            // Добавляем существующие жанры
            foreach (var genre in allGenres)
            {
                var checkBox = new CheckBox();
                checkBox.Content = genre.Genre;
                checkBox.Checked += CheckBox_Checked;
                checkBox.Unchecked += CheckBox_Unchecked;
                checkBox.DataContext = genre;
                // Устанавливаем свойство IsChecked
                checkBox.IsChecked = sel.Contains(genre.Genre);

                checkBoxes.Add(checkBox);
            }

            // Устанавливаем коллекцию CheckBox в ItemsControl
            genresCollection.ItemsSource = checkBoxes;
        }
    }
}
