using _14_Dapper.DbEntity;
using System;
using System.Collections.Generic;
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

namespace _14_Dapper.Pages
{
    /// <summary>
    /// Interaction logic for AddCity.xaml
    /// </summary>
    public partial class AddCity : Window
    {
        private string connectionString;
        private City existingCity;
        private bool isUpdate;

        public AddCity(string connectionString)
        {
            InitializeComponent();
            this.connectionString = connectionString;
            isUpdate = false;
            LoadCountries();
        }

        public AddCity(string connectionString, City existingCity) : this(connectionString)
        {
            this.existingCity = existingCity;
            CityNameTextBox.Text = existingCity.Name;
            CountryComboBox.SelectedValue = existingCity.CountrieId;
            isUpdate = true;
        }

        private void LoadCountries()
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                try
                {
                    db.Open();
                    var countries = db.Query<Country>("SELECT * FROM Countries");
                    CountryComboBox.ItemsSource = countries;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при загрузке стран: {ex.Message}");
                }
            }
        }

        private void SaveCity_Click(object sender, RoutedEventArgs e)
        {
            string cityName = CityNameTextBox.Text;
            Country country = (Country)CountryComboBox.SelectedValue;

            using (IDbConnection db = new SqlConnection(connectionString))
            {
                try
                {
                    db.Open();
                    if (isUpdate)
                    {
                        string query = @"UPDATE City SET Name = @Name, CountrieId = @CountryId WHERE Id = @Id";
                        var rowsAffected = db.Execute(query, new { Name = cityName, CountryId = country.Id, Id = existingCity.Id });
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Город успешно обновлен.");
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Ошибка при обновлении города.");
                        }
                    }
                    else
                    {
                        string query = @"INSERT INTO City (Name, CountrieId) VALUES (@Name, @CountryId)";
                        var rowsAffected = db.Execute(query, new { Name = cityName, CountryId = country.Id });
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Город успешно добавлен.");
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Ошибка при добавлении города.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при {(isUpdate ? "обновлении" : "добавлении")} города: {ex.Message}");
                }
            }
        }
    }
}
