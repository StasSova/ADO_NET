using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows;
using _02_ConnectionToDB_RequestToDB.MVVM;
using System.Windows.Input;
using System.Data;
using _02_ConnectionToDB_RequestToDB.Response;

namespace _02_ConnectionToDB_RequestToDB.Service
{
    public class DB_Crud
    {
        public readonly string? ConnectionString;
        public readonly string? Database;
        public readonly string? User;
        private SqlConnection connect;
        private SqlCommand command;
        public bool Connected
        {
            get { return connect.State != System.Data.ConnectionState.Closed; }
        }
        public DB_Crud()
        {
            Database = "Vegetables_Fruit";
            User = "DESKTOP-DSCJOEB\\MSSQLSERVERDEV";
            ConnectionString = $@"Initial Catalog={Database};Data Source={User};Integrated Security=SSPI";
        }
        public DB_Crud(string connectionString) :base()
        {
            ConnectionString = connectionString;
        }
        ~DB_Crud()
        {
            if (Connected) { Disconnect(); }
        }
        public bool Connection()
        {
            connect = new SqlConnection(ConnectionString);
            command = new SqlCommand();
            try
            {
                connect.Open();
                return true;
            }
            catch { throw; }
            finally
            {
                connect.Close();
            }
        }
        public List<Product> ExecuteReader(string request)
        {
            try
            {
                connect.Open();
                command.Connection = connect;
                command.CommandText = request;
                SqlDataReader reader = command.ExecuteReader();
                List<Product> products = new();
                while (reader.Read())
                {
                    try
                    {
                        int id = Convert.ToInt32(reader["ID"]);
                        string name = Convert.ToString(reader["Name"]);
                        int typeID = Convert.ToInt32(reader["Type_ID"]);
                        string color = Convert.ToString(reader["Color"]);
                        int calories = Convert.ToInt32(reader["Calories"]);

                        Product obj = new() { 
                            ID = id,
                            Name = name,
                            TypeID = typeID,
                            Color = color,
                            Calories = calories,
                        };
                        products.Add(obj);
                    }
                    catch { continue; }
                }
                reader.Close();
                return products;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                command.Dispose();
                connect.Close();
            }
            return null;
        }
        public List<Product> GetAllColor(string request)
        {
            try
            {
                connect.Open();
                command.Connection = connect;
                command.CommandText = request;
                SqlDataReader reader = command.ExecuteReader();
                List<Product> products = new();
                while (reader.Read())
                {
                    try
                    {
                        string color = Convert.ToString(reader["Color"]);
                        Product obj = new()
                        {
                            ID = null,
                            Name = null,
                            TypeID = null,
                            Color = color,
                            Calories = null,
                        };
                        products.Add(obj);
                    }
                    catch { continue; }
                }
                reader.Close();
                return products;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                command.Dispose();
                connect.Close();
            }
            return null;
        }
        public Dictionary<int,string> GetTypesProduct()
        {
            try
            {
                connect.Open();
                command.Connection = connect;
                command.CommandText = "select * from TypeOfProduct";
                SqlDataReader reader = command.ExecuteReader();
                Dictionary<int, string> types = new();
                while (reader.Read())
                {
                    try
                    {
                        int id = Convert.ToInt32(reader["ID"]);
                        string type = Convert.ToString(reader["Type"]);
                        types.Add(id,type);
                    }
                    catch { continue; }
                }
                reader.Close();
                return types;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                command.Dispose();
                connect.Close();
            }
            return null;
        }
        public double GetNumber(string request)
        {
            try
            {
                connect.Open();
                command.Connection = connect;
                command.CommandText = request;
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                double number = double.Parse(reader[0].ToString());
                reader.Close();
                return number;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                command.Dispose();
                connect.Close();
            }
            return -1;
        }
        private void Disconnect()
        {
            try
            {
                command.Dispose();
                connect.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error"); }
        }
    }
}
