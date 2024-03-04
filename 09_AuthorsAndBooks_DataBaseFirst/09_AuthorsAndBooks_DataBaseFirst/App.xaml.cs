using _09_AuthorsAndBooks_DataBaseFirst.ViewModels;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Windows;

namespace _09_AuthorsAndBooks_DataBaseFirst
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    /// Microsoft.EntityFrameworkCore.SqlServer
    /// Microsoft.EntityFrameworkCore.Tools
    /// Scaffold-DbContext "Server=localhost\MSSQLSERVER01;Database=AuthorAndBook;Integrated Security=SSPI;TrustServerCertificate=true" Microsoft.EntityFrameworkCore.SqlServer
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture = new CultureInfo("ua-UA");
            MainWindow window = new MainWindow();
            MainWindow = window;

            VM_Main main = new VM_Main(new DB_Context.AuthorAndBookContext());
            window.DataContext = main;

            window.Show();
        }
    }

}
