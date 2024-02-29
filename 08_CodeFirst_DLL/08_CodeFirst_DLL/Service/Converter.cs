using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace _08_CodeFirst_DLL.Service
{
    public class PopulationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int population)
            {
                return FormatPopulation(population);
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private string FormatPopulation(int population)
        {
            double formattedPopulation;
            string suffix;

            switch (population)
            {
                case var _ when population < 1000:
                    formattedPopulation = population;
                    suffix = "";
                    break;
                case var _ when population < 1000000:
                    formattedPopulation = population / 1000.0;
                    suffix = "K";
                    break;
                case var _ when population < 1000000000:
                    formattedPopulation = population / 1000000.0;
                    suffix = "M";
                    break;
                default:
                    formattedPopulation = population / 1000000000.0;
                    suffix = "B";
                    break;
            }

            return $"{formattedPopulation:N1}{suffix}";
        }
    }

    public class AreaConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double area)
            {
                return FormatArea(area);
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private string FormatArea(double area)
        {
            if (area < 1000000)
            {
                return $"{area:N0} sq km";
            }
            else if (area < 1000000000)
            {
                return $"{area / 1000000:N1}M sq km";
            }
            else
            {
                return $"{area / 1000000000:N1}B sq km";
            }
        }
    }
}
