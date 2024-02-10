using System.Data.SqlClient;

class Program
{

    private static string User = "DESKTOP-DSCJOEB\\MSSQLSERVERDEV"; 
    private static string Database = "03_Products";
    public static string connectionString = $@"Initial Catalog={Database};Data Source={User};Integrated Security=SSPI";
    static void Main()
    {
        string key;
        int value;
        bool exit = false;
        do
        {
            Console.WriteLine(@"What do you want to do?
    1. Display all products.
    2. Display of all types of products.
    3. Display of all suppliers.
    4. Show products with maximum quantity.
    5. Show products with a minimum quantity.
    6. Show the product with the lowest cost.
    7. Show the product with the maximum cost.
    8. Show products of a given category.
    9. Show products from a given supplier.
    10. Show the oldest item in stock.
    11. Show the average number of products for each
    type of product.
    12. Exit.");
            key = Console.ReadLine();

            // Проверяем введенное значение
            switch (key)
            {
                case "1": { CrudOperation.PrintAllProducts(); break; }
                case "2": { CrudOperation.PrintAllTypes(); break; }
                case "3": { CrudOperation.PrintAllSuppliers(); break; }
                case "4": { CrudOperation.PrintCountProducts(isMax: true); break; }
                case "5": { CrudOperation.PrintCountProducts(isMax: false); break; }
                case "6": { CrudOperation.PrintPriceProducts(isMax: false); break; }
                case "7": { CrudOperation.PrintPriceProducts(isMax: true); break; }
                case "8":
                    {
                        Console.WriteLine("Select category: ");
                        List<string> categories = CrudOperation.GetAllTypes();

                        for (int i = 0; i < categories.Count; i++)
                        {
                            Console.WriteLine($"\t{i + 1}: {categories[i]}");
                        }

                        bool validInput = false;
                        do
                        {
                            if (int.TryParse(Console.ReadLine(), out value))
                            {
                                if (value > 0 && value <= categories.Count)
                                {
                                    validInput = true;
                                }
                                else
                                {
                                    Console.WriteLine("Incorrect number. Please enter a number between 1 and " + categories.Count);
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid input. Please enter a number.");
                            }
                        } while (!validInput);
                        value -= 1;
                        CrudOperation.PrintAllProducts(ProductType: categories[value]);
                        break;
                    }
                case "9":
                    {
                        Console.WriteLine("Select supplier: ");
                        List<string> supplier = CrudOperation.GetAllSuppliers();

                        for (int i = 0; i < supplier.Count; i++)
                        {
                            Console.WriteLine($"\t{i + 1}: {supplier[i]}");
                        }

                        bool validInput = false;
                        do
                        {
                            if (int.TryParse(Console.ReadLine(), out value))
                            {
                                if (value > 0 && value <= supplier.Count)
                                {
                                    validInput = true;
                                }
                                else
                                {
                                    Console.WriteLine("Incorrect number. Please enter a number between 1 and " + supplier.Count);
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid input. Please enter a number.");
                            }
                        } while (!validInput);
                        value -= 1;
                        CrudOperation.PrintAllProducts(Supplier: supplier[value]);
                        break;
                    }
                case "10":
                    {
                        CrudOperation.PrintOldestProduct();
                        break;
                    }
                case "11":
                    {
                        CrudOperation.PrintAverageCountByType();
                        break;
                    }
                case "12":
                    {
                        exit = true;
                        break;
                    }
                default:
                    Console.WriteLine("Invalid input. Please enter a number between 1 and 12.");
                    break;
            }
            Console.WriteLine("Press any key to continue"); Console.ReadKey();
            Console.Clear();
        } while (!exit);
    }
}

class CrudOperation
{
    public static void PrintAllProducts(string Supplier = null, string ProductType = null)
    {
        // Создание подключения к базе данных
        using (SqlConnection connection = new SqlConnection(Program.connectionString))
        {
            try
            {
                // Открытие подключения
                connection.Open();

                // Выполнение SQL-запроса или вызов хранимой процедуры
                string sqlQuery = @"SELECT 
                                        P.Id,  
                                        P.Name AS ProductName, 
                                        TP.Type AS ProductType,  
                                        S.Name AS SupplierName, 
                                        P.Count,  
                                        P.Cost_Price, 
                                        P.Date_Delivery 
                                    FROM   
                                        Product AS P 
                                    JOIN   
                                        TypeOfProduct AS TP ON P.Type_ID = TP.Id 
                                    JOIN 
                                        Supplier AS S ON P.Supplier_ID = S.Id
                                ";

                if (!string.IsNullOrWhiteSpace(Supplier))
                {
                    sqlQuery += $" WHERE S.Name = '{Supplier}'";
                }

                if (!string.IsNullOrWhiteSpace(ProductType))
                {
                    sqlQuery += string.IsNullOrWhiteSpace(Supplier) ? " WHERE" : " AND";
                    sqlQuery += $" TP.Type = '{ProductType}'";
                }

                SqlCommand command = new SqlCommand(sqlQuery, connection);

                // Выполнение запроса и обработка результатов
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine($"Product Name: {reader["ProductName"]}, \n\tType: {reader["ProductType"]},\n\tSupplier: {reader["SupplierName"]}, \n\tCount: {reader["Count"]}, \n\tCost Price: {reader["Cost_Price"]}, \n\tDelivery date: {reader["Date_Delivery"]}\n\n");
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
    public static void PrintAllTypes()
    {
        // Создание подключения к базе данных
        using (SqlConnection connection = new SqlConnection(Program.connectionString))
        {
            try
            {
                // Открытие подключения
                connection.Open();

                string sqlQuery = "select Type from TypeOfProduct";
                SqlCommand command = new SqlCommand(sqlQuery, connection);

                // Выполнение запроса и обработка результатов
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine($"Type: {reader["Type"]}");
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
    public static List<string> GetAllTypes()
    {
        List<string> result = new List<string>();
        // Создание подключения к базе данных
        using (SqlConnection connection = new SqlConnection(Program.connectionString))
        {
            try
            {
                // Открытие подключения
                connection.Open();

                string sqlQuery = "select Type from TypeOfProduct";
                SqlCommand command = new SqlCommand(sqlQuery, connection);

                // Выполнение запроса и обработка результатов
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(reader["Type"].ToString());
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            return result;
        }
    }
    public static void PrintAverageCountByType()
    {
        // Создание подключения к базе данных
        using (SqlConnection connection = new SqlConnection(Program.connectionString))
        {
            try
            {
                // Открытие подключения
                connection.Open();

                // Выполнение SQL-запроса
                string sqlQuery = @"
                SELECT 
                    TP.[Type] AS ProductType,
                    AVG(P.[Count]) AS AverageCount
                FROM
                    Product AS P
                JOIN
                    TypeOfProduct AS TP ON P.Type_ID = TP.Id
                GROUP BY
                    TP.[Type]";

                SqlCommand command = new SqlCommand(sqlQuery, connection);

                // Выполнение запроса и обработка результатов
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine($"Product Type: {reader["ProductType"]}, Average Count: {reader["AverageCount"]}");
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }

    public static void PrintAllSuppliers()
    {
        // Создание подключения к базе данных
        using (SqlConnection connection = new SqlConnection(Program.connectionString))
        {
            try
            {
                // Открытие подключения
                connection.Open();

                string sqlQuery = "select Name from Supplier";
                SqlCommand command = new SqlCommand(sqlQuery, connection);

                // Выполнение запроса и обработка результатов
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine($"Name: {reader["Name"]}");
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
    public static List<string> GetAllSuppliers()
    {
        List<string> result = new();
        // Создание подключения к базе данных
        using (SqlConnection connection = new SqlConnection(Program.connectionString))
        {
            try
            {
                // Открытие подключения
                connection.Open();

                string sqlQuery = "select Name from Supplier";
                SqlCommand command = new SqlCommand(sqlQuery, connection);

                // Выполнение запроса и обработка результатов
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(reader["Name"].ToString());
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            return result;
        }
    }
    public static void PrintCountProducts(bool isMax = true)
    {
        // Создание подключения к базе данных
        using (SqlConnection connection = new SqlConnection(Program.connectionString))
        {
            try
            {
                // Открытие подключения
                connection.Open();

                // Выполнение SQL-запроса или вызов хранимой процедуры
                string sqlQuery = "SELECT TOP 1 " +
                                    "P.Id, " +
                                    "P.Name AS ProductName, " +
                                    "TP.[Type] AS ProductType, " +
                                    "S.[Name] AS SupplierName, " +
                                    "P.[Count], " +
                                    "P.Cost_Price, " +
                                    "P.Date_Delivery " +
                                "FROM " +
                                    "Product AS P " +
                                "JOIN " +
                                    "TypeOfProduct AS TP ON P.Type_ID = TP.Id " +
                                "JOIN " +
                                    "Supplier AS S ON P.Supplier_ID = S.Id " +
                                "ORDER BY " +
                                    $"P.[Count] {(isMax ? "desc" : "asc")}";

                SqlCommand command = new SqlCommand(sqlQuery, connection);

                // Выполнение запроса и обработка результатов
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine($"Product Name: {reader["ProductName"]}, \n\tType: {reader["ProductType"]},\n\tSupplier: {reader["SupplierName"]}, \n\tCount: {reader["Count"]}, \n\tCost Price: {reader["Cost_Price"]}, \n\tDelivery date: {reader["Date_Delivery"]}\n\n");
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
    public static void PrintOldestProduct()
    {
        // Создание подключения к базе данных
        using (SqlConnection connection = new SqlConnection(Program.connectionString))
        {
            try
            {
                // Открытие подключения
                connection.Open();

                // Выполнение SQL-запроса
                string sqlQuery = @"
                SELECT TOP 1
                    P.Id,
                    P.Name AS ProductName,
                    TP.[Type] AS ProductType,
                    S.[Name] AS SupplierName,
                    P.[Count],
                    P.Cost_Price,
                    P.Date_Delivery
                FROM
                    Product AS P
                JOIN
                    TypeOfProduct AS TP ON P.Type_ID = TP.Id
                JOIN
                    Supplier AS S ON P.Supplier_ID = S.Id
                ORDER BY
                    P.Date_Delivery ASC";

                SqlCommand command = new SqlCommand(sqlQuery, connection);

                // Выполнение запроса и обработка результатов
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine($"Product Name: {reader["ProductName"]}, \n\tType: {reader["ProductType"]},\n\tSupplier: {reader["SupplierName"]}, \n\tCount: {reader["Count"]}, \n\tCost Price: {reader["Cost_Price"]}, \n\tDelivery date: {reader["Date_Delivery"]}\n\n");
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }

    public static void PrintPriceProducts(bool isMax = true)
    {
        // Создание подключения к базе данных
        using (SqlConnection connection = new SqlConnection(Program.connectionString))
        {
            try
            {
                // Открытие подключения
                connection.Open();

                // Выполнение SQL-запроса или вызов хранимой процедуры
                string sqlQuery = "SELECT TOP 1 " +
                                    "P.Id, " +
                                    "P.Name AS ProductName, " +
                                    "TP.[Type] AS ProductType, " +
                                    "S.[Name] AS SupplierName, " +
                                    "P.[Count], " +
                                    "P.Cost_Price, " +
                                    "P.Date_Delivery " +
                                "FROM " +
                                    "Product AS P " +
                                "JOIN " +
                                    "TypeOfProduct AS TP ON P.Type_ID = TP.Id " +
                                "JOIN " +
                                    "Supplier AS S ON P.Supplier_ID = S.Id " +
                                "ORDER BY " +
                                    $"P.[Cost_Price] {(isMax ? "desc" : "asc")}";

                SqlCommand command = new SqlCommand(sqlQuery, connection);

                // Выполнение запроса и обработка результатов
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine($"Product Name: {reader["ProductName"]}, \n\tType: {reader["ProductType"]},\n\tSupplier: {reader["SupplierName"]}, \n\tCount: {reader["Count"]}, \n\tCost Price: {reader["Cost_Price"]}, \n\tDelivery date: {reader["Date_Delivery"]}\n\n");
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }


}