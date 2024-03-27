using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace _04_StationeryCompany.Components
{
    public class GridHeaderButton2:Button
    {
        public static readonly DependencyProperty SortedByProperty = DependencyProperty.Register("SortedBy", typeof(string), typeof(GridHeaderButton2));
        public string SortedBy
        {
            get => (string)GetValue(SortedByProperty);
            set => SetValue(SortedByProperty, value);
        }

        public static readonly DependencyProperty SortDirectionProperty = DependencyProperty.Register("SortDirection", typeof(ListSortDirection), typeof(GridHeaderButton2));
        public ListSortDirection SortDirection
        {
            get => (ListSortDirection)GetValue(SortDirectionProperty);
            set => SetValue(SortDirectionProperty, value);
        }

        public static readonly DependencyProperty ColumnNameProperty = DependencyProperty.Register("ColumnName", typeof(string), typeof(GridHeaderButton2));
        public string ColumnName
        {
            get => (string)GetValue(ColumnNameProperty);
            set => SetValue(ColumnNameProperty, value);
        }
    }
}
