using _11_EF_StorageProcedure_Stationary.ViewModel;
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

namespace _11_EF_StorageProcedure_Stationary.View
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

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            m ??= (VM_Main)this.DataContext;
            m.SelectedItemPropertyChanged();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Types_TextChanged(object sender, TextChangedEventArgs e)
        {
            m ??= (VM_Main)this.DataContext;
            m.SelectedTypePropertyChanged();
        }

        private void Managers_TextChanged(object sender, TextChangedEventArgs e)
        {
            m ??= (VM_Main)this.DataContext;
            m.SelectedmanagerPropertyChanged();
        }

        private void Companies_TextChanged(object sender, TextChangedEventArgs e)
        {
            m ??= (VM_Main)this.DataContext;
            m.SelectedCompaniesPropertyChanged();
        }
    }
}
