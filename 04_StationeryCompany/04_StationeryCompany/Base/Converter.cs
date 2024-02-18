using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;
using _04_StationeryCompany.MVVM;
using _04_StationeryCompany.MVVM.ViewModelResponseDB;

namespace _04_StationeryCompany.Base
{
    public class SortToText : IMultiValueConverter
    {
        private static string Prefix(ListSortDirection direction) => direction == ListSortDirection.Ascending ? "⏶" : "⏷";
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var column = values[0] as string;
            var sortedBy = values[1] as string;
            var sortDirection = (ListSortDirection)values[2];
            var alignment = HorizontalAlignment.Right;
            if (values[3] is HorizontalAlignment a) alignment = a;

            string format = alignment == HorizontalAlignment.Right ? $"{Prefix(sortDirection)}{column}" : $"{column}{Prefix(sortDirection)}";
            return (string)(sortedBy == column ? format : column);
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class TypeIdToTypeName : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return "NULL";
            int id;
            bool res = int.TryParse(value.ToString(), out id);
            return res ? StorageCollection.Instance.TypeOfItems.First(x => x.Id == id).Type : "NULL";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class ManagerIdToManagerFullName : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return "NULL";
            int id;
            bool res = int.TryParse(value.ToString(), out id);
            VM_Manager man = StorageCollection.Instance.Managers.First(x => x.Id == id);
            return res ? $"{man.LName[0]}. {man.FName}" : "NULL";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class ItemIdToItemName : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return "NULL";
            int id;
            bool res = int.TryParse(value.ToString(), out id);
            VM_Item el = StorageCollection.Instance.Items.First(x => x.Id == id);
            return res ? el.Name : "NULL";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class DateToShort : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                return ((DateTime)value).ToShortDateString();
            }
            catch { return "NULL"; }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class BuyersCompanyToBuyersCompanyName : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return "NULL";
            int id;
            bool res = int.TryParse(value.ToString(), out id);
            VM_Company el = StorageCollection.Instance.Company.First(x => x.Id == id);
            return res ? el.Name : "NULL";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class DecimalToVariableCurrency : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null) return string.Empty;

            var price = (decimal)value;

            int precision;
            if (price >= 1 || price < 0) precision = 2;
            else if (price > 0.099M) precision = 4;
            else precision = (int)Math.Log10((double)(1M / price)) * 2;
            return string.Format(CultureInfo.CurrentCulture, "{0:C" + precision + "}", price);
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
