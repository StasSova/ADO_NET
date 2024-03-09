using _10_FluentAPI.ViewModels;
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
using System.Windows.Shapes;

namespace _10_FluentAPI.Views
{
    /// <summary>
    /// Interaction logic for V_Main.xaml
    /// </summary>
    public partial class V_Main : Window
    {
        VM_Main m;
        public V_Main()
        {
            InitializeComponent();
        }
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            m ??= (VM_Main)this.DataContext;
            m.DeleteEmployeeCommand.Execute(this.ListBoxColl.SelectedValue);
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            m ??= (VM_Main)this.DataContext;
            m.NotifySelectedEmployeePropertyChangedCommand.Execute(null);
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            m ??= (VM_Main)this.DataContext;
            m.ChangeStateEmployeeToEditingCommand.Execute(this.ListBoxColl.SelectedValue);
        }

        private void txtNewPosition_TextChanged(object sender, TextChangedEventArgs e)
        {
            m ??= (VM_Main)this.DataContext;
            m.NotifySelectedPositionPropertyChangedCommand.Execute(null);
        }
    }
}
