﻿using BookStore_DbContext.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore_DbContext.Interface;

public interface IDataBaseAPI
{
    BookStore_DbContext Context { get; }
    Task<bool> Add<T>(T entity) where T : DbEntity;
    Task<bool> Delete<T>(int id) where T : DbEntity;
    Task<bool> Delete<T>(T entity) where T : DbEntity;

    Task<bool> Update<T>(int id, T entity) where T : DbEntity, ICloneable;
    Task<bool> Update<T>(T oldEntity, T newEntity) where T : DbEntity, ICloneable;

    Task<List<T>> Get<T>() where T : DbEntity;
    Task<List<T>> Get<T>(int page = 1, int entitiesPerPage = 10) where T : DbEntity;
    Task<List<TEntity>> Get<TEntity, TValue>(string propertyName, TValue propertyValue) where TEntity : DbEntity;
    Task<List<TEntity>> Get<TEntity>(Dictionary<string, (object value, bool isExactMatch)> propertyFilters) where TEntity : DbEntity;
}
