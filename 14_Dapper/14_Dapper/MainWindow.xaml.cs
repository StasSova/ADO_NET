﻿using _14_Dapper.DbEntity;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _14_Dapper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static string? connectionString;
        private List<City> cities;
        private List<Country> countries;
        private List<Buyer> buyers;
        private List<ProductSection> sections;
        public MainWindow()
        {
            InitializeComponent();

            var builder = new ConfigurationBuilder();
            string path = Directory.GetCurrentDirectory();
            builder.SetBasePath(path);
            builder.AddJsonFile("appsettings.json");
            var config = builder.Build();
            connectionString = config.GetConnectionString("DefaultConnection");
            LoadComboBoxData();
        }
        private void LoadComboBoxData()
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                // Загрузка списка городов
                cities = db.Query<City>("SELECT * FROM City").ToList();

                // Загрузка списка стран
                countries = db.Query<Country>("SELECT * FROM Countries").ToList();

                // Загрузка списка покупателей
                buyers = db.Query<Buyer>("SELECT * FROM Buyer").ToList();

                // Загрузка списка разделов
                sections = db.Query<ProductSection>("SELECT * FROM ProductSection").ToList();
            }

            // Заполнение ComboBox данными
            CityComboBox.ItemsSource = cities;
            CountryComboBox.ItemsSource = countries;
            BuyerComboBox.ItemsSource = buyers;
            SectionComboBox.ItemsSource = sections;
        }

        // Логика для отображения всех покупателей
        private void ShowAllCustomers_Click(object sender, RoutedEventArgs e)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var groups = db.Query<Buyer>("SELECT * FROM Buyer");
                this.InformationzBlock.ItemsSource = groups;
            }
        }

        // Логика для отображения email всех покупателей
        private void ShowAllCustomerEmails_Click(object sender, RoutedEventArgs e)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var emails = db.Query<string>("SELECT Email FROM Buyer").ToList();
                var dataTable = new DataTable();
                dataTable.Columns.Add("Email");

                foreach (var email in emails)
                {
                    dataTable.Rows.Add(email);
                }

                InformationzBlock.ItemsSource = dataTable.DefaultView;
            }

        }

        // Логика для отображения списка разделов
        private void ShowAllSections_Click(object sender, RoutedEventArgs e)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var ProductSection = db.Query<ProductSection>("SELECT * FROM ProductSection");
                this.InformationzBlock.ItemsSource = ProductSection;
            }
        }

        // Логика для отображения списка акционных товаров
        private void ShowAllPromotions_Click(object sender, RoutedEventArgs e)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var promotions = db.Query<ProductSection>(@"
                    SELECT ps.* 
                    FROM ProductSection ps
                    JOIN Promotion p ON p.ProductSectionId = ps.Id
                    WHERE p.Discount > 0 AND p.ExpirationDate > GETDATE()
                ");
                this.InformationzBlock.ItemsSource = promotions;
            }
        }

        // Логика для отображения всех городов
        private void ShowAllCities_Click(object sender, RoutedEventArgs e)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var Cities = db.Query<City>("SELECT * FROM City");
                this.InformationzBlock.ItemsSource = Cities;
            }
        }

        // Логика для отображения всех стран
        private void ShowAllCountries_Click(object sender, RoutedEventArgs e)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var Countries = db.Query<Country>("SELECT * FROM Countries");
                this.InformationzBlock.ItemsSource = Countries;
            }
        }

        // Логика для отображения всех покупателей из конкретного города
        private void ShowCustomersInCity_Click(object sender, RoutedEventArgs e)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var cityName = this.CityNameTextBox.Text;

                int cityId = db.QueryFirstOrDefault<int>(@"
                    SELECT Id FROM City WHERE Name = @cityName
                ", new { cityName });

                if (cityId != 0)
                {
                    var buyersInCity = db.Query<Buyer>(@"
                        SELECT * FROM Buyer WHERE CityId = @cityId
                    ", new { cityId });

                    this.InformationzBlock.ItemsSource = buyersInCity;
                }
                else
                {
                    MessageBox.Show($"Город '{cityName}' не найден.");
                }
            }
        }

        // Логика для отображения всех покупателей из конкретной страны
        private void ShowCustomersInCountry_Click(object sender, RoutedEventArgs e)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var countryName = this.CountryNameTextBox.Text;

                int countryId = db.QueryFirstOrDefault<int>(@"
                    SELECT Id FROM Countries WHERE Name = @countryName
                ", new { countryName });

                if (countryId != 0)
                {
                    var buyersInCountry = db.Query<Buyer>(@"
                        SELECT * FROM Buyer WHERE CityId IN (SELECT Id FROM City WHERE CountrieId = @countryId)
                    ", new { countryId });

                    this.InformationzBlock.ItemsSource = buyersInCountry;
                }
                else
                {
                    MessageBox.Show($"Страна '{countryName}' не найдена.");
                }
            }
        }

        // Логика для отображения всех акций для конкретной страны
        private void ShowPromotionsInCountry_Click(object sender, RoutedEventArgs e)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var countryName = this.PromotionsCountryTextBox.Text;

                int countryId = db.QueryFirstOrDefault<int>(@"
                    SELECT Id FROM Countries WHERE Name = @countryName
                ", new { countryName });

                if (countryId != 0)
                {
                    var promotions = db.Query<ProductSection>(@"
                        SELECT ps.* 
                        FROM ProductSection ps
                        JOIN Promotion p ON p.ProductSectionId = ps.Id
                        WHERE p.Discount > 0 AND p.ExpirationDate > GETDATE() AND p.CountrieId = @countryId
                    ", new { countryId });
                    this.InformationzBlock.ItemsSource = promotions;
                }
                else
                {
                    MessageBox.Show($"Страна '{countryName}' не найдена.");
                }
            }
        }
        // Отображение списка городов конкретной страны
        private void ShowCitiesInCountry_Click(object sender, RoutedEventArgs e)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var countryName = this.CitiesCountryTextBox.Text;

                int countryId = db.QueryFirstOrDefault<int>(@"
                    SELECT Id FROM Countries WHERE Name = @countryName
                ", new { countryName });

                if (countryId != 0)
                {
                    var cities = db.Query<City>(@"
                        SELECT * FROM City WHERE CountrieId = @countryId
                    ", new { countryId });
                    this.InformationzBlock.ItemsSource = cities;
                }
                else
                {
                    MessageBox.Show($"Страна '{countryName}' не найдена.");
                }
            }
        }
        // Отображение списка разделов конкретного покупателя
        private void ShowSectionsForBuyer_Click(object sender, RoutedEventArgs e)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var buyerName = BuyerIdTextBox.Text; 

                int buyerId = db.QueryFirstOrDefault<int>(@"
                    SELECT Id FROM Buyer WHERE FullName = @buyerName
                ", new { buyerName });

                if (buyerId != 0)
                {
                    var sections = db.Query<ProductSection>(@"
                        SELECT ps.* FROM ProductSection ps
                        JOIN ShoppingCart sc ON ps.Id = sc.ProductSectionId
                        WHERE sc.BuyerId = @buyerId
                    ", new { buyerId });
                    this.InformationzBlock.ItemsSource = sections;
                }
                else
                {
                    MessageBox.Show($"Покупатель '{buyerName}' не найден.");
                }
            }
        }

        //Отображение списка акционных товаров конкретного раздела:
        private void ShowPromotionsForSection_Click(object sender, RoutedEventArgs e)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sectionName = SectionIdTextBox.Text;

                var sectionId = db.QueryFirstOrDefault<int>(@"
                    SELECT Id FROM ProductSection WHERE Name = @sectionName
                ", new { sectionName });

                if (sectionId != 0)
                {
                    var promotions = db.Query<Product>(@"
                        SELECT p.* 
                        FROM Product p
                        JOIN Promotion pr ON pr.ProductSectionId = p.ProductSectionId
                        WHERE pr.Discount > 0 AND pr.ExpirationDate > GETDATE() AND p.ProductSectionId = @sectionId
                    ", new { sectionId });

                    if (promotions.Any())
                    {
                        this.InformationzBlock.ItemsSource = promotions;
                    }
                    else
                    {
                        MessageBox.Show($"Для секции товаров '{sectionName}' нет акционных предложений.");
                    }
                }
                else
                {
                    MessageBox.Show($"Секция товаров '{sectionName}' не найдена.");
                }
            }
        }


    }
}