using System;
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
using _14_Dapper.DbEntity;

namespace _14_Dapper.Pages
{
    public partial class AddCountrie : Window
    {
        private string connectionString;
        private Country existingCountry;
        private bool isUpdate;

        public AddCountrie(string connectionString)
        {
            InitializeComponent();
            this.connectionString = connectionString;
            isUpdate = false;
        }

        public AddCountrie(string connectionString, Country existingCountry) : this(connectionString)
        {
            this.existingCountry = existingCountry;
            CountryNameTextBox.Text = existingCountry.Name;
            isUpdate = true;
        }

        private void SaveCountry_Click(object sender, RoutedEventArgs e)
        {
            string countryName = CountryNameTextBox.Text;

            using (IDbConnection db = new SqlConnection(connectionString))
            {
                try
                {
                    db.Open();
                    if (isUpdate)
                    {
                        string query = @"UPDATE Countries SET Name = @Name WHERE Id = @Id";
                        var rowsAffected = db.Execute(query, new { Name = countryName, Id = existingCountry.Id });
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Страна успешно обновлена.");
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Ошибка при обновлении страны.");
                        }
                    }
                    else
                    {
                        string query = @"INSERT INTO Countries (Name) VALUES (@Name)";
                        var rowsAffected = db.Execute(query, new { Name = countryName });
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Страна успешно добавлена.");
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Ошибка при добавлении страны.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при {(isUpdate ? "обновлении" : "добавлении")} страны: {ex.Message}");
                }
            }
        }
    }
}

