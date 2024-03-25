using _14_Dapper.DbEntity;
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
    public partial class AddBuyer : Window
    {
        private string connectionString;
        private Buyer existingBuyer;
        private bool isUpdate;

        public AddBuyer(string connectionString)
        {
            InitializeComponent();
            this.connectionString = connectionString;
            LoadCities();
            isUpdate = false;
        }

        public AddBuyer(string connectionString, Buyer existingBuyer) : this(connectionString)
        {
            this.existingBuyer = existingBuyer;
            FillForm();
            isUpdate = true;
        }

        private void LoadCities()
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                try
                {
                    db.Open();
                    var cities = db.Query<City>("SELECT * FROM City");
                    CityComboBox.ItemsSource = cities;
                    CityComboBox.DisplayMemberPath = "Name";
                    CityComboBox.SelectedValuePath = "Id";
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при загрузке списка городов: {ex.Message}");
                }
            }
        }

        private void FillForm()
        {
            FullNameTextBox.Text = existingBuyer.FullName;
            GenderComboBox.SelectedItem = existingBuyer.Gender == 'M' ? "Male" : "Female";
            EmailTextBox.Text = existingBuyer.Email;
            CityComboBox.SelectedValue = existingBuyer.CityId;
        }

        private void AddBuyer_Click(object sender, RoutedEventArgs e)
        {
            string fullName = FullNameTextBox.Text;
            char gender = GenderComboBox.SelectedItem.ToString() == "Male" ? 'M' : 'F';
            string email = EmailTextBox.Text;
            int cityId = (int)CityComboBox.SelectedValue;

            using (IDbConnection db = new SqlConnection(connectionString))
            {
                try
                {
                    db.Open();
                    if (isUpdate)
                    {
                        string query = @"UPDATE Buyer SET FullName = @FullName, Gender = @Gender, Email = @Email, CityId = @CityId WHERE Id = @Id";
                        var rowsAffected = db.Execute(query, new { FullName = fullName, Gender = gender, Email = email, CityId = cityId, Id = existingBuyer.Id });
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Покупатель успешно обновлен.");
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Ошибка при обновлении покупателя.");
                        }
                    }
                    else
                    {
                        string query = @"INSERT INTO Buyer (FullName, Gender, Email, CityId) VALUES (@FullName, @Gender, @Email, @CityId)";
                        var rowsAffected = db.Execute(query, new { FullName = fullName, Gender = gender, Email = email, CityId = cityId });
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Покупатель успешно добавлен.");
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Ошибка при добавлении покупателя.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при {(isUpdate ? "обновлении" : "добавлении")} покупателя: {ex.Message}");
                }
            }
        }
    }
}
