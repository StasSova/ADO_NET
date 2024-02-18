using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _04_StationeryCompany.DataBaseResponse;
using _04_StationeryCompany.MVVM;

namespace _04_StationeryCompany.Interface
{
    public interface IDataBaseCrud
    {
        public string ConnectionString { get; set; }

        /// <summary>
        /// Вернуть канцтовары
        /// </summary>
        /// <returns>Список канцтоваров</returns>
        Task<List<M_Item>> GetAllItemsInformationWithID();

        /// <summary>
        /// Вернуть канцтовары с максимальным количеством единиц:
        /// </summary>
        /// <returns></returns>
        Task<List<M_Item>> GetItemsWithMaxQuantityWithID();

        /// <summary>
        /// Вернуть канцтовары с минимальным количеством единиц:
        /// </summary>
        /// <returns></returns>
        Task<List<M_Item>> GetItemsWithMinQuantityWithID();

        /// <summary>
        /// Вернуть канцтовары с минимальной себестоимостью единицы 
        /// </summary>
        /// <returns></returns>
        Task<List<M_Item>> GetItemsWithMinCostPriceWithID();

        /// <summary>
        /// Вернуть канцтовары с максимальной себестоимостью единицы:
        /// </summary>
        /// <returns></returns>
        Task<List<M_Item>> GetItemsWithMaxCostPriceWithID();

        /// <summary>
        /// Вернуть канцтовары, которые продал конкретный менеджер по продажам
        /// </summary>
        /// <param name="ManagerId"></param>
        /// <returns></returns>

        /// <summary>
        /// Показать канцтовары, которые купила конкретная фирма покупатель;
        /// </summary>
        /// <param name="ManagerId"></param>
        /// <returns></returns>

        /// <summary>
        /// Показать название самых популярных канцтоваров. Популярность высчитываем по количеству проданных единиц;
        /// </summary>
        /// <param name="TopItemsCount">Количество элементов, которые необходимо вернуть</param>
        /// <returns></returns>
        Task<List<M_Item>> GetMostPopularItems(int TopItemsCount = 15);

        /// <summary>
        /// Показать канцтовары, которые не продавались заданное количество дней.
        /// </summary>
        /// <param name="NumberOfDays">Количество дней, которые товар не продавался</param>
        /// <returns></returns>
        Task<List<M_Item>> GetItemsNotSoldForDays(int NumberOfDays = 15);

        /// <summary>
        /// Показать информацию о фирмах покупателях
        /// </summary>
        /// <returns></returns>
        Task<List<M_Company>> GetCompanyInfo();

        /// <summary>
        /// Показать информацию о фирме покупателе, которая купила на самую большую сумму
        /// </summary>
        /// <param name="TopCompaniesCount">Количество компаний, которое возвращает функция</param>
        /// <returns></returns>
        Task<List<M_Company>> GetTopCompaniesByPurchaseAmount(int TopCompaniesCount = 15);


        /// <summary>
        /// Отображение всех менеджеров по продажам
        /// </summary>
        /// <returns></returns>
        Task<List<M_Manager>> GetManagers();

        /// <summary>
        /// Показать информацию о менеджере с наибольшим количеством продаж по количеству единиц
        /// </summary>
        /// <param name="TopManagersCount">Количество менеджеров, которое возвращает функция</param>
        /// <returns></returns>
        Task<List<M_Manager>> GetTopManagersBySales(int TopManagersCount = 15);

        /// <summary>
        /// Показать информацию о менеджере по продажам с наибольшей общей суммой прибыли
        /// </summary>
        /// <param name="TopManagersCount">Количество менеджеров, которое возвращает функция</param>
        /// <returns></returns>
        Task<List<M_Manager>> GetTopManagersByProfit(int TopManagersCount = 15);

        /// <summary>
        /// Показать информацию о менеджере по продажам с наибольшей общей суммой прибыли за указанный промежуток времени
        /// </summary>
        /// <param name="StartDate">Дата, с которой начинается промежуток (включительно)</param>
        /// <param name="EndDate">Дата которой заканчивается промежуток (включительно)</param>
        /// <param name="TopManagersCount">Количество менеджеров, которое возвращает функция</param>
        /// <returns></returns>
        Task<List<M_Manager>> GetTopManagersByProfit(DateTime StartDate, DateTime EndDate, int TopManagersCount = 15);

        /// <summary>
        /// Показать информацию о самой недавней продаже
        /// </summary>
        /// <param name="TopCount">Количество продаж, которое возвращает функция</param>
        /// <returns></returns>
        Task<List<M_HistoryOfSells>> GetRecentSaleInformation(int TopCount = 15);


        Task<List<M_TypeOfItem>> GetAllItemTypesWithID();
        Task<List<M_TypeOfItem>> GetMostProfitableItemType(int TopCount = 15);
        Task<List<M_TypeOfItem>> GetItemTypeWithMostSales(int TopCount = 15);
        Task<List<M_HistoryOfSells>> GetAllHystorySells();
        Task<List<M_HistoryOfSells>> GetItemsSoldByManagerId(int ManagerID);
        Task<List<M_HistoryOfSells>> GetItemsBoughtByCompanyId(int CompanyID);


        Task DeleteItemById(int ItemId);
        Task DeleteManagerById(int ManagerId);
        Task DeleteItemTypeById(int TypeId);
        Task DeleteCompanyById(int CompanyId);

        Task UpdateItemById(int ItemId, M_Item item);
        Task InsertItem(M_Item item);
    }




}
