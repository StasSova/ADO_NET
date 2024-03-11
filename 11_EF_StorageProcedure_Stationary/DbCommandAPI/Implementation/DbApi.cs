using DbCntx;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbCommandAPI.Implementation
{
    public class DbApi : IDbAPI, IDisposable
    {
        public _04StationeryCompanyContext Context { get; set; }
        public DbApi() 
        {
            Context = new _04StationeryCompanyContext();
        }
        public DbApi(_04StationeryCompanyContext context)
        {
            Context = context;
        }

        public void DeleteCompany(int companyId)
        {
            SqlParameter param = new()
            {
                ParameterName = "@CompanyName",
                SqlDbType = SqlDbType.Int,
                Value = companyId
            };
            Context.Database.ExecuteSqlRaw("DeleteCompanyById @CompanyName", param);
        }
        public void DeleteItem(int itemId)
        {
            SqlParameter param = new()
            {
                ParameterName = "@ItemId",
                SqlDbType = SqlDbType.Int,
                Value = itemId
            };
            Context.Database.ExecuteSqlRaw("DeleteItemById @ItemId", param);
        }
        public void DeleteManager(int managerId)
        {
            SqlParameter param = new()
            {
                ParameterName = "@ManagerId",
                SqlDbType = SqlDbType.Int,
                Value = managerId
            };
            Context.Database.ExecuteSqlRaw("DeleteManagerById @ManagerId", param);
        }
        public void DeleteType(int typeId)
        {
            SqlParameter param = new()
            {
                ParameterName = "@TypeId",
                SqlDbType = SqlDbType.Int,
                Value = typeId
            };
            Context.Database.ExecuteSqlRaw("DeleteItemTypeById @TypeId", param);
        }


        public List<Item> GetItems()
        {
            var items = Context.Items.FromSqlRaw("EXEC ShowAllItemsInformationWithID").ToList();
            return items;
        }
        public List<Manager> GetManagers()
        {
            var items = Context.Managers.FromSqlRaw("EXEC ShowAllManagersWithID").ToList();
            return items;
        }
        public List<TypeOfItem> GetTypes()
        {
            var items = Context.TypeOfItems.FromSqlRaw("EXEC ShowAllItemTypesWithID").ToList();
            return items;
        }
        public List<Company> GetCompanys()
        {
            var items = Context.Companies.FromSqlRaw("EXEC ShowAllCompnayWithID").ToList();
            return items;
        }
        public List<HistoryOfSell> GetHistoryOfSells()
        {
            var items = Context.HistoryOfSells.FromSqlRaw("EXEC ShowAllHistoryWithID").ToList();
            return items;
        }


        public void InsertNewCompany(Company newCompany)
        {
            var companyNameParam = new SqlParameter("@CompanyName", newCompany.Name);

            Context.Database.ExecuteSqlRaw("EXEC InsertNewBuyerCompany @CompanyName", companyNameParam);
        }
        public void InsertNewItem(Item newItem)
        {
            var itemNameParam = new SqlParameter("@ItemName", newItem.Name);
            var typeIdParam = new SqlParameter("@TypeId", newItem.TypeId);
            var countParam = new SqlParameter("@Count", newItem.Count);
            var costPriceParam = new SqlParameter("@CostPrice", newItem.CostPrice);
            var priceParam = new SqlParameter("@Price", newItem.Price);

            Context.Database.ExecuteSqlRaw("EXEC InsertNewItem @ItemName, @TypeId, @Count, @CostPrice, @Price",
                itemNameParam, typeIdParam, countParam, costPriceParam, priceParam);
        }
        public void InsertNewManager(Manager newManager)
        {
            var FirstNameParam = new SqlParameter("@FirstName", newManager.Fname);
            var LastNameParam = new SqlParameter("@LastName", newManager.Lname);

            Context.Database.ExecuteSqlRaw("EXEC InsertNewManager @FirstName, @LastName",
                FirstNameParam, LastNameParam);
        }
        public void InsertNewType(TypeOfItem newType)
        {
            var TypeNameParam = new SqlParameter("@TypeName", newType.Type);

            Context.Database.ExecuteSqlRaw("EXEC InsertNewItemType @TypeName", TypeNameParam);
        }


        public void UpdateCompanyInformation(int companyId, Company updatedCompany)
        {
            var companyIdParam = new SqlParameter("@CompanyId", companyId);
            var newCompanyNameParam = new SqlParameter("@NewCompanyName", updatedCompany.Name);

            Context.Database.ExecuteSqlRaw("EXEC UpdateCompanyInformation @CompanyId, @NewCompanyName",
                companyIdParam, newCompanyNameParam);
        }
        public void UpdateItemInformation(int itemId, Item updatedItem)
        {
            var itemIdParam = new SqlParameter("@ItemId", itemId);
            var itemNameParam = new SqlParameter("@ItemName", updatedItem.Name);
            var typeIdParam = new SqlParameter("@TypeId", updatedItem.TypeId);
            var countParam = new SqlParameter("@Count", updatedItem.Count);
            var costPriceParam = new SqlParameter("@CostPrice", updatedItem.CostPrice);
            var priceParam = new SqlParameter("@Price", updatedItem.Price);

            Context.Database.ExecuteSqlRaw("EXEC UpdateItemInformation @ItemId, @ItemName, @TypeId, @Count, @CostPrice, @Price",
                itemIdParam, itemNameParam, typeIdParam, countParam, costPriceParam, priceParam);
        }
        public void UpdateManagerInformation(int managerId, Manager updatedManager)
        {
            var managerIdParam = new SqlParameter("@ManagerId", managerId);
            var newFirstNameParam = new SqlParameter("@NewFirstName", updatedManager.Fname);
            var newLastNameParam = new SqlParameter("@NewLastName", updatedManager.Lname);

            Context.Database.ExecuteSqlRaw("EXEC UpdateManagerInformation @ManagerId, @NewFirstName, @NewLastName",
                managerIdParam, newFirstNameParam, newLastNameParam);
        }
        public void UpdateTypeInformation(int typeId, TypeOfItem updatedType)
        {
            var typeIdParam = new SqlParameter("@TypeId", typeId);
            var newTypeNameParam = new SqlParameter("@NewTypeName", updatedType.Type);

            Context.Database.ExecuteSqlRaw("EXEC UpdateItemTypeInformation @TypeId, @NewTypeName",
                typeIdParam, newTypeNameParam);
        }



        public List<Item> GetItemsWithMaxCostPrice()
        {
            var result = Context.Items.FromSqlRaw("EXEC ShowItemsWithMaxCostPriceWithID")
                                         .ToList();

            return result;
        }
        public List<Item> GetItemsWithMinCostPrice()
        {
            var result = Context.Items.FromSqlRaw("EXEC ShowItemsWithMinCostPriceWithID")
                                                     .ToList();

            return result;
        }
        public List<Item> GetItemsWithMaxNumbers()
        {
            var result = Context.Items.FromSqlRaw("EXEC ShowItemsWithMaxQuantityWithID")
                                                     .ToList();

            return result;
        }
        public List<Item> GetItemsWithMinNumbers()
        {
            var result = Context.Items.FromSqlRaw("EXEC ShowItemsWithMinQuantityWithID")
                                                     .ToList();

            return result;
        }



        public Dictionary<string, int?> GetAverageItemCountPerType()
        {
            var result = new Dictionary<string, int?>();

            // Выполнение хранимой процедуры через FromSqlRaw
            var queryResult = Context.TypeOfItems
                .FromSqlRaw("EXEC ShowAverageItemCountPerType")
                .ToList();

            // Преобразование результатов запроса в словарь
            foreach (var item in queryResult)
            {
                result.Add(item.Type, item.Count);
            }

            return result;
        }
        public List<HistoryOfSell> GetRecentSalesInformation(int topCount = 5)
        {
            var result = new List<HistoryOfSell>();

            // Создание параметра для количества последних продаж
            var topCountParam = new SqlParameter("@TopCount", topCount);

            // Выполнение хранимой процедуры с использованием параметра
            result = Context.HistoryOfSells
                .FromSqlRaw("EXEC GetRecentSaleInformation @TopCount", topCountParam)
                .ToList();

            return result;
        }


        public List<Item> GetItemsBoughtByCompany(int companyId)
        {
            var result = new List<Item>();

            var companyIdParam = new SqlParameter("@CompanyId", companyId);

            result = Context.Items
                .FromSqlRaw("EXEC ShowItemsBoughtByCompanyId @CompanyId", companyIdParam)
                .ToList();

            return result;
        }
        public List<Item> GetItemsByType(int typeId)
        {
            var result = new List<Item>();

            var TypeIdParam = new SqlParameter("@TypeId", typeId);

            result = Context.Items
                .FromSqlRaw("EXEC ShowItemsInformationByType @TypeId", TypeIdParam)
                .ToList();

            return result;
        }
        public List<Item> GetItemsSoldByManager(int managerId)
        {
            var result = new List<Item>();

            var ManagerIdParam = new SqlParameter("@ManagerId", managerId);

            result = Context.Items
                .FromSqlRaw("EXEC ShowItemsSoldByManagerId @ManagerId", ManagerIdParam)
                .ToList();

            return result;
        }


        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
