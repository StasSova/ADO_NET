using _02_ConnectionToDB_RequestToDB.MVVM;
using System.Configuration;
using System.Data;
using System.Windows;

namespace _02_ConnectionToDB_RequestToDB
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Создаем экземпляр главного окна
            V_Main mainWindow = new V_Main();

            // Устанавливаем главное окно приложения
            this.MainWindow = mainWindow;

            // Показываем главное окно
            mainWindow.Show();
        }
    }

}
