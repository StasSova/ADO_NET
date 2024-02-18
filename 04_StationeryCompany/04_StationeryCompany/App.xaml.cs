using _04_StationeryCompany.Base;
using _04_StationeryCompany.Interface;
using _04_StationeryCompany.InterfaceImp;
using _04_StationeryCompany.MVVM;
using _04_StationeryCompany.MVVM.ViewModel;
using Newtonsoft.Json.Linq;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Windows;

namespace _04_StationeryCompany
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
            string connection = LoadConnectionString();
            DataBaseCrudDefault dataBaseCrud = new(connection);
            NavigationPageService.Instance.NavigateTo<VM_Main>(dataBaseCrud);

            MainWindow.Show();
        }

        protected override void OnExit(ExitEventArgs e)
        {

            base.OnExit(e);
        }
        private static string LoadConnectionString()
        {
            try
            {
                string jsonFilePath = FindAppSettingsFilePath();

                if (jsonFilePath != null && File.Exists(jsonFilePath))
                {
                    string jsonContent = File.ReadAllText(jsonFilePath);
                    JObject jsonObject = JObject.Parse(jsonContent);
                    return jsonObject["ConnectionStrings"]["DefaultConnection"].ToString();
                }
                else
                {
                    throw new FileNotFoundException("JSON file not found or invalid.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading connection string: " + ex.Message);
                throw; // Rethrow the exception for further handling
            }
        }
        private static string FindAppSettingsFilePath()
        {
            return "appsettings.json";
        }

    }

}
