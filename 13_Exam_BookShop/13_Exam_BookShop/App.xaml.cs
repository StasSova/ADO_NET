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

            BookStore_DbContext.BookStore_DbContext a = new BookStore_DbContext.BookStore_DbContext();
        }
    }

}
