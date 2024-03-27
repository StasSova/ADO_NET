using _04_StationeryCompany.Interface;
using _04_StationeryCompany.MVVM;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _04_StationeryCompany.DataBaseResponse;
using System.ComponentModel.Design;

namespace _04_StationeryCompany.InterfaceImp
{
    public class DataBaseCrudDefault : IDataBaseCrud
    {
        private DataBaseCrudDefault() { }
        public DataBaseCrudDefault(string connectionString) : base()
        {
            ConnectionString = connectionString;
        }
        public string ConnectionString { get; set; }

        private async Task<List<M_TypeOfItem>> GetTypes(string ProcedureName, Dictionary<string, (object value, SqlDbType type)> parameters = null)
        {
            List<M_TypeOfItem> types = new List<M_TypeOfItem>();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand(ProcedureName, connection);
                command.CommandType = CommandType.StoredProcedure;

                // Добавляем параметры, если они переданы
                if (parameters != null)
                {
                    foreach (var parameter in parameters)
                    {
                        command.Parameters.Add(parameter.Key, parameter.Value.type).Value = parameter.Value.value;
                    }
                }

                await connection.OpenAsync();
                SqlDataReader reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    M_TypeOfItem type = new M_TypeOfItem
                    {
                        Id = reader.GetInt32("Id"),
                        Type = reader.GetString("Type"),
                        Count = reader.GetInt32("Count")
                    };

                    types.Add(type);
                }
            }

            return types;
        }
        public async Task<List<M_TypeOfItem>> GetAllItemTypesWithID()
        {
            return await GetTypes("ShowAllItemTypesWithID");
        }
        public async Task<List<M_TypeOfItem>> GetMostProfitableItemType(int TopCount = 15)
        {
            return await GetTypes("GetMostProfitableItemType", new Dictionary<string, (object value, SqlDbType type)>
                            {
                                { "@NumberOfItemsToShow", (TopCount, SqlDbType.Int) }
                            });
        }
        public async Task<List<M_TypeOfItem>> GetItemTypeWithMostSales(int TopCount = 15)
        {
            return await GetTypes("GetItemTypeWithMostSales", new Dictionary<string, (object value, SqlDbType type)>
                            {
                                { "@NumberOfItemsToShow", (TopCount, SqlDbType.Int) }
                            });
        }




        private async Task<List<M_Item>> GetItems(string ProcedureName, Dictionary<string, (object value, SqlDbType type)> parameters = null)
        {
            List<M_Item> items = new();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand(ProcedureName, connection);
                command.CommandType = CommandType.StoredProcedure;

                // Добавляем параметры, если они были предоставлены
                if (parameters != null)
                {
                    foreach (var parameter in parameters)
                    {
                        command.Parameters.Add(parameter.Key, parameter.Value.type).Value = parameter.Value.value;
                    }
                }

                await connection.OpenAsync();
                SqlDataReader reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    M_Item item = new M_Item
                    {
                        Id = reader.GetInt32("Id"),
                        Name = reader.GetString("Name"),
                        Type_ID = reader.GetInt32("Type_ID"),
                        Count = reader.GetInt32("Count"),
                        CostPrice = reader.GetDecimal("CostPrice"),
                        Price = reader.GetDecimal("Price")
                    };

                    items.Add(item);
                }
            }

            return items;
        }
        public async Task<List<M_Item>> GetAllItemsInformationWithID()
        {
            return await GetItems("ShowAllItemsInformationWithID");
        }
        public async Task<List<M_Item>> GetItemsWithMaxQuantityWithID()
        {
            return await GetItems("ShowItemsWithMaxQuantityWithID");
        }
        public async Task<List<M_Item>> GetItemsWithMinQuantityWithID()
        {
            return await GetItems("ShowItemsWithMinQuantityWithID");
        }

 
        public async Task<List<M_Item>> GetItemsNotSoldForDays(int NumberOfDays = 15)
        {
            return await GetItems("GetItemsNotSoldForDays", new Dictionary<string, (object value, SqlDbType type)>
                            {
                                { "@NumberOfDays", (NumberOfDays, SqlDbType.Int) }
                            });
        }
        public async Task<List<M_Item>> GetItemsWithMaxCostPriceWithID()
        {
            return await GetItems("ShowItemsWithMaxCostPriceWithID");
        }
        public async Task<List<M_Item>> GetItemsWithMinCostPriceWithID()
        {
            return await GetItems("ShowItemsWithMinCostPriceWithID");
        }
        public async Task<List<M_Item>> GetMostPopularItems(int TopItemsCount = 15)
        {
            return await GetItems("GetMostPopularItems", new Dictionary<string, (object value, SqlDbType type)>
                            {
                                { "@TopItemsCount", (TopItemsCount, SqlDbType.Int) }
                            });
        }

        private async Task<List<M_Company>> GetCompanies(string ProcedureName, Dictionary<string, (object value, SqlDbType type)> parameters = null)
        {
            List<M_Company> companies = new List<M_Company>();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand(ProcedureName, connection);
                command.CommandType = CommandType.StoredProcedure;

                // Добавляем параметры, если они переданы
                if (parameters != null)
                {
                    foreach (var parameter in parameters)
                    {
                        command.Parameters.Add(parameter.Key, parameter.Value.type).Value = parameter.Value.value;
                    }
                }

                await connection.OpenAsync();
                SqlDataReader reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    M_Company company = new M_Company
                    {
                        Id = reader.GetInt32("Id"),
                        Name = reader.GetString("Name"),
                    };

                    companies.Add(company);
                }
            }

            return companies;
        }
        public async Task<List<M_Company>> GetCompanyInfo()
        {
            return await GetCompanies("GetCompanyInfo");
        }
        public async Task<List<M_Company>> GetTopCompaniesByPurchaseAmount(int TopCompaniesCount = 15)
        {
            return await GetCompanies("GetMostPopularItems", new Dictionary<string, (object value, SqlDbType type)>
                            {
                                { "@TopCompaniesCount", (TopCompaniesCount, SqlDbType.Int) }
                            });
        }


        private async Task<List<M_Manager>> GetManagers(string ProcedureName, Dictionary<string, (object value, SqlDbType type)> parameters = null)
        {
            List<M_Manager> managers = new List<M_Manager>();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand(ProcedureName, connection);
                command.CommandType = CommandType.StoredProcedure;

                if (parameters != null)
                {
                    foreach (var parameter in parameters)
                    {
                        command.Parameters.Add(parameter.Key, parameter.Value.type).Value = parameter.Value.value;
                    }
                }

                await connection.OpenAsync();
                SqlDataReader reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    M_Manager manager = new M_Manager
                    {
                        Id = reader.GetInt32("Id"),
                        FName = reader.GetString("FName"),
                        LName = reader.GetString("LName"),
                    };

                    managers.Add(manager);
                }
            }

            return managers;
        }
        public async Task<List<M_Manager>> GetManagers()
        {
            return await GetManagers("ShowAllManagersWithID");
        }
        public async Task<List<M_Manager>> GetTopManagersByProfit(int TopManagersCount = 15)
        {
            return await GetManagers("GetTopManagersByProfit", new Dictionary<string, (object value, SqlDbType type)>
                            {
                                { "@TopCompaniesCount", (TopManagersCount, SqlDbType.Int) }
                            });
        }
        public async Task<List<M_Manager>> GetTopManagersByProfit(DateTime StartDate, DateTime EndDate, int TopManagersCount = 15)
        {
            return await GetManagers("GetManagerWithHighestProfitInRange", new Dictionary<string, (object value, SqlDbType type)>
                            {
                                { "@StartDate", (StartDate, SqlDbType.Date) },
                                { "@EndDate", (EndDate, SqlDbType.Date) },
                                { "@TopManagersCount", (TopManagersCount, SqlDbType.Int) }
                            });
        }
        public async Task<List<M_Manager>> GetTopManagersBySales(int TopManagersCount = 15)
        {
            return await GetManagers("GetTopManagersBySales", new Dictionary<string, (object value, SqlDbType type)>
                            {
                                { "@TopCompaniesCount", (TopManagersCount, SqlDbType.Int) }
                            });
        }


        private async Task<List<M_HistoryOfSells>> GetHistorySells(string ProcedureName, Dictionary<string, (object value, SqlDbType type)> parameters = null)
        {
            List<M_HistoryOfSells> historySells = new List<M_HistoryOfSells>();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand(ProcedureName, connection);
                command.CommandType = CommandType.StoredProcedure;

                // Добавляем параметры, если они переданы
                if (parameters != null)
                {
                    foreach (var parameter in parameters)
                    {
                        command.Parameters.Add(parameter.Key, parameter.Value.type).Value = parameter.Value.value;
                    }
                }

                await connection.OpenAsync();
                SqlDataReader reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    M_HistoryOfSells historySell = new M_HistoryOfSells
                    {
                        Id = reader.GetInt32("Id"),
                        ItemId = reader.IsDBNull("ItemId") ? (int?)null : reader.GetInt32("ItemId"),
                        ManagerID = reader.IsDBNull("ManagerID") ? (int?)null : reader.GetInt32("ManagerID"),
                        BuyersCompany = reader.IsDBNull("BuyersCompany") ? (int?)null : reader.GetInt32("BuyersCompany"),
                        Count = reader.GetInt32("Count"),
                        Date = reader.GetDateTime("Date"),
                        // Проверяем наличие столбца AdditionInformation перед его чтением
                        AdditionInformation = reader.GetSchemaTable().Columns.Contains("AdditionInformation") ? reader.GetFloat("AdditionInformation") : 0
                    };

                    historySells.Add(historySell);
                }
            }

            return historySells;
        }
        
        public async Task<List<M_HistoryOfSells>> GetAllHystorySells()
        {
            return await GetHistorySells("ShowAllHystoryOfSells");
        }
        public async Task<List<M_HistoryOfSells>> GetRecentSaleInformation(int TopCount = 15)
        {
            return await GetHistorySells("GetRecentSaleInformation", new Dictionary<string, (object value, SqlDbType type)>
                            {
                                { "@TopCount", (TopCount, SqlDbType.Int) }
                            });
        }
        public async Task<List<M_HistoryOfSells>> GetItemsSoldByManagerId(int ManagerID)
        {
            return await GetHistorySells("ShowItemsSoldByManagerId", new Dictionary<string, (object value, SqlDbType type)>
                            {
                                { "@ManagerId", (ManagerID, SqlDbType.Int) }
                            });
        }
        public async Task<List<M_HistoryOfSells>> GetItemsBoughtByCompanyId(int CompanyID)
        {
            return await GetHistorySells("ShowItemsBoughtByCompanyId", new Dictionary<string, (object value, SqlDbType type)>
                            {
                                { "@CompanyId", (CompanyID, SqlDbType.Int) }
                            });
        }


        private async Task DeleteEntityById(string procedureName, Dictionary<string, (object value, SqlDbType type)> parameters = null)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand(procedureName, connection);
                command.CommandType = CommandType.StoredProcedure;

                // Добавляем параметры, если они переданы
                if (parameters != null)
                {
                    foreach (var parameter in parameters)
                    {
                        command.Parameters.Add(parameter.Key, parameter.Value.type).Value = parameter.Value.value;
                    }
                }

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
        }
        public async Task DeleteItemById(int itemId)
        {
            await DeleteEntityById("DeleteItemById", new Dictionary<string, (object value, SqlDbType type)>
                            {
                                { "@ItemId", (itemId, SqlDbType.Int) }
                            });
        }
        public async Task DeleteManagerById(int managerId)
        {
            await DeleteEntityById("DeleteManagerById", new Dictionary<string, (object value, SqlDbType type)>
                            {
                                { "@ManagerId", (managerId, SqlDbType.Int) }
                            });
        }
        public async Task DeleteItemTypeById(int typeId)
        {
            await DeleteEntityById("DeleteItemTypeById", new Dictionary<string, (object value, SqlDbType type)>
                            {
                                { "@TypeId", (typeId, SqlDbType.Int) }
                            });
        }
        public async Task DeleteCompanyById(int companyId)
        {
            await DeleteEntityById("DeleteCompanyById", new Dictionary<string, (object value, SqlDbType type)>
                            {
                                { "@CompanyId", (companyId, SqlDbType.Int) }
                            });
        }


        private async Task UpdateItem(string procedureName, Dictionary<string, (object value, SqlDbType type)> parameters = null)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    SqlCommand command = new SqlCommand(procedureName, connection);
                    command.CommandType = CommandType.StoredProcedure;

                    // Добавляем параметры, если они переданы
                    if (parameters != null)
                    {
                        foreach (var parameter in parameters)
                        {
                            command.Parameters.Add(parameter.Key, parameter.Value.type).Value = parameter.Value.value;
                        }
                    }

                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                }
            }
            catch { throw; }
        }
        public async Task UpdateItemById(int ItemId, M_Item item)
        {
            await UpdateItem("UpdateItemInformation", new Dictionary<string, (object value, SqlDbType type)>
                            {
                                { "@ItemId", (ItemId, SqlDbType.Int) },
                                { "@ItemName", (item.Name, SqlDbType.NVarChar) },
                                { "@TypeId", (item.Type_ID, SqlDbType.Int) },
                                { "@Count", (item.Count, SqlDbType.Int) },
                                { "@CostPrice", (item.CostPrice, SqlDbType.Decimal) },
                                { "@Price", (item.Price, SqlDbType.Decimal) }
                            });
        }
        public async Task InsertItem(M_Item item)
        {
            await UpdateItem("InsertNewItem", new Dictionary<string, (object value, SqlDbType type)>
                            {
                                { "@ItemName", (item.Name, SqlDbType.NVarChar) },
                                { "@TypeId", (item.Type_ID, SqlDbType.Int) },
                                { "@Count", (item.Count, SqlDbType.Int) },
                                { "@CostPrice", (item.CostPrice, SqlDbType.Decimal) },
                                { "@Price", (item.Price, SqlDbType.Decimal) }
                            });
        }
    }
}
