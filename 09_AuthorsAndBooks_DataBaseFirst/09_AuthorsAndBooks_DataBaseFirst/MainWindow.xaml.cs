using _09_AuthorsAndBooks_DataBaseFirst.ViewModels;
using DB_Context;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _09_AuthorsAndBooks_DataBaseFirst
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        VM_Main vm;
        public MainWindow()
        {
            InitializeComponent();
            this.vm = (VM_Main)DataContext;
        }

        private void MenuItem_Remove_Click(object sender, RoutedEventArgs e)
        {
            if (sender is MenuItem menuItem && menuItem.DataContext is VM_Book book)
            {
                vm.RemoveBook(book);
            }
        }

        private void MenuItem_Edit_Click(object sender, RoutedEventArgs e)
        {
            if (sender is MenuItem menuItem && menuItem.DataContext is VM_Book book)
            {
                vm.EditBook(book);
            }
        }
    }
}