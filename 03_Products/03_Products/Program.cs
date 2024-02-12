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
    0. Exit.
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
    
    12. Inserting new products
    13. Inserting new types of products
    14. Inserting new suppliers

    15. Updating information about existing products
    16. Updating information about existing supplies boxes
    17. Updating information about existing types goods

    18. Removing products
    19. Removing suppliers
    20. Deleting product types

    21. Show information about the supplier with the largest quantity of goods in stock
    22. Show information about the supplier with the lowest quantity of goods in stock    
    23. Show information about the type of products with the largest quantity of goods in stock
    24. Show information about the type of products with the lowest quantity of goods in stock
    25. Show products from delivery that have passed the specified new number of days");
            key = Console.ReadLine();

            // Проверяем введенное значение
            int i;
            bool validInput;
            switch (key)
            {
                case "0":
                    {
                        exit = true;
                        break;
                    }
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

                        for (i = 0; i < categories.Count; i++)
                        {
                            Console.WriteLine($"\t{i + 1}: {categories[i]}");
                        }

                        bool validInputCat = false;
                        //int value;
                        do
                        {
                            if (int.TryParse(Console.ReadLine(), out value))
                            {
                                if (value > 0 && value <= categories.Count)
                                {
                                    validInputCat = true;
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
                        } while (!validInputCat);
                        value -= 1;
                        CrudOperation.PrintAllProducts(ProductType: categories[value]);
                        break;
                    }

                case "9":
                    {
                        Console.WriteLine("Select supplier: ");
                        List<string> supplier = CrudOperation.GetAllSuppliers();

                        for (i = 0; i < supplier.Count; i++)
                        {
                            Console.WriteLine($"\t{i + 1}: {supplier[i]}");
                        }

                        validInput = false;
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
                
                // NEW FUNC
                case "12":
                    {
                        Console.Clear();
                        // SELECT CATEGORY
                        Console.WriteLine("Select category: ");
                        Dictionary<int, string> cat = CrudOperation.GetAllTypesWithId();

                        i = 0;
                        foreach (var item in cat)
                        {
                            ++i;
                            Console.WriteLine($"\t{i}: {item.Value}");
                        }

                        validInput = false;
                        int categID;
                        do
                        {
                            if (int.TryParse(Console.ReadLine(), out categID))
                            {
                                if (categID > 0 && categID <= cat.Count)
                                {
                                    validInput = true;
                                }
                                else
                                {
                                    Console.WriteLine("Incorrect number. Please enter a number between 1 and " + cat.Count);
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid input. Please enter a number.");
                            }
                        } while (!validInput);
                        categID = cat.ElementAt(categID - 1).Key;


                        // SELECT SUPPLIER
                        Console.WriteLine($"Category: {cat[categID]}");
                        Console.WriteLine("Select supplier: ");
                        Dictionary<int, string> supp = CrudOperation.GetAllSuppliersWithId();

                        i = 0;
                        foreach (var item in supp)
                        {
                            ++i;
                            Console.WriteLine($"\t{i}: {item.Value}");
                        }

                        validInput = false;
                        int suppID;
                        do
                        {
                            if (int.TryParse(Console.ReadLine(), out suppID))
                            {
                                if (suppID > 0 && suppID <= supp.Count)
                                {
                                    validInput = true;
                                }
                                else
                                {
                                    Console.WriteLine("Incorrect number. Please enter a number between 1 and " + supp.Count);
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid input. Please enter a number.");
                            }
                        } while (!validInput);
                        suppID = supp.ElementAt(suppID - 1).Key;

                        // INPUT NAME
                        validInput = false;
                        string name;
                        do
                        {
                            Console.Clear();
                            Console.WriteLine($"Category: {cat[categID]}");
                            Console.WriteLine($"Supplier: {supp[suppID]}");
                            Console.WriteLine("Input name of product");
                            name = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(name))
                            {
                                Console.WriteLine("Invalid input. Please enter a name.");
                            }
                            else { validInput = true; }
                        } while (!validInput);


                        // INPUT COST
                        validInput = false;
                        float cost;
                        do
                        {
                            Console.Clear();
                            Console.WriteLine($"Category: {cat[categID]}");
                            Console.WriteLine($"Supplier: {supp[suppID]}");
                            Console.WriteLine($"Product name: {name}");
                            Console.WriteLine("Input cost of product: ");
                            if (float.TryParse(Console.ReadLine()?.Replace('.',','), out cost))
                            {
                                validInput |= true;
                            }
                            else
                            {
                                Console.WriteLine("Invalid input. Please enter a cost price.");
                            }
                        } while (!validInput);

                        // INPUT COUNT
                        validInput = false;
                        int number;
                        do
                        {
                            Console.Clear();
                            Console.WriteLine($"Category: {cat[categID]}");
                            Console.WriteLine($"Supplier: {supp[suppID]}");
                            Console.WriteLine($"Product name: {name}");
                            Console.WriteLine($"Product cost: {cost}");
                            Console.WriteLine("Input count of product: ");
                            if (int.TryParse(Console.ReadLine(), out number))
                            {
                                validInput = true;
                            }
                            else
                            {
                                Console.WriteLine("Invalid input. Please enter a cost price.");
                            }
                        } while (!validInput);
                        Console.Clear();
                        CrudOperation.InsertInToProducts(
                                        ProductName: name,
                                        ProductTypeId: categID,
                                        ProductSupplierId: suppID,
                                        ProductPrice: cost,
                                        ProductCount: number);


                        break;
                    }
                case "13":
                    {
                        // Получаем текущий список типов продуктов
                        List<string> currentTypes = CrudOperation.GetAllTypes();
                        // Выводим текущие типы
                        Console.WriteLine("Current types:");
                        foreach (var type in currentTypes)
                        {
                            Console.WriteLine("\t" + type);
                        }
                        // Запрашиваем у пользователя новые типы продуктов
                        Console.WriteLine("Input new types (separated by commas): ");
                        string[] newTypes = Console.ReadLine().Split(',');

                        // Удаляем лишние пробелы у каждого нового типа
                        for (i = 0; i < newTypes.Length; i++)
                        {
                            if (!string.IsNullOrWhiteSpace(newTypes[i]))
                                newTypes[i] = newTypes[i].Trim();
                        }

                        // Определяем новые типы, которых еще нет в текущем списке
                        List<string> typesToAdd = new List<string>();
                        foreach (string type in newTypes)
                        {
                            if (!currentTypes.Contains(type))
                            {
                                typesToAdd.Add(type);
                            }
                        }

                        // Если есть новые типы, добавляем их в базу данных
                        if (typesToAdd.Count > 0)
                        {
                            CrudOperation.InsertInToTypes(typesToAdd);
                        }
                        else
                        {
                            Console.WriteLine("No new types to add.");
                        }
                        break;
                    }
                case "14":
                    {
                        // Получаем текущий список поставщиков
                        List<string> currentSuppliers = CrudOperation.GetAllSuppliers();
                        // Выводим текущих поставщиков
                        Console.WriteLine("Current suppliers:");
                        foreach (var supplier in currentSuppliers)
                        {
                            Console.WriteLine("\t" + supplier);
                        }
                        // Запрашиваем у пользователя новых поставщиков
                        Console.WriteLine("Input new suppliers (separated by commas): ");
                        string[] newSuppliers = Console.ReadLine().Split(',');

                        // Удаляем лишние пробелы у каждого нового поставщика
                        for (i = 0; i < newSuppliers.Length; i++)
                        {
                            if (!string.IsNullOrWhiteSpace(newSuppliers[i]))
                                newSuppliers[i] = newSuppliers[i].Trim();
                        }

                        // Определяем новых поставщиков, которых еще нет в текущем списке
                        List<string> suppliersToAdd = new List<string>();
                        foreach (string supplier in newSuppliers)
                        {
                            if (!currentSuppliers.Contains(supplier))
                            {
                                suppliersToAdd.Add(supplier);
                            }
                        }

                        // Если есть новые поставщики, добавляем их в базу данных
                        if (suppliersToAdd.Count > 0)
                        {
                            CrudOperation.InsertInToSuppliers(suppliersToAdd);
                        }
                        else
                        {
                            Console.WriteLine("No new suppliers to add.");
                        }
                        break;
                    }
                case "15":
                    {
                        bool returnToMenu = false;
                        do
                        {
                            Console.WriteLine("Select product:");
                            int productIndex = 0;
                            List<Product> products = CrudOperation.GetAllInfoAboutProduct();
                            foreach (var p in products)
                            {
                                productIndex++;
                                Console.WriteLine($"\t{productIndex}: \tN: {p.Name}, \tT: {p.TypeName}, \tS: {p.SupplierName}, \tC: {p.Count}, \tP:{p.CostPrice}, \tD: {p.Date}");
                            }

                            validInput = false;
                            do
                            {
                                if (int.TryParse(Console.ReadLine(), out productIndex))
                                {
                                    if (productIndex > 0 && productIndex <= products.Count)
                                    {
                                        validInput = true;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Incorrect number. Please enter a number between 1 and " + products.Count);
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Invalid input. Please enter a number.");
                                }
                            } while (!validInput);
                            
                            Product selProd = products.ElementAt(productIndex - 1);

                            Console.Clear();
                            Console.WriteLine(@"
    1. Change name. (" + selProd.Name + @")
    2. Change Type. (" + selProd.TypeName + @")
    3. Change Supplier. (" + selProd.SupplierName + @")
    4. Change Count. (" + selProd.Count + @")
    5. Change Price. (" + selProd.CostPrice + @")
    6. Back to product selection.
    7. Return to main menu.");

                            string choice = Console.ReadLine();
                            switch (choice)
                            {
                                case "1":
                                    Console.WriteLine($"Old name: {selProd.Name}");
                                    string newName;
                                    bool isValidName = false;
                                    do
                                    {
                                        Console.WriteLine("Enter new name:");
                                        newName = Console.ReadLine();
                                        if (!string.IsNullOrWhiteSpace(newName))
                                        {
                                            isValidName = true;
                                        }
                                        else
                                        {
                                            Console.WriteLine("Invalid input. Please enter a non-empty name.");
                                        }
                                    } while (!isValidName);

                                    CrudOperation.ChangeProductName(selProd.ID, newName);
                                    break;
                                case "2":
                                    Dictionary<int, string> types = CrudOperation.GetAllTypesWithId();
                                    bool isValidInput = false;
                                    int newTypeId;
                                    int typeIndex = 0;
                                    do
                                    {
                                        Console.Clear();
                                        Console.WriteLine($"Old type: {selProd.TypeName}");
                                        Console.WriteLine("Select new type:");

                                        // Выводим список доступных типов
                                        foreach (var type in types)
                                        {
                                            typeIndex++;
                                            Console.WriteLine($"\t{typeIndex}: {type.Value}");
                                        }

                                        if (int.TryParse(Console.ReadLine(), out newTypeId))
                                        {
                                            if (newTypeId > 0 && newTypeId <= types.Count)
                                            {
                                                isValidInput = true;
                                            }
                                            else
                                            {
                                                Console.WriteLine($"Incorrect number. Please enter a number between 1 and {types.Count}");
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("Invalid input. Please enter a number.");
                                        }
                                    } while (!isValidInput);

                                    // Получаем идентификатор выбранного типа
                                    newTypeId = types.ElementAt(newTypeId - 1).Key;

                                    // Вызываем метод для изменения типа продукта
                                    CrudOperation.ChangeProductType(selProd.ID, newTypeId);
                                    break;
                                case "3":
                                    Dictionary<int, string> suppliers = CrudOperation.GetAllSuppliersWithId();
                                    isValidInput = false;
                                    int newSupplierId;
                                    do
                                    {
                                        Console.Clear();
                                        Console.WriteLine($"Old supplier: {selProd.SupplierName}");
                                        Console.WriteLine("Select new supplier:");

                                        // Выводим список доступных поставщиков
                                        int supplierIndex = 0;
                                        foreach (var supplier in suppliers)
                                        {
                                            supplierIndex++;
                                            Console.WriteLine($"\t{supplierIndex}: {supplier.Value}");
                                        }
                                        if (int.TryParse(Console.ReadLine(), out newSupplierId))
                                        {
                                            if (newSupplierId > 0 && newSupplierId <= suppliers.Count)
                                            {
                                                isValidInput = true;
                                            }
                                            else
                                            {
                                                Console.WriteLine($"Incorrect number. Please enter a number between 1 and {suppliers.Count}");
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("Invalid input. Please enter a number.");
                                        }
                                    } while (!isValidInput);

                                    // Получаем идентификатор выбранного поставщика
                                    newSupplierId = suppliers.ElementAt(newSupplierId - 1).Key;

                                    // Вызываем метод для изменения поставщика продукта
                                    CrudOperation.ChangeProductSupplier(selProd.ID, newSupplierId);
                                    break;
                                case "4":
                                    int newCount;
                                    isValidInput = false;
                                    do
                                    {
                                        Console.WriteLine("Enter new count:");
                                        if (int.TryParse(Console.ReadLine(), out newCount))
                                        {
                                            isValidInput = true;
                                        }
                                        else
                                        {
                                            Console.WriteLine("Invalid count. Please enter a valid integer.");
                                        }
                                    } while (!isValidInput);

                                    CrudOperation.ChangeProductCount(selProd.ID, newCount);
                                    break;
                                case "5":
                                    decimal newPrice;
                                    isValidInput = false;
                                    do
                                    {
                                        Console.WriteLine("Enter new price:");
                                        if (decimal.TryParse(Console.ReadLine(), out newPrice))
                                        {
                                            isValidInput = true;
                                        }
                                        else
                                        {
                                            Console.WriteLine("Invalid price. Please enter a valid decimal number.");
                                        }
                                    } while (!isValidInput);

                                    CrudOperation.ChangeProductPrice(selProd.ID, newPrice);
                                    break;
                                case "6":
                                    // Возврат к выбору продукта
                                    break;
                                case "7":
                                    // Возврат в основное меню
                                    returnToMenu = true; 
                                    break; 
                                default:
                                    Console.WriteLine("Invalid choice. Please enter a valid option.");
                                    break;
                            }


                        } while (!returnToMenu);
                        break;
                    }
                case "16":
                    while (true)
                    {
                        validInput = false;
                        i = 0;
                        Console.Clear();
                        // SELECT SUPPLIER
                        Console.WriteLine("Select a supplier to update (enter '0' to return to the previous menu):");
                        Dictionary<int, string> supp = CrudOperation.GetAllSuppliersWithId();

                        foreach (var item in supp)
                        {
                            ++i;
                            Console.WriteLine($"\t{i}: {item.Value}");
                        }

                        int suppID;
                        do
                        {
                            if (int.TryParse(Console.ReadLine(), out suppID))
                            {
                                if (suppID >= 0 && suppID <= supp.Count)
                                {
                                    validInput = true;
                                }
                                else
                                {
                                    Console.WriteLine("Incorrect number. Please enter a number between 1 and " + supp.Count);
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid input. Please enter a number.");
                            }
                        } while (!validInput);

                        if (suppID == 0) // Пользователь хочет вернуться к предыдущему меню
                        {
                            break;
                        }

                        suppID = supp.ElementAt(suppID - 1).Key;

                        // Теперь запрашиваем новое имя для поставщика
                        Console.WriteLine("Enter the new name for the supplier:");
                        string newSupplierName = Console.ReadLine();

                        // Проверяем, что введённое имя не пустое
                        if (string.IsNullOrWhiteSpace(newSupplierName))
                        {
                            Console.WriteLine("Invalid input. Supplier name cannot be empty.");
                            continue; // Повторяем цикл, чтобы попросить пользователя ввести имя заново
                        }
                        // Проверка на уникальность имени поставщика
                        if (supp.ContainsValue(newSupplierName))
                        {
                            Console.WriteLine("Supplier name must be unique. Please enter a different name.");
                            continue; // Повторяем цикл, чтобы попросить пользователя ввести имя заново
                        }
                        // Если все проверки прошли успешно, можно обновить информацию о поставщике
                        // Здесь нужно вызвать метод для обновления информации о поставщике с новым именем
                        CrudOperation.UpdateSupplierName(suppID, newSupplierName);
                    }
                    break;
                case "17":
                    {
                        while (true)
                        {

                            validInput = false;
                            i = 0;
                            Console.Clear();
                            // SELECT CATEGORY
                            Console.WriteLine("Select a category to update (enter '0' to return to the previous menu):");
                            Dictionary<int, string> cat = CrudOperation.GetAllTypesWithId();

                            foreach (var item in cat)
                            {
                                ++i;
                                Console.WriteLine($"\t{i}: {item.Value}");
                            }

                            int typeId;
                            do
                            {
                                if (int.TryParse(Console.ReadLine(), out typeId))
                                {
                                    if (typeId >= 0 && typeId <= cat.Count)
                                    {
                                        validInput = true;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Incorrect number. Please enter a number between 1 and " + cat.Count);
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Invalid input. Please enter a number.");
                                }
                            } while (!validInput);

                            if (typeId == 0) // Пользователь хочет вернуться к предыдущему меню
                            {
                                break;
                            }

                            typeId = cat.ElementAt(typeId - 1).Key;

                            // Теперь запрашиваем новое имя для категории товара
                            Console.WriteLine("Enter the new name for the category:");
                            string newCategoryName = Console.ReadLine();

                            // Проверяем, что введённое имя не пустое
                            if (string.IsNullOrWhiteSpace(newCategoryName))
                            {
                                Console.WriteLine("Invalid input. Category name cannot be empty.");
                                continue; // Повторяем цикл, чтобы попросить пользователя ввести имя заново
                            }
                            // Проверка на уникальность имени категории
                            if (cat.ContainsValue(newCategoryName))
                            {
                                Console.WriteLine("Category name must be unique. Please enter a different name.");
                                continue; // Повторяем цикл, чтобы попросить пользователя ввести имя заново
                            }
                            // Если все проверки прошли успешно, можно обновить информацию о категории
                            // Здесь нужно вызвать метод для обновления информации о категории с новым именем
                            CrudOperation.UpdateCategoryName(typeId, newCategoryName);
                        }
                        break;
                    }
                
                case "18":
                    {
                        while (true)
                        {
                            Console.Clear();
                            Console.WriteLine("Select product to delete (enter '0' to exit):");
                            int productIndex = 0;
                            List<Product> products = CrudOperation.GetAllInfoAboutProduct();
                            foreach (var p in products)
                            {
                                productIndex++;
                                Console.WriteLine($"\t{productIndex}: \tN: {p.Name}, \tT: {p.TypeName}, \tS: {p.SupplierName}, \tC: {p.Count}, \tP:{p.CostPrice}, \tD: {p.Date}");
                            }

                            Console.WriteLine("Enter the ID of the product to delete (or '0' to exit):");
                            int productId;
                            if (!int.TryParse(Console.ReadLine(), out productId))
                            {
                                Console.WriteLine("Invalid input. Please enter a valid product ID.");
                                continue;
                            }

                            if (productId == 0)
                            {
                                break; // Пользователь выбрал выход
                            }

                            // Проверка наличия выбранного ID в списке продуктов
                            if (productId < 1 || productId > products.Count)
                            {
                                Console.WriteLine("Invalid product ID. Please enter a valid ID.");
                                continue;
                            }

                            // Запрос на подтверждение удаления
                            Console.WriteLine($"Are you sure you want to delete the product with ID {productId}? (yes/no)");
                            string confirmation = Console.ReadLine().ToLower();

                            if (confirmation == "yes")
                            {
                                // Выполнение удаления продукта
                                Product productToDelete = products[productId - 1];
                                CrudOperation.DeleteProduct(productToDelete.ID);
                                Console.WriteLine("Product deleted successfully.");
                            }
                            else if (confirmation == "no")
                            {
                                Console.WriteLine("Deletion cancelled.");
                            }
                            else
                            {
                                Console.WriteLine("Invalid input. Please enter 'yes' or 'no'.");
                            }
                        }
                        break;
                    }
                case "19":
                    {
                        while (true)
                        {
                            // Выводим список всех поставщиков
                            Console.WriteLine("Select a supplier to delete (enter '0' to return to the previous menu):");
                            Dictionary<int, string> suppliers = CrudOperation.GetAllSuppliersWithId();

                            // Выводим список всех поставщиков
                            i = 0;
                            foreach (var item in suppliers)
                            {
                                i++;
                                Console.WriteLine($"\t{i}: {item.Value}");
                            }

                            // Считываем выбор пользователя
                            int selectedSupplierIndex;
                            bool isValidInput = int.TryParse(Console.ReadLine(), out selectedSupplierIndex);
                            if (!isValidInput || (selectedSupplierIndex < 0 || selectedSupplierIndex > suppliers.Count))
                            {
                                Console.WriteLine("Invalid input. Please enter a valid number.");
                                continue;
                            }

                            if (selectedSupplierIndex == 0)
                            {
                                // Пользователь хочет вернуться к предыдущему меню
                                break;
                            }

                            // Получаем ID выбранного поставщика
                            int selectedSupplierId = suppliers.ElementAt(selectedSupplierIndex - 1).Key;

                            // Запрашиваем подтверждение удаления
                            Console.WriteLine($"Are you sure you want to delete the supplier '{suppliers[selectedSupplierId]}'? (yes/no)");
                            string confirmation = Console.ReadLine().ToLower();

                            if (confirmation == "yes")
                            {
                                // Удаляем выбранного поставщика
                                CrudOperation.DeleteSupplier(selectedSupplierId);
                                break;
                            }
                            else if (confirmation == "no")
                            {
                                // Пользователь отменил удаление, возвращаемся к списку поставщиков
                                continue;
                            }
                            else
                            {
                                Console.WriteLine("Invalid input. Please enter 'yes' or 'no'.");
                                continue;
                            }
                        }
                        break;
                    }
                case "20":
                    {
                        while (true)
                        {
                            // Выводим список всех типов продуктов
                            Console.WriteLine("Select a product type to delete (enter '0' to return to the previous menu):");
                            Dictionary<int, string> productTypes = CrudOperation.GetAllTypesWithId();

                            // Выводим список всех типов продуктов
                            i = 0;
                            foreach (var item in productTypes)
                            {
                                i++;
                                Console.WriteLine($"\t{i}: {item.Value}");
                            }

                            // Считываем выбор пользователя
                            int selectedTypeIndex;
                            bool isValidInput = int.TryParse(Console.ReadLine(), out selectedTypeIndex);
                            if (!isValidInput || (selectedTypeIndex < 0 || selectedTypeIndex > productTypes.Count))
                            {
                                Console.WriteLine("Invalid input. Please enter a valid number.");
                                continue;
                            }

                            if (selectedTypeIndex == 0)
                            {
                                // Пользователь хочет вернуться к предыдущему меню
                                break;
                            }

                            // Получаем ID выбранного типа продукта
                            int selectedTypeId = productTypes.ElementAt(selectedTypeIndex - 1).Key;

                            // Запрашиваем подтверждение удаления
                            Console.WriteLine($"Are you sure you want to delete the product type '{productTypes[selectedTypeId]}'? (yes/no)");
                            string confirmation = Console.ReadLine().ToLower();

                            if (confirmation == "yes")
                            {
                                // Удаляем выбранный тип продукта
                                CrudOperation.DeleteProductType(selectedTypeId);
                                break;
                            }
                            else if (confirmation == "no")
                            {
                                // Пользователь отменил удаление, возвращаемся к списку типов продуктов
                                continue;
                            }
                            else
                            {
                                Console.WriteLine("Invalid input. Please enter 'yes' or 'no'.");
                                continue;
                            }
                        }
                        break;
                    }

                case "21": {CrudOperation.ShowSupplierWithMinStockQuantity(false); break; }
                case "22": {CrudOperation.ShowSupplierWithMinStockQuantity(true); break; }
                
                case "23": {CrudOperation.ShowTypeWithMaxStockQuantity(false); break; }
                case "24": {CrudOperation.ShowTypeWithMaxStockQuantity(true); break; }

                case "25":
                    {
                        Console.WriteLine("Enter the number of days:");
                        int days;
                        while (!int.TryParse(Console.ReadLine(), out days) || days <= 0)
                        {
                            Console.WriteLine("Invalid input. Please enter a positive integer value for the number of days.");
                        }

                        CrudOperation.ShowProductsFromDelivery(days);
                        break;
                    }

                default:
                    Console.WriteLine("Invalid input. Please enter a number between 0 and 25.");
                    break;
            }
            Console.WriteLine("Press any key to continue"); Console.ReadKey();
            Console.Clear();
        } while (!exit);
    }
}

public struct Product
{
    public int ID;
    public string Name;
    public int TypeID;
    public string TypeName;
    public string SupplierName;
    public int SupplierID;
    public int Count;
    public decimal CostPrice;
    public DateTime Date;
}
class CrudOperation
{
    public static void ShowProductsFromDelivery(int days)
    {
        try
        {
            using (SqlConnection connection = new SqlConnection(Program.connectionString))
            {
                connection.Open();

                // Выполняем SQL-запрос для получения информации о товарах с поставки, которым прошло заданное количество дней
                string sqlQuery = $@"SELECT P.Name AS ProductName, TP.Type AS ProductType, S.Name AS SupplierName, P.Count AS Quantity, P.Cost_Price AS Price, P.Date_Delivery AS DeliveryDate
                                FROM Product AS P
                                JOIN TypeOfProduct AS TP ON P.Type_ID = TP.Id
                                JOIN Supplier AS S ON P.Supplier_ID = S.Id
                                WHERE DATEDIFF(DAY, P.Date_Delivery, GETDATE()) >= {days}";

                SqlCommand command = new SqlCommand(sqlQuery, connection);
                SqlDataReader reader = command.ExecuteReader();

                // Проверяем, есть ли результаты запроса
                if (reader.HasRows)
                {
                    Console.WriteLine($"Products from delivery that are {days} days old or more:");
                    while (reader.Read())
                    {
                        string productName = (string)reader["ProductName"];
                        string productType = (string)reader["ProductType"];
                        string supplierName = (string)reader["SupplierName"];
                        int quantity = (int)reader["Quantity"];
                        decimal price = (decimal)reader["Price"];
                        DateTime deliveryDate = (DateTime)reader["DeliveryDate"];

                        Console.WriteLine($"Product: {productName}, Type: {productType}, Supplier: {supplierName}, Quantity: {quantity}, Price: {price}, Delivery Date: {deliveryDate.ToShortDateString()}");
                    }
                }
                else
                {
                    Console.WriteLine($"No products from delivery that are {days} days old or more.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    public static void ShowSupplierWithMinStockQuantity(bool isSmallest)
    {
        try
        {
            using (SqlConnection connection = new SqlConnection(Program.connectionString))
            {
                connection.Open();

                // Определяем порядок сортировки в зависимости от значения параметра isSmallest
                string order = isSmallest ? "ASC" : "DESC";

                // Выполняем SQL-запрос для получения информации о поставщике с наименьшим количеством товаров на складе
                string sqlQuery = $@"SELECT TOP 1 S.Name AS SupplierName, SUM(P.Count) AS TotalStockQuantity
                                FROM Product AS P
                                JOIN Supplier AS S ON P.Supplier_ID = S.Id
                                GROUP BY S.Name
                                ORDER BY TotalStockQuantity {order}";

                SqlCommand command = new SqlCommand(sqlQuery, connection);
                SqlDataReader reader = command.ExecuteReader();

                // Проверяем, есть ли результаты запроса
                if (reader.Read())
                {
                    string supplierName = (string)reader["SupplierName"];
                    int totalStockQuantity = (int)reader["TotalStockQuantity"];

                    Console.WriteLine($"Supplier with the {(isSmallest ? "smallest" : "largest")} stock quantity: {supplierName}, Total stock quantity: {totalStockQuantity}");
                }
                else
                {
                    Console.WriteLine("No data found.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
    public static void ShowTypeWithMaxStockQuantity(bool isSmallest)
    {
        try
        {
            using (SqlConnection connection = new SqlConnection(Program.connectionString))
            {
                connection.Open();

                // Определяем порядок сортировки в зависимости от значения параметра isSmallest
                string order = isSmallest ? "ASC" : "DESC";

                // Выполняем SQL-запрос для получения информации о типе товара с наибольшим количеством товаров на складе
                string sqlQuery = $@"SELECT TOP 1 TP.Type AS ProductType, SUM(P.Count) AS TotalStockQuantity
                                FROM Product AS P
                                JOIN TypeOfProduct AS TP ON P.Type_ID = TP.Id
                                GROUP BY TP.Type
                                ORDER BY TotalStockQuantity {order}";

                SqlCommand command = new SqlCommand(sqlQuery, connection);
                SqlDataReader reader = command.ExecuteReader();

                // Проверяем, есть ли результаты запроса
                if (reader.Read())
                {
                    string productType = (string)reader["ProductType"];
                    int totalStockQuantity = (int)reader["TotalStockQuantity"];

                    Console.WriteLine($"Product type with the {(isSmallest ? "smallest" : "largest")} stock quantity: {productType}, Total stock quantity: {totalStockQuantity}");
                }
                else
                {
                    Console.WriteLine("No data found.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
    public static void DeleteProductType(int typeId)
    {
        try
        {
            using (SqlConnection connection = new SqlConnection(Program.connectionString))
            {
                connection.Open();

                // Удаляем тип продукта из таблицы TypeOfProduct по его ID
                string sqlQuery = "DELETE FROM TypeOfProduct WHERE Id = @TypeId";
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                command.Parameters.AddWithValue("@TypeId", typeId);

                int rowsAffected = command.ExecuteNonQuery();

                // Проверяем количество затронутых строк
                if (rowsAffected > 0)
                {
                    Console.WriteLine("Product type deleted successfully.");
                }
                else
                {
                    Console.WriteLine("Product type not found.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
    public static void DeleteSupplier(int supplierId)
    {
        try
        {
            using (SqlConnection connection = new SqlConnection(Program.connectionString))
            {
                // Открытие подключения
                connection.Open();

                // SQL-запрос для удаления поставщика по его ID
                string sqlQuery = "DELETE FROM Supplier WHERE Id = @SupplierId";
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                command.Parameters.AddWithValue("@SupplierId", supplierId);

                // Выполнение запроса
                int rowsAffected = command.ExecuteNonQuery();

                // Проверка количества удаленных строк
                if (rowsAffected > 0)
                {
                    Console.WriteLine("Supplier deleted successfully.");
                }
                else
                {
                    Console.WriteLine("Supplier not found or could not be deleted.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
    public static void DeleteProduct(int productId)
    {
        try
        {
            using (SqlConnection connection = new SqlConnection(Program.connectionString))
            {
                // Открытие подключения
                connection.Open();

                // SQL-запрос для удаления продукта по его ID
                string sqlQuery = "DELETE FROM Product WHERE Id = @ProductId";
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                command.Parameters.AddWithValue("@ProductId", productId);

                // Выполнение запроса
                int rowsAffected = command.ExecuteNonQuery();

                // Проверка количества удаленных строк
                if (rowsAffected > 0)
                {
                    Console.WriteLine("Product deleted successfully.");
                }
                else
                {
                    Console.WriteLine("Product not found or could not be deleted.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
    public static void UpdateCategoryName(int typeId, string newCategoryName)
    {
        try
        {
            // Создание подключения к базе данных
            using (SqlConnection connection = new SqlConnection(Program.connectionString))
            {
                try
                {
                    // Открытие подключения
                    connection.Open();

                    string sqlQuery = $"UPDATE TypeOfProduct SET [Type] = @NewCategoryName WHERE Id = @TypeId";
                    SqlCommand command = new SqlCommand(sqlQuery, connection);
                    command.Parameters.AddWithValue("@NewCategoryName", newCategoryName);
                    command.Parameters.AddWithValue("@TypeId", typeId);

                    int rowsAffected = command.ExecuteNonQuery();
                    Console.WriteLine($"{rowsAffected} rows updated.");

                    command.Dispose();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
    public static void UpdateSupplierName(int supplierId, string newSupplierName)
    {
        // Создание подключения к базе данных
        using (SqlConnection connection = new SqlConnection(Program.connectionString))
        {
            try
            {
                // Открытие подключения
                connection.Open();

                // Выполнение SQL-запроса для обновления имени поставщика
                string sqlQuery = $"UPDATE Supplier SET [Name] = @NewName WHERE [Id] = @SupplierId";
                SqlCommand command = new SqlCommand(sqlQuery, connection);

                // Передача параметров в запрос
                command.Parameters.AddWithValue("@NewName", newSupplierName);
                command.Parameters.AddWithValue("@SupplierId", supplierId);

                // Выполнение запроса
                int rowsAffected = command.ExecuteNonQuery();
                Console.WriteLine($"Supplier name updated successfully. {rowsAffected} rows affected.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating supplier name: {ex.Message}");
            }
        }
    }
    public static void ChangeProductName(int productId, string newName)
    {
        try
        {
            using (SqlConnection connection = new SqlConnection(Program.connectionString))
            {
                connection.Open();
                string sqlQuery = $"UPDATE Product SET Name = '{newName}' WHERE Id = {productId}";
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                    Console.WriteLine("Product name updated successfully.");
                else
                    Console.WriteLine("Failed to update product name.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
    public static void ChangeProductType(int productId, int newTypeId)
    {
        try
        {
            using (SqlConnection connection = new SqlConnection(Program.connectionString))
            {
                connection.Open();
                string sqlQuery = $"UPDATE Product SET Type_ID = {newTypeId} WHERE Id = {productId}";
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                    Console.WriteLine("Product type updated successfully.");
                else
                    Console.WriteLine("Failed to update product type.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
    public static void ChangeProductSupplier(int productId, int newSupplierId)
    {
        try
        {
            using (SqlConnection connection = new SqlConnection(Program.connectionString))
            {
                connection.Open();
                string sqlQuery = $"UPDATE Product SET Supplier_ID = {newSupplierId} WHERE Id = {productId}";
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                    Console.WriteLine("Product supplier updated successfully.");
                else
                    Console.WriteLine("Failed to update product supplier.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
    public static void ChangeProductCount(int productId, int newCount)
    {
        try
        {
            using (SqlConnection connection = new SqlConnection(Program.connectionString))
            {
                connection.Open();
                string sqlQuery = $"UPDATE Product SET [Count] = {newCount} WHERE Id = {productId}";
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                    Console.WriteLine("Product count updated successfully.");
                else
                    Console.WriteLine("Failed to update product count.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
    public static void ChangeProductPrice(int productId, decimal newPrice)
    {
        try
        {
            using (SqlConnection connection = new SqlConnection(Program.connectionString))
            {
                connection.Open();
                string sqlQuery = $"UPDATE Product SET Cost_Price = {newPrice} WHERE Id = {productId}";
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                    Console.WriteLine("Product price updated successfully.");
                else
                    Console.WriteLine("Failed to update product price.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    public static void InsertInToProducts(string ProductName, int ProductTypeId, int ProductSupplierId, double ProductPrice, int ProductCount = 0)
    {
        try
        {
            // Создание подключения к базе данных
            using (SqlConnection connection = new SqlConnection(Program.connectionString))
            {
                try
                {
                    // Открытие подключения
                    connection.Open();

                    string sqlQuery = "INSERT INTO Product ([Name], [Type_ID], [Supplier_ID], [Count], [Cost_Price], [Date_Delivery])"+
                                        $"VALUES ({ProductName}, {ProductTypeId}, {ProductSupplierId}, {ProductCount}, {ProductPrice}, \'{DateTime.Now.ToString("yyyy-MM-dd")}\')";
                    SqlCommand command = new SqlCommand(sqlQuery, connection);

                    int res = command.ExecuteNonQuery();
                    command.Dispose();
                    Console.WriteLine($"\t{res} records inserted");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }
        catch(Exception ex) { Console.WriteLine(ex.Message); }
    }
    public static void InsertInToSuppliers(List<string> Suppliers)
    {
        try
        {
            // Создание подключения к базе данных
            using (SqlConnection connection = new SqlConnection(Program.connectionString))
            {
                try
                {
                    // Открытие подключения
                    connection.Open();

                    // Преобразуем каждый элемент списка в строку с одинарными кавычками и разделяем их запятыми
                    string values = string.Join(",", Suppliers.Select(supplier => $"('{supplier}')"));

                    string sqlQuery = $"INSERT INTO Supplier ([Name]) VALUES {values}";
                    SqlCommand command = new SqlCommand(sqlQuery, connection);

                    int res = command.ExecuteNonQuery();
                    Console.WriteLine($"\t{res} records inserted");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
    public static void InsertInToTypes(List<string> Types)
    {
        try
        {
            // Создание подключения к базе данных
            using (SqlConnection connection = new SqlConnection(Program.connectionString))
            {
                try
                {
                    // Открытие подключения
                    connection.Open();

                    // Преобразуем каждый элемент списка в строку с одинарными кавычками и разделяем их запятыми
                    string values = string.Join(",", Types.Select(type => $"('{type}')"));

                    string sqlQuery = $"INSERT INTO TypeOfProduct ([Type]) VALUES {values}";
                    SqlCommand command = new SqlCommand(sqlQuery, connection);

                    int res = command.ExecuteNonQuery();
                    Console.WriteLine($"\t{res} records inserted");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
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
    public static List<Product> GetAllInfoAboutProduct(string Supplier = null, string ProductType = null)
    {
        // Создание подключения к базе данных
        using (SqlConnection connection = new SqlConnection(Program.connectionString))
        {
            List<Product> products = new();
            try
            {
                // Открытие подключения
                connection.Open();

                // Выполнение SQL-запроса или вызов хранимой процедуры
                string sqlQuery = @"SELECT 
                                    P.Id,  
                                    P.Name AS ProductName, 
                                    TP.Type AS ProductType,
                                    TP.Id AS TypeId,
                                    S.Name AS SupplierName,
                                    S.Id AS SupplierId,
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
                    products.Add(new Product
                    {
                        ID = (int)reader["Id"],
                        Name = (string)reader["ProductName"],
                        SupplierID = (int)reader["SupplierId"],
                        SupplierName = (string)reader["SupplierName"],
                        TypeID = (int)reader["TypeId"],
                        TypeName = (string)reader["ProductType"],
                        Count = (int)reader["Count"],
                        CostPrice = (decimal)reader["Cost_Price"],
                        Date = (DateTime)reader["Date_Delivery"]
                    });
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            return products;
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
    public static Dictionary<int,string> GetAllTypesWithId()
    {
        Dictionary<int, string> result = new();
        // Создание подключения к базе данных
        using (SqlConnection connection = new SqlConnection(Program.connectionString))
        {
            try
            {
                // Открытие подключения
                connection.Open();

                string sqlQuery = "select * from TypeOfProduct";
                SqlCommand command = new SqlCommand(sqlQuery, connection);

                // Выполнение запроса и обработка результатов
                SqlDataReader reader = command.ExecuteReader();
                int id;
                while (reader.Read())
                {
                    int.TryParse(reader["Id"].ToString(), out id);
                    result.Add(id, reader["Type"].ToString());
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

    public static Dictionary<int, string> GetAllSuppliersWithId()
    {
        Dictionary<int, string> result = new();
        // Создание подключения к базе данных
        using (SqlConnection connection = new SqlConnection(Program.connectionString))
        {
            try
            {
                // Открытие подключения
                connection.Open();

                string sqlQuery = "select * from Supplier";
                SqlCommand command = new SqlCommand(sqlQuery, connection);

                // Выполнение запроса и обработка результатов
                SqlDataReader reader = command.ExecuteReader();
                int id;
                while (reader.Read())
                {
                    int.TryParse(reader["Id"].ToString(), out id);
                    result.Add(id, reader["Name"].ToString());
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