using _10_FluentAPI.ViewModels;
using _10_FluentAPI.Views;
using DataBase;
using DataBaseAPI.Implementation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.IO;
using System.Windows;

namespace _10_FluentAPI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            try
            {
                base.OnStartup(e);


                V_Main window = new V_Main();

                // Создаем контекст базы данных
                var optionsBuilder = new DbContextOptionsBuilder<DbCntx>();

                string? connectionString = ReadConnection();
                if (connectionString == null) throw new Exception("Error reading application configuration file. The configuration file does not contain a connection string."); ;

                optionsBuilder
                    .UseSqlServer(connectionString)
                    .UseLazyLoadingProxies();

                DbCntx context = new DbCntx(optionsBuilder.Options);
                DataBaseAPI_D api = new DataBaseAPI_D(context);

                VM_Main model = new VM_Main(api);
                window.DataContext = model;
                MainWindow = window;
                MainWindow.Show();
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }

        }
        private string? ReadConnection()
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            var config = builder.Build();
            return config.GetConnectionString("DefaultConnection"); 
        }
    }

}
