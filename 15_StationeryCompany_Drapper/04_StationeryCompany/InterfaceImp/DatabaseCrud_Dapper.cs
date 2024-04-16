using _04_StationeryCompany.DataBaseResponse;
using _04_StationeryCompany.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data.SqlClient;
using System.Data;

namespace _04_StationeryCompany.InterfaceImp;

class DatabaseCrud_Dapper : IDataBaseCrud
{
    public string ConnectionString { get; set; }
    public DatabaseCrud_Dapper(string connectionString) : base()
    {
        ConnectionString = connectionString;
    }

    private async Task<List<M_TypeOfItem>> GetTypes(string ProcedureName, DynamicParameters parameters = null)
    {
        List<M_TypeOfItem> types;
        using (var connection = new SqlConnection(ConnectionString))
        {
            await connection.OpenAsync();
            types = (await connection.QueryAsync<M_TypeOfItem>(ProcedureName, parameters, commandType: CommandType.StoredProcedure)).ToList();
        }
        return types;
    }
    public async Task<List<M_TypeOfItem>> GetAllItemTypesWithID()
    {
        return await GetTypes("ShowAllItemTypesWithID");
    }
    public async Task<List<M_TypeOfItem>> GetMostProfitableItemType(int TopCount = 15)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@NumberOfItemsToShow", TopCount, DbType.Int32);
        return await GetTypes("GetMostProfitableItemType", parameters);
    }
    public async Task<List<M_TypeOfItem>> GetItemTypeWithMostSales(int TopCount = 15)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@NumberOfItemsToShow", TopCount, DbType.Int32);
        return await GetTypes("GetItemTypeWithMostSales", parameters);
    }


    private async Task<List<M_Item>> GetItems(string ProcedureName, DynamicParameters parameters = null)
    {
        List<M_Item> items;
        using (var connection = new SqlConnection(ConnectionString))
        {
            await connection.OpenAsync();
            items = (await connection.QueryAsync<M_Item>(ProcedureName, parameters, commandType: CommandType.StoredProcedure)).ToList();
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
        var parameters = new DynamicParameters();
        parameters.Add("@NumberOfDays", NumberOfDays, DbType.Int32);
        return await GetItems("GetItemsNotSoldForDays", parameters);
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
        var parameters = new DynamicParameters();
        parameters.Add("@TopItemsCount", TopItemsCount, DbType.Int32);
        return await GetItems("GetMostPopularItems", parameters);
    }


    private async Task<List<M_Company>> GetCompanies(string ProcedureName, DynamicParameters parameters = null)
    {
        List<M_Company> companies;
        using (var connection = new SqlConnection(ConnectionString))
        {
            await connection.OpenAsync();
            companies = (await connection.QueryAsync<M_Company>(ProcedureName, parameters, commandType: CommandType.StoredProcedure)).ToList();
        }
        return companies;
    }
    public async Task<List<M_Company>> GetCompanyInfo()
    {
        return await GetCompanies("GetCompanyInfo");
    }
    public async Task<List<M_Company>> GetTopCompaniesByPurchaseAmount(int TopCompaniesCount = 15)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@TopCompaniesCount", TopCompaniesCount, DbType.Int32);
        return await GetCompanies("GetTopCompaniesByPurchaseAmount", parameters);
    }


    private async Task<List<M_Manager>> GetManagers(string ProcedureName, DynamicParameters parameters = null)
    {
        List<M_Manager> managers;
        using (var connection = new SqlConnection(ConnectionString))
        {
            await connection.OpenAsync();
            managers = (await connection.QueryAsync<M_Manager>(ProcedureName, parameters, commandType: CommandType.StoredProcedure)).ToList();
        }
        return managers;
    }
    public async Task<List<M_Manager>> GetManagers()
    {
        return await GetManagers("ShowAllManagersWithID");
    }
    public async Task<List<M_Manager>> GetTopManagersByProfit(int TopManagersCount = 15)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@TopManagersCount", TopManagersCount, DbType.Int32);
        return await GetManagers("GetTopManagersByProfit", parameters);
    }
    public async Task<List<M_Manager>> GetTopManagersByProfit(DateTime StartDate, DateTime EndDate, int TopManagersCount = 15)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@StartDate", StartDate, DbType.Date);
        parameters.Add("@EndDate", EndDate, DbType.Date);
        parameters.Add("@TopManagersCount", TopManagersCount, DbType.Int32);
        return await GetManagers("GetManagerWithHighestProfitInRange", parameters);
    }
    public async Task<List<M_Manager>> GetTopManagersBySales(int TopManagersCount = 15)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@TopManagersCount", TopManagersCount, DbType.Int32);
        return await GetManagers("GetTopManagersBySales", parameters);
    }



    private async Task<List<M_HistoryOfSells>> GetHistorySells(string ProcedureName, DynamicParameters parameters = null)
    {
        List<M_HistoryOfSells> historySells;
        using (var connection = new SqlConnection(ConnectionString))
        {
            await connection.OpenAsync();
            var reader = await connection.ExecuteReaderAsync(ProcedureName, parameters, commandType: CommandType.StoredProcedure);

            historySells = new List<M_HistoryOfSells>();
            while (await reader.ReadAsync())
            {
                M_HistoryOfSells historySell = new M_HistoryOfSells
                {
                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                    ItemId = reader.IsDBNull(reader.GetOrdinal("ItemId")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("ItemId")),
                    ManagerID = reader.IsDBNull(reader.GetOrdinal("ManagerID")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("ManagerID")),
                    BuyersCompany = reader.IsDBNull(reader.GetOrdinal("BuyersCompany")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("BuyersCompany")),
                    Count = reader.GetInt32(reader.GetOrdinal("Count")),
                    Date = reader.GetDateTime(reader.GetOrdinal("Date")),
                    // Проверяем наличие столбца AdditionInformation перед его чтением
                    AdditionInformation = reader.GetSchemaTable().Columns.Contains("AdditionInformation") ? reader.GetFloat(reader.GetOrdinal("AdditionInformation")) : 0
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
        var parameters = new DynamicParameters();
        parameters.Add("@TopCount", TopCount, DbType.Int32);
        return await GetHistorySells("GetRecentSaleInformation", parameters);
    }
    public async Task<List<M_HistoryOfSells>> GetItemsSoldByManagerId(int ManagerID)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@ManagerId", ManagerID, DbType.Int32);
        return await GetHistorySells("ShowItemsSoldByManagerId", parameters);
    }
    public async Task<List<M_HistoryOfSells>> GetItemsBoughtByCompanyId(int CompanyID)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@CompanyId", CompanyID, DbType.Int32);
        return await GetHistorySells("ShowItemsBoughtByCompanyId", parameters);
    }


    private async Task UpdateItem(string procedureName, DynamicParameters parameters = null)
    {
        try
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                await connection.OpenAsync();
                await connection.ExecuteAsync(procedureName, parameters, commandType: CommandType.StoredProcedure);
            }
        }
        catch { throw; }
    }
    public async Task UpdateItemById(int ItemId, M_Item item)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@ItemId", ItemId, DbType.Int32);
        parameters.Add("@ItemName", item.Name, DbType.String);
        parameters.Add("@TypeId", item.Type_ID, DbType.Int32);
        parameters.Add("@Count", item.Count, DbType.Int32);
        parameters.Add("@CostPrice", item.CostPrice, DbType.Decimal);
        parameters.Add("@Price", item.Price, DbType.Decimal);

        await UpdateItem("UpdateItemInformation", parameters);
    }
    public async Task InsertItem(M_Item item)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@ItemName", item.Name, DbType.String);
        parameters.Add("@TypeId", item.Type_ID, DbType.Int32);
        parameters.Add("@Count", item.Count, DbType.Int32);
        parameters.Add("@CostPrice", item.CostPrice, DbType.Decimal);
        parameters.Add("@Price", item.Price, DbType.Decimal);

        await UpdateItem("InsertNewItem", parameters);
    }


    private async Task DeleteEntityById(string procedureName, DynamicParameters parameters = null)
    {
        using (SqlConnection connection = new SqlConnection(ConnectionString))
        {
            await connection.OpenAsync();
            await connection.ExecuteAsync(procedureName, parameters, commandType: CommandType.StoredProcedure);
        }
    }
    public async Task DeleteItemById(int itemId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@ItemId", itemId, DbType.Int32);
        await DeleteEntityById("DeleteItemById", parameters);
    }
    public async Task DeleteManagerById(int managerId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@ManagerId", managerId, DbType.Int32);
        await DeleteEntityById("DeleteManagerById", parameters);
    }
    public async Task DeleteItemTypeById(int typeId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@TypeId", typeId, DbType.Int32);
        await DeleteEntityById("DeleteItemTypeById", parameters);
    }
    public async Task DeleteCompanyById(int companyId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@CompanyId", companyId, DbType.Int32);
        await DeleteEntityById("DeleteCompanyById", parameters);
    }
}
