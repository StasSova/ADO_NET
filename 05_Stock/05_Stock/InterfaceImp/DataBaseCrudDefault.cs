using _05_Stock.Interface;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json.Linq;
using System.IO;

namespace _05_Stock.InterfaceImp
{
    public partial class DataBaseCrudDefault : ObservableObject, IDataBaseCrud
    {
        [ObservableProperty] public string? connectionString;
        [ObservableProperty] public DataSet dataset = new();
        SqlConnection connection;
        Dictionary<string, SqlDataAdapter> adapters = new();
        Dictionary<string, SqlCommandBuilder> builders = new();
        Dictionary<string, DataTable> tables = new();
        public DataBaseCrudDefault()
        {

            ConnectionString = LoadConnectionString();
            connection = new SqlConnection(ConnectionString);

            CreatePrototypeTable();
        }
        public void AddProduct(string name, int typeId, int supplierId, int count, decimal costPrice, DateTime dateDelivery)
        {
            // Получаем таблицу продуктов
            DataTable productTable = dataset.Tables["Product"];

            // Создаем новую строку для продукта
            DataRow newRow = productTable.NewRow();
            newRow["Name"] = name;
            newRow["Type_ID"] = typeId;
            newRow["Supplier_ID"] = supplierId;
            newRow["Count"] = count;
            newRow["Cost_Price"] = costPrice;
            newRow["Date_Delivery"] = dateDelivery;

            // Добавляем новую строку в таблицу продуктов
            productTable.Rows.Add(newRow);
            adapters["Product"].Update(dataset,"Product");
        }
        public void AddTypeOfProduct(string type)
        {
            // Получаем таблицу типов продуктов
            DataTable typeTable = dataset.Tables["TypeOfProduct"];

            // Создаем новую строку для типа продукта
            DataRow newRow = typeTable.NewRow();
            newRow["Type"] = type;

            // Добавляем новую строку в таблицу типов продуктов
            typeTable.Rows.Add(newRow);
            adapters["TypeOfProduct"].Update(dataset, "TypeOfProduct");

        }

        public void AddSupplier(string name)
        {
            // Получаем таблицу поставщиков
            DataTable supplierTable = dataset.Tables["Supplier"];

            // Создаем новую строку для поставщика
            DataRow newRow = supplierTable.NewRow();
            newRow["Name"] = name;

            // Добавляем новую строку в таблицу поставщиков
            supplierTable.Rows.Add(newRow);
            adapters["Supplier"].Update(dataset, "Supplier");
        }


        private void CreatePrototypeTable()
        {
            // TypeOfProduct
            adapters.Add("TypeOfProduct", new SqlDataAdapter("select * from TypeOfProduct", connection));
            builders.Add("TypeOfProduct", new SqlCommandBuilder(adapters["TypeOfProduct"]));
            tables.Add("TypeOfProduct", dataset.Tables.Add("TypeOfProduct"));
            DataTable typeOfProductTable = tables["TypeOfProduct"];
            typeOfProductTable.Columns.Add("Id", typeof(int));
            typeOfProductTable.Columns.Add("Type", typeof(string));
            typeOfProductTable.Constraints.Add("PK_TypeOfProduct", typeOfProductTable.Columns["Id"], true); // Первичный ключ
            typeOfProductTable.Columns["Id"].AutoIncrement = true; // Identity
            typeOfProductTable.Columns["Id"].AllowDBNull = false; // Недопустима пустая ячейка
            typeOfProductTable.Columns["Type"].AllowDBNull = false;
            adapters["TypeOfProduct"]?.Fill(dataset, "TypeOfProduct");

            // Supplier
            adapters.Add("Supplier", new SqlDataAdapter("select * from Supplier", connection));
            builders.Add("Supplier", new SqlCommandBuilder(adapters["Supplier"]));
            tables.Add("Supplier", dataset.Tables.Add("Supplier"));
            DataTable supplierTable = tables["Supplier"];
            supplierTable.Columns.Add("Id", typeof(int));
            supplierTable.Columns.Add("Name", typeof(string));
            supplierTable.Constraints.Add("PK_Supplier", supplierTable.Columns["Id"], true); // Первичный ключ
            supplierTable.Columns["Id"].AutoIncrement = true; // Identity
            supplierTable.Columns["Id"].AllowDBNull = false; // Недопустима пустая ячейка
            supplierTable.Columns["Name"].AllowDBNull = false;
            adapters["Supplier"]?.Fill(dataset, "Supplier");

            // Product
            adapters.Add("Product", new SqlDataAdapter("select * from Product", connection));
            builders.Add("Product", new SqlCommandBuilder(adapters["Product"]));
            tables.Add("Product", dataset.Tables.Add("Product"));
            DataTable productTable = tables["Product"];
            productTable.Columns.Add("Id", typeof(int));
            productTable.Columns.Add("Name", typeof(string));
            productTable.Columns.Add("Type_ID", typeof(int));
            productTable.Columns.Add("Supplier_ID", typeof(int));
            productTable.Columns.Add("Count", typeof(int));
            productTable.Columns.Add("Cost_Price", typeof(decimal));
            productTable.Columns.Add("Date_Delivery", typeof(DateTime));
            productTable.Constraints.Add("PK_Product", productTable.Columns["Id"], true); // Первичный ключ
            productTable.Columns["Id"].AutoIncrement = true; // Identity
            productTable.Columns["Id"].AllowDBNull = false; // Недопустима пустая ячейка
            productTable.Columns["Name"].AllowDBNull = false;
            productTable.Columns["Count"].AllowDBNull = false;
            productTable.Columns["Cost_Price"].AllowDBNull = false;
            productTable.Columns["Date_Delivery"].AllowDBNull = false;
            adapters["Product"]?.Fill(dataset, "Product");

            ForeignKeyConstraint FK_Supplier = // Внешний ключ
                new ForeignKeyConstraint("FK_Supplier", supplierTable.Columns["id"], productTable.Columns["Supplier_ID"]);
                FK_Supplier.DeleteRule = Rule.Cascade;
                FK_Supplier.UpdateRule = Rule.Cascade;

            ForeignKeyConstraint FK_Type = // Внешний ключ
                new ForeignKeyConstraint("FK_Type", typeOfProductTable.Columns["id"], productTable.Columns["Supplier_ID"]);
                FK_Type.DeleteRule = Rule.Cascade;
                FK_Type.UpdateRule = Rule.Cascade;


        }
        private string LoadConnectionString()
        {
            try
            {
                string jsonFilePath = "D:\\Course_3_2\\ADO_NET\\05_Stock\\05_Stock\\appsettings.json";

                if (jsonFilePath != null && File.Exists(jsonFilePath))
                {
                    string jsonContent = File.ReadAllText(jsonFilePath);
                    JObject jsonObject = JObject.Parse(jsonContent);
                    return jsonObject["ConnectionStrings"]["DefaultConnection"].ToString();
                }
                else
                {
                    throw new FileNotFoundException("JSON file not found or invalid.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading connection string: " + ex.Message);
                throw; // Rethrow the exception for further handling
            }
        }
        public DataTable GetProductDataTable()
        {
            return Dataset.Tables["Product"] ?? throw new Exception("Таблица не найдена");
        }
        public DataTable GetProductDataTableWithLookup()
        {
            // Выполняем объединение таблиц "Product", "TypeOfProduct" и "Supplier" с использованием LINQ
            var query = from productRow in dataset.Tables["Product"].AsEnumerable()
                        join typeRow in dataset.Tables["TypeOfProduct"].AsEnumerable()
                            on productRow.Field<int>("Type_ID") equals typeRow.Field<int>("Id") into typeJoin
                        from type in typeJoin.DefaultIfEmpty()
                        join supplierRow in dataset.Tables["Supplier"].AsEnumerable()
                            on productRow.Field<int>("Supplier_ID") equals supplierRow.Field<int>("Id") into supplierJoin
                        from supplier in supplierJoin.DefaultIfEmpty()
                        select new
                        {
                            ProductId = productRow.Field<int>("Id"),
                            ProductName = productRow.Field<string>("Name"),
                            TypeName = type?.Field<string>("Type"),
                            SupplierName = supplier?.Field<string>("Name"),
                            Count = productRow.Field<int>("Count"),
                            CostPrice = productRow.Field<decimal>("Cost_Price"),
                            DateDelivery = productRow.Field<DateTime>("Date_Delivery")
                        };
            // Создаем новую DataTable и добавляем в нее результаты объединения
            DataTable resultTable = new DataTable();

            resultTable.TableName = "Product";
            resultTable.Columns.Add("ProductId", typeof(int));
            resultTable.Columns.Add("ProductName", typeof(string));
            resultTable.Columns.Add("TypeName", typeof(string));
            resultTable.Columns.Add("SupplierName", typeof(string));
            resultTable.Columns.Add("Count", typeof(int));
            resultTable.Columns.Add("CostPrice", typeof(decimal));
            resultTable.Columns.Add("DateDelivery", typeof(DateTime));

            foreach (var item in query)
            {
                resultTable.Rows.Add(item.ProductId, item.ProductName, item.TypeName, item.SupplierName, item.Count, item.CostPrice, item.DateDelivery);
            }

            return resultTable;
        }
        public async Task UpdateValue(DataRow row, string columnName, object newValue)
        {
            try
            {
                UpdateProduct(row, columnName, newValue);
            }
            catch (Exception ex)
            {
                // Обработка ошибок
                throw;
            }
        }

        private void UpdateProduct(DataRow row, string columnName, object newValue)
        {
            try
            {
                DataTable dt = tables[row.Table.TableName];
                DataRow rw = dt.Select("id = " + row.ItemArray[0])[0];
                string col = columnName switch
                {
                    "ProductId" => "Id",
                    "ProductName" => "Name",
                    "TypeName" => "Type_ID",
                    "Type" => "Type",
                    "Name" => "Name",
                    "SupplierName" => "Supplier_ID",
                    "Count" => "Count",
                    "CostPrice" => "Cost_Price",
                    "DateDelivery" => "Date_Delivery",
                    _ => throw new Exception("Поле не найдено") // По умолчанию оставляем текущее имя столбца
                };
                // Если изменились внешние ключи, обновляем их значения в зависимых таблицах
                if (columnName == "TypeName")
                {
                    // Находим соответствующий ID в таблице TypeOfProduct
                    DataRow[] typeRows = tables["TypeOfProduct"].Select($"Type = '{newValue}'");
                    if (typeRows.Length > 0)
                    {
                        int typeId = typeRows[0].Field<int>("Id");
                        rw[col] = typeId;
                    }
                    else throw new Exception("Такого типа не существует. Добавьте его либо повторите попытку.");
                }
                else if (columnName == "SupplierName")
                {
                    // Находим соответствующий ID в таблице Supplier
                    DataRow[] supplierRows = tables["Supplier"].Select($"Name = '{newValue}'");
                    if (supplierRows.Length > 0)
                    {
                        int supplierId = supplierRows[0].Field<int>("Id");
                        rw[col] = supplierId;
                    }
                    else throw new Exception("Такого поставщика не существует. Добавьте его либо повторите попытку.");
                }
                else
                {
                    rw[col] = newValue;
                }

                // Выполняем обновление базы данных через DataAdapter
                adapters[rw.Table.TableName].Update(dataset, rw.Table.TableName);
            }
            catch (Exception ex)
            {
                // Обработка ошибок
                MessageBox.Show($"Ошибка при обновлении данных: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                throw;
            }
        }

        public void Remove(DataRow row)
        {
            DataTable dt = tables[row.Table.TableName];
            DataRow rw = dt.Select("id = " + row.ItemArray[0])[0];
            dt.Rows.Remove(rw);
            if (dt.TableName == "TypeOfProduct")
            {
                adapters[dt.TableName].Update(dataset, rw.Table.TableName);
            }
            else if (dt.TableName == "Supplier")
            {
                adapters[dt.TableName].Update(dataset, rw.Table.TableName);
            }
            adapters["Product"].Update(dataset, "Product");
        }


        public DataTable GetTypeOfProductDataTable()
        {
            return Dataset.Tables["TypeOfProduct"] ?? throw new Exception("Таблица не найдена");
        }
        public DataTable GetSupplierDataTable()
        {
            return Dataset.Tables["Supplier"] ?? throw new Exception("Таблица не найдена");
        }
        
        public Task UpdateTable(string tableName)
        {
            try
            {
                adapters[tableName].Update(dataset, tableName);
            }
            catch { throw; };
            return Task.CompletedTask;
        }
    }
}
