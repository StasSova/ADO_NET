using _13_Exam_BookShop.Service;
using _13_Exam_BookShop.ViewModels.Pages.View;
using _13_Exam_BookShop.ViewModels.Pages.ViewModel;
using _13_Exam_BookShop.ViewModels.Registration.Authorization;
using BookStore_DbContext.Interface;
using BookStore_DbContext.Models;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using System.Data;
using System.IO;
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

            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            var config = builder.Build();
            string connectionString = config.GetConnectionString("DefaultConnection");

            IDataBaseAPI db = new DataBaseAPI_D();
            StorageCollection.API = db;
            NavigationPageService.Instance.NavigateTo<VM_Authorization>();

            window.DataContext = context;
            window.Show();
        
        }


    }

}
