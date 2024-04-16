using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Exam3_0.View.Main
{
    /// <summary>
    /// Interaction logic for V_Main.xaml
    /// </summary>
    public partial class V_Main : Window
    {
        public V_Main()
        {
            InitializeComponent();

            // Подписываемся на событие Navigated
            MainFrame.Navigated += MainFrame_Navigated;
        }
        private void MainFrame_Navigated(object sender, NavigationEventArgs e)
        {
            // Получаем текущую страницу
            var currentPage = e.Content as Page;

            if (currentPage != null)
            {
                // Обновляем размеры окна
                this.MinWidth = currentPage.MinWidth;
                this.Width = currentPage.Width;
                this.MaxWidth = currentPage.MaxWidth;

                this.MinHeight = currentPage.MinHeight;
                this.Height = currentPage.Height;
                this.MaxHeight = currentPage.MaxHeight;


                // Обновляем заголовок окна
                this.Title = currentPage.Title;
            }
        }
    }
}
