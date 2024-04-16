using BookContext.Interfaces;
using Exam3_0.Service;
using Exam3_0.View.Main;
using Exam3_0.View.Register;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using System.Data;
using System.DirectoryServices.ActiveDirectory;
using System.IO;
using System.Windows;

namespace Exam3_0
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            APIProvider.Initialize(new DataBaseAPI_D());

            var window = new V_Main();
            NavigationPageService.Instance.NavigateTo<VM_Registration>("Registration");

            window.Show();
        }
    }

}
