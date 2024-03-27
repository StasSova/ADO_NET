using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
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

namespace _04_StationeryCompany.Components
{
    /// <summary>
    /// Interaction logic for GridHeaderButton.xaml
    /// </summary>
    public partial class GridHeaderButton : Component
    {
        public static readonly DependencyProperty CommandTestProperty = DependencyProperty.Register("CommandTest", typeof(ICommand), typeof(GridHeaderButton));
        public ICommand CommandTest
        {
            get => (ICommand)GetValue(CommandTestProperty);
            set => SetValue(CommandTestProperty, value);
        }

        private string _columnName;
        public string ColumnName
        {
            get { return this._columnName; }
            set
            {
                _columnName = value;
                OnPropertyChanged(nameof(ColumnName));
            }
        }
        public GridHeaderButton()
        {
            InitializeComponent();
        }
    }
}
