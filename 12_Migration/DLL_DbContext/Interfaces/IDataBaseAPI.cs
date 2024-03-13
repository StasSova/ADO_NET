using DLL_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL_DbContext.Interfaces;
public interface IDataBaseAPI
{
    GameDbContext Context { get; }
    Task<bool> Add<T>(T entity) where T : DbEntity;
    Task<bool> Delete<T>(int id) where T : DbEntity;
    Task<bool> Delete<T>(T entity) where T : DbEntity;

    Task<bool> Update<T>(int id, T entity) where T : DbEntity, ICloneable;
    Task<bool> Update<T>(T oldEntity, T newEntity) where T : DbEntity, ICloneable;

    Task<ICollection<T>> Get<T>() where T : DbEntity;
    Task<ICollection<T>> Get<T>(int page = 1, int entitiesPerPage = 10) where T : DbEntity;
    Task<ICollection<TEntity>> Get<TEntity, TValue>(string propertyName, TValue propertyValue) where TEntity : DbEntity;
    Task<ICollection<TEntity>> Get<TEntity>(Dictionary<string, (object value, bool isExactMatch)> propertyFilters) where TEntity : DbEntity;
}
