using DbCntx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbCommandAPI
{
    public interface IDbAPI
    {
        _04StationeryCompanyContext Context { get; set; }
        //• Отображение всей информации о канцтоварах;
        List<Item> GetItems();
        //• Отображение всех типов канцтоваров;
        List<TypeOfItem> GetTypes();
        //• Отображение всех менеджеров по продажам;
        List<Manager> GetManagers();
        //• Отображение всех менеджеров по продажам;
        List<Company> GetCompanys();

        //• Показать канцтовары с максимальным количеством единиц;
        List<Item> GetItemsWithMaxNumbers();
        //• Показать канцтовары с минимальным количеством единиц;
        List<Item> GetItemsWithMinNumbers();
        //• Показать канцтовары с минимальной себестоимостью единицы;
        List<Item> GetItemsWithMinCostPrice();
        //• Показать канцтовары с максимальной себестоимостью единицы.
        List<Item> GetItemsWithMaxCostPrice();
        //• Показать канцтовары, заданного типа;
        List<Item> GetItemsByType(int typeId);
        //• Показать канцтовары, которые продал конкретный менеджер по
        //продажам;
        List<Item> GetItemsSoldByManager(int managerId);
        //• Показать канцтовары, которые купила конкретная фирма покупатель;
        List<Item> GetItemsBoughtByCompany(int companyId);
        //• Показать информацию о самой недавней продаже;
        List<HistoryOfSell> GetRecentSalesInformation(int topCount);
        //• Показать среднее количество товаров по каждому типу канцтоваров;
        Dictionary<string, int?> GetAverageItemCountPerType();
        //• Вставка новых канцтоваров;
        void InsertNewItem(Item newItem);
        //• Вставка новых типов канцтоваров;
        void InsertNewType(TypeOfItem newType);
        //• Вставка новых менеджеров по продажам;
        void InsertNewManager(Manager newManager);
        //• Вставка новых фирм покупателей;
        void InsertNewCompany(Company newCompany);
        //• Обновление информации о существующих канцтоварах;
        void UpdateItemInformation(int itemId,Item updatedItem);
        //• Обновление информации о существующих фирмах
        //покупателях;
        void UpdateCompanyInformation(int companyId, Company updatedCompany);
        //• Обновление информации о существующих менеджерах по
        //продажам;
        void UpdateManagerInformation(int managerId, Manager updatedManager);
        //• Обновление информации о существующих типах канцтоваров;
        void UpdateTypeInformation(int typeID, TypeOfItem updatedType);
        //• Удаление канцтоваров;
        void DeleteItem(int itemId);
        //• Удаление менеджеров по продажам;
        void DeleteManager(int managerId);
        //• Удаление типов канцтоваров;
        void DeleteType(int typeId);
        //• Удаление фирм покупателей.
        void DeleteCompany(int companyId);
        List<HistoryOfSell> GetHistoryOfSells();
    }
}
