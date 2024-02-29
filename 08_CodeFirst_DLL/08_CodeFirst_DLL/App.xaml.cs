using _08_CodeFirst_DLL.Service;
using _08_CodeFirst_DLL.ViewModel;
using _08_CodeFirst_DLL_Context;
using Newtonsoft.Json.Linq;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Windows;

namespace _08_CodeFirst_DLL
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture = new CultureInfo("ua-UA");
            MainWindow window = new MainWindow();
            MainWindow = window;

            // Start page
            DataBaseCrud dataBaseCrud = new(new CountryDbContext());
            NavigationPageService.Instance.NavigateTo<VM_Main>(dataBaseCrud);
            MainWindow.Show();
        }
        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
        }
    }

}
