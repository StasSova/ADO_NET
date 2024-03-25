using _13_Exam_BookShop.ViewModels.Pages.View;
using _13_Exam_BookShop.ViewModels.Pages.ViewModel;
using BookStore_DbContext.Models;
using System.Configuration;
using System.Data;
using System.Windows;

namespace _13_Exam_BookShop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var context = new VM_Main();
            var window = new V_Main(context);
            window.DataContext = context;
            window.Show();
        
        }
    }

}
