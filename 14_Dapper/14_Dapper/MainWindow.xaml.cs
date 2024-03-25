using _14_Dapper.DbEntity;
using _14_Dapper.Pages;
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
            PromotionsCountryComboBox.ItemsSource = countries;
            CitiesCountryComboBox.ItemsSource = countries;
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

        private void ShowAllPromotions2_Click(object sender, RoutedEventArgs e)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var promotions = db.Query<Promotion>(@"SELECT * FROM Promotion");
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
                var selectedCity = CityComboBox.SelectedItem as City;

                if (selectedCity != null)
                {
                    var buyersInCity = db.Query<Buyer>(@"
                        SELECT * FROM Buyer WHERE CityId = @cityId
                    ", new { cityId = selectedCity.Id });

                    this.InformationzBlock.ItemsSource = buyersInCity;
                }
                else
                {
                    MessageBox.Show("Выберите город.");
                }
            }
        }

        // Логика для отображения всех покупателей из конкретной страны
        private void ShowCustomersInCountry_Click(object sender, RoutedEventArgs e)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var selectedCountry = CountryComboBox.SelectedItem as Country;

                if (selectedCountry != null)
                {
                    var buyersInCountry = db.Query<Buyer>(@"
                        SELECT * FROM Buyer WHERE CityId IN (SELECT Id FROM City WHERE CountrieId = @countryId)
                    ", new { countryId = selectedCountry.Id });

                    this.InformationzBlock.ItemsSource = buyersInCountry;
                }
                else
                {
                    MessageBox.Show("Выберите страну.");
                }
            }
        }

        // Логика для отображения всех акций для конкретной страны
        private void ShowPromotionsInCountry_Click(object sender, RoutedEventArgs e)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var selectedCountry = PromotionsCountryComboBox.SelectedItem as Country;

                if (selectedCountry != null)
                {
                    var promotions = db.Query<ProductSection>(@"
                        SELECT ps.* 
                        FROM ProductSection ps
                        JOIN Promotion p ON p.ProductSectionId = ps.Id
                        WHERE p.Discount > 0 AND p.ExpirationDate > GETDATE() AND p.CountrieId = @countryId
                    ", new { countryId = selectedCountry.Id });

                    this.InformationzBlock.ItemsSource = promotions;
                }
                else
                {
                    MessageBox.Show("Выберите страну.");
                }
            }
        }
        // Отображение списка городов конкретной страны
        private void ShowCitiesInCountry_Click(object sender, RoutedEventArgs e)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var selectedCountry = CitiesCountryComboBox.SelectedItem as Country;

                if (selectedCountry != null)
                {
                    var cities = db.Query<City>(@"
                        SELECT * FROM City WHERE CountrieId = @countryId
                    ", new { countryId = selectedCountry.Id });

                    this.InformationzBlock.ItemsSource = cities;
                }
                else
                {
                    MessageBox.Show("Выберите страну.");
                }
            }
        }
        // Отображение списка разделов конкретного покупателя
        private void ShowSectionsForBuyer_Click(object sender, RoutedEventArgs e)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var selectedBuyer = BuyerComboBox.SelectedItem as Buyer;

                if (selectedBuyer != null)
                {
                    var sections = db.Query<ProductSection>(@"
                        SELECT ps.* FROM ProductSection ps
                        JOIN ShoppingCart sc ON ps.Id = sc.ProductSectionId
                        WHERE sc.BuyerId = @buyerId
                    ", new { buyerId = selectedBuyer.Id });

                    this.InformationzBlock.ItemsSource = sections;
                }
                else
                {
                    MessageBox.Show("Выберите покупателя.");
                }
            }
        }

        //Отображение списка акционных товаров конкретного раздела:
        private void ShowPromotionsForSection_Click(object sender, RoutedEventArgs e)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var selectedSection = SectionComboBox.SelectedItem as ProductSection;

                if (selectedSection != null)
                {
                    var promotions = db.Query<Product>(@"
                        SELECT p.* 
                        FROM Product p
                        JOIN Promotion pr ON pr.ProductSectionId = p.ProductSectionId
                        WHERE pr.Discount > 0 AND pr.ExpirationDate > GETDATE() AND p.ProductSectionId = @sectionId
                    ", new { sectionId = selectedSection.Id });

                    if (promotions.Any())
                    {
                        this.InformationzBlock.ItemsSource = promotions;
                    }
                    else
                    {
                        MessageBox.Show($"Для секции товаров '{selectedSection.Name}' нет акционных предложений.");
                    }
                }
                else
                {
                    MessageBox.Show("Выберите секцию товаров.");
                }
            }
        }



        // Метод для добавления нового покупателя
        private void AddCustomer_Click(object sender, RoutedEventArgs e)
        {
            AddBuyer addCustomerWindow = new AddBuyer(connectionString);
            addCustomerWindow.ShowDialog();
        }

        // Метод для добавления новой страны
        private void AddCountry_Click(object sender, RoutedEventArgs e)
        {
            AddCountrie addCountryWindow = new AddCountrie(connectionString);
            addCountryWindow.ShowDialog();
            LoadComboBoxData();
        }

        // Метод для добавления нового города
        private void AddCity_Click(object sender, RoutedEventArgs e)
        {
            AddCity addCityWindow = new AddCity(connectionString);
            addCityWindow.ShowDialog();
            LoadComboBoxData();
        }

        // Метод для добавления нового раздела
        private void AddSection_Click(object sender, RoutedEventArgs e)
        {
            AddSection addSectionWindow = new AddSection(connectionString);
            addSectionWindow.ShowDialog();
            LoadComboBoxData();
        }

        // Метод для добавления новой акции
        private void AddPromotion_Click(object sender, RoutedEventArgs e)
        {
            AddPromotion addPromotionWindow = new AddPromotion(connectionString);
            addPromotionWindow.ShowDialog();
            LoadComboBoxData();
        }


        // Метод для обновления выбранного покупателя
        private void UpdateSelectedCustomer_Click(object sender, RoutedEventArgs e)
        {
            if (InformationzBlock.SelectedItem != null && InformationzBlock.SelectedItem is Buyer)
            {
                Buyer selectedBuyer = (Buyer)InformationzBlock.SelectedItem;
                AddBuyer updateCustomerWindow = new AddBuyer(connectionString, selectedBuyer);
                updateCustomerWindow.ShowDialog();
            }
        }

        // Метод для обновления выбранной страны
        private void UpdateSelectedCountry_Click(object sender, RoutedEventArgs e)
        {
            if (InformationzBlock.SelectedItem != null && InformationzBlock.SelectedItem is Country)
            {
                Country selectedCountry = (Country)InformationzBlock.SelectedItem;
                AddCountrie updateCountryWindow = new AddCountrie(connectionString, selectedCountry);
                updateCountryWindow.ShowDialog();
                LoadComboBoxData();
            }
        }

        // Метод для обновления выбранного города
        private void UpdateSelectedCity_Click(object sender, RoutedEventArgs e)
        {
            if (InformationzBlock.SelectedItem != null && InformationzBlock.SelectedItem is City)
            {
                City selectedCity = (City)InformationzBlock.SelectedItem;
                AddCity updateCityWindow = new AddCity(connectionString, selectedCity);
                updateCityWindow.ShowDialog();
                LoadComboBoxData();
            }
        }

        // Метод для обновления выбранного раздела
        private void UpdateSelectedSection_Click(object sender, RoutedEventArgs e)
        {
            if (InformationzBlock.SelectedItem != null && InformationzBlock.SelectedItem is ProductSection)
            {
                ProductSection selectedSection = (ProductSection)InformationzBlock.SelectedItem;
                AddSection updateSectionWindow = new AddSection(connectionString, selectedSection);
                updateSectionWindow.ShowDialog();
                LoadComboBoxData();
            }
        }

        // Метод для обновления выбранной акции
        private void UpdateSelectedPromotion_Click(object sender, RoutedEventArgs e)
        {
            if (InformationzBlock.SelectedItem != null && InformationzBlock.SelectedItem is Promotion)
            {
                Promotion selectedPromotion = (Promotion)InformationzBlock.SelectedItem;
                AddPromotion updatePromotionWindow = new AddPromotion(connectionString, selectedPromotion);
                updatePromotionWindow.ShowDialog();
                LoadComboBoxData();
            }
        }

        private void DeleteSelectedCustomer_Click(object sender, RoutedEventArgs e)
        {
            if (InformationzBlock.SelectedItem != null && InformationzBlock.SelectedItem is Buyer)
            {
                Buyer selectedBuyer = (Buyer)InformationzBlock.SelectedItem;
                DeleteCustomer(selectedBuyer.Id);
            }
        }

        private void DeleteSelectedCountry_Click(object sender, RoutedEventArgs e)
        {
            if (InformationzBlock.SelectedItem != null && InformationzBlock.SelectedItem is Country)
            {
                Country selectedCountry = (Country)InformationzBlock.SelectedItem;
                DeleteCountry(selectedCountry.Id);
            }
        }

        private void DeleteSelectedCity_Click(object sender, RoutedEventArgs e)
        {
            if (InformationzBlock.SelectedItem != null && InformationzBlock.SelectedItem is City)
            {
                City selectedCity = (City)InformationzBlock.SelectedItem;
                DeleteCity(selectedCity.Id);
            }
        }

        private void DeleteSelectedSection_Click(object sender, RoutedEventArgs e)
        {
            if (InformationzBlock.SelectedItem != null && InformationzBlock.SelectedItem is ProductSection)
            {
                ProductSection selectedSection = (ProductSection)InformationzBlock.SelectedItem;
                DeleteSection(selectedSection.Id);
            }
        }

        private void DeleteSelectedPromotion_Click(object sender, RoutedEventArgs e)
        {
            if (InformationzBlock.SelectedItem != null && InformationzBlock.SelectedItem is Promotion)
            {
                Promotion selectedPromotion = (Promotion)InformationzBlock.SelectedItem;
                DeletePromotion(selectedPromotion.Id);
            }
        }



        private void DeleteCustomer(int customerId)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                try
                {
                    db.Open();
                    string query = @"DELETE FROM Buyer WHERE Id = @CustomerId";
                    var rowsAffected = db.Execute(query, new { CustomerId = customerId });
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Покупатель успешно удален.");
                        ShowAllCustomers_Click(null, null);
                    }
                    else
                    {
                        MessageBox.Show("Ошибка при удалении покупателя.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при удалении покупателя: {ex.Message}");
                }
            }
        }

        private void DeleteCountry(int countryId)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                try
                {
                    db.Open();
                    string query = @"DELETE FROM Countries WHERE Id = @CountryId";
                    var rowsAffected = db.Execute(query, new { CountryId = countryId });
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Страна успешно удалена.");
                        ShowAllCountries_Click(null, null);
                    }
                    else
                    {
                        MessageBox.Show("Ошибка при удалении страны.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при удалении страны: {ex.Message}");
                }
            }
        }


        private void DeleteCity(int cityId)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                try
                {
                    db.Open();
                    string query = @"DELETE FROM City WHERE Id = @CityId";
                    var rowsAffected = db.Execute(query, new { CityId = cityId });
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Город успешно удален.");
                        ShowAllCities_Click(null, null);
                    }
                    else
                    {
                        MessageBox.Show("Ошибка при удалении города.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при удалении города: {ex.Message}");
                }
            }
        }

        private void DeleteSection(int sectionId)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                try
                {
                    db.Open();
                    string query = @"DELETE FROM ProductSection WHERE Id = @SectionId";
                    var rowsAffected = db.Execute(query, new { SectionId = sectionId });
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Раздел успешно удален.");
                        ShowAllSections_Click(null, null);
                    }
                    else
                    {
                        MessageBox.Show("Ошибка при удалении раздела.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при удалении раздела: {ex.Message}");
                }
            }
        }

        private void DeletePromotion(int promotionId)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                try
                {
                    db.Open();
                    string query = @"DELETE FROM Promotion WHERE Id = @PromotionId";
                    var rowsAffected = db.Execute(query, new { PromotionId = promotionId });
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Акция успешно удалена.");
                        ShowAllPromotions_Click(null, null);
                    }
                    else
                    {
                        MessageBox.Show("Ошибка при удалении акции.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при удалении акции: {ex.Message}");
                }
            }
        }

    }
}