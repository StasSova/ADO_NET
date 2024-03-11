using _11_EF_StorageProcedure_Stationary.View;
using _11_EF_StorageProcedure_Stationary.ViewModel;
using DbCntx;
using DbCommandAPI.Implementation;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Windows;

namespace _11_EF_StorageProcedure_Stationary
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    /// 
    /// Microsoft.EntityFrameworkCore.SqlServer
    /// Microsoft.EntityFrameworkCore.Tools
    /// Scaffold-DbContext "Server=localhost\MSSQLSERVER01;Database=AuthorAndBook;Integrated Security=SSPI;TrustServerCertificate=true" Microsoft.EntityFrameworkCore.SqlServer
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture = new CultureInfo("ua-UA");
            V_Main window = new V_Main();
            MainWindow = window;

            _04StationeryCompanyContext Context = new _04StationeryCompanyContext();
            DbApi dbApi = new DbApi(Context);
            VM_Main main = new VM_Main(dbApi);
            window.DataContext = main;

            window.Show();
            window.Show();
        }
    }

}
