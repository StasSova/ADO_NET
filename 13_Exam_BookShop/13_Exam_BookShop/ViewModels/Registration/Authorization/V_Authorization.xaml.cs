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

namespace _13_Exam_BookShop.ViewModels.Registration.Authorization
{
    /// <summary>
    /// Interaction logic for V_Authorization.xaml
    /// </summary>
    public partial class V_Authorization : Page
    {
        public V_Authorization()
        {
            InitializeComponent();
            this.container.DataContext = this.DataContext;
        }
    }
}
