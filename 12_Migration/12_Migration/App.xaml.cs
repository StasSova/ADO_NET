using DLL_DbContext;
using System.Configuration;
using System.Data;
using System.Windows;

namespace _12_Migration
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            MainWindow main = new MainWindow();
            main.Show();
        }
    }

}
