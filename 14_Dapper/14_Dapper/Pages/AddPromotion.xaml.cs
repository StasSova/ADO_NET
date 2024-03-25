using _14_Dapper.DbEntity;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace _14_Dapper.Pages
{
    /// <summary>
    /// Interaction logic for AddPromotion.xaml
    /// </summary>
    public partial class AddPromotion : Window
    {
        private string connectionString;
        private Promotion existingPromotion;
        private bool isUpdate;

        public AddPromotion(string connectionString)
        {
            InitializeComponent();
            this.connectionString = connectionString;
            LoadCountries();
            LoadSections();
            isUpdate = false;
        }

        public AddPromotion(string connectionString, Promotion existingPromotion) : this(connectionString)
        {
            this.existingPromotion = existingPromotion;
            CountryComboBox.SelectedValue = existingPromotion.CountrieId;
            SectionComboBox.SelectedValue = existingPromotion.ProductSectionId;
            DiscountTextBox.Text = existingPromotion.Discount.ToString();
            ExpirationDatePicker.SelectedDate = existingPromotion.ExpirationDate;
            isUpdate = true;
        }

        private void LoadCountries()
        {
            try
            {
                using (IDbConnection db = new SqlConnection(connectionString))
                {
                    db.Open();
                    var countries = db.Query<Country>("SELECT * FROM Countries");
                    CountryComboBox.ItemsSource = countries;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки списка стран: {ex.Message}");
            }
        }

        private void LoadSections()
        {
            try
            {
                using (IDbConnection db = new SqlConnection(connectionString))
                {
                    db.Open();
                    var sections = db.Query<ProductSection>("SELECT * FROM ProductSection");
                    SectionComboBox.ItemsSource = sections;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки списка разделов: {ex.Message}");
            }
        }

        private void SavePromotion_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int countryId = (int)CountryComboBox.SelectedValue;
                int sectionId = (int)SectionComboBox.SelectedValue;
                float discount = float.Parse(DiscountTextBox.Text);
                DateTime expirationDate = ExpirationDatePicker.SelectedDate ?? DateTime.Now;

                using (IDbConnection db = new SqlConnection(connectionString))
                {
                    db.Open();
                    string query;
                    if (isUpdate)
                    {
                        // Получаем Id существующей акции
                        int promotionId = existingPromotion.Id;
                        query = @"UPDATE Promotion SET CountrieId = @CountryId, ProductSectionId = @SectionId, Discount = @Discount, ExpirationDate = @ExpirationDate WHERE Id = @Id";
                        var rowsAffected = db.Execute(query, new { CountryId = countryId, SectionId = sectionId, Discount = discount, ExpirationDate = expirationDate, Id = promotionId });
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Акция успешно обновлена.");
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Ошибка при обновлении акции.");
                        }
                    }
                    else
                    {
                        query = @"INSERT INTO Promotion (CountrieId, ProductSectionId, Discount, ExpirationDate) VALUES (@CountryId, @SectionId, @Discount, @ExpirationDate)";
                        var rowsAffected = db.Execute(query, new { CountryId = countryId, SectionId = sectionId, Discount = discount, ExpirationDate = expirationDate });
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Акция успешно сохранена.");
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Ошибка при сохранении акции.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении акции: {ex.Message}");
            }
        }

    }
}
