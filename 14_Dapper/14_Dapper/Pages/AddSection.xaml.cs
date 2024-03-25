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
    /// <summary>
    /// Interaction logic for AddSection.xaml
    /// </summary>
    public partial class AddSection : Window
    {
        private string connectionString;
        private ProductSection existingSection;
        private bool isUpdate;

        public AddSection(string connectionString)
        {
            InitializeComponent();
            this.connectionString = connectionString;
            isUpdate = false;
        }

        public AddSection(string connectionString, ProductSection existingSection) : this(connectionString)
        {
            this.existingSection = existingSection;
            SectionNameTextBox.Text = existingSection.Name;
            isUpdate = true;
        }

        private void SaveSection_Click(object sender, RoutedEventArgs e)
        {
            string sectionName = SectionNameTextBox.Text;

            using (IDbConnection db = new SqlConnection(connectionString))
            {
                try
                {
                    db.Open();
                    if (isUpdate)
                    {
                        string query = @"UPDATE ProductSection SET Name = @Name WHERE Id = @Id";
                        var rowsAffected = db.Execute(query, new { Name = sectionName, Id = existingSection.Id });
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Раздел успешно обновлен.");
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Ошибка при обновлении раздела.");
                        }
                    }
                    else
                    {
                        string query = @"INSERT INTO ProductSection (Name) VALUES (@Name)";
                        var rowsAffected = db.Execute(query, new { Name = sectionName });
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Раздел успешно добавлен.");
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Ошибка при добавлении раздела.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при {(isUpdate ? "обновлении" : "добавлении")} раздела: {ex.Message}");
                }
            }
        }
    }
}
