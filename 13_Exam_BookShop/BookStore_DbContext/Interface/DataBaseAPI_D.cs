using BookStore_DbContext.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BookStore_DbContext.Interface;

public class DataBaseAPI_D : IDataBaseAPI
{
    public BookStore_DbContext Context { get; private set; }
    
    public DataBaseAPI_D() 
    {
        Context = new BookStore_DbContext();
    }
    public DataBaseAPI_D(BookStore_DbContext context)
    {
        if (context == null) throw new ArgumentNullException("context");
        Context = context;
    }
    public async Task<bool> Delete<T>(int id) where T : DbEntity
    {
        T? entity = await Context.Set<T>().FirstOrDefaultAsync((e) => e.Id == id);
        if (entity == null) return false;
        Context.Set<T>().Remove(entity);
        await Context.SaveChangesAsync();
        return true;
    }
    public async Task<bool> Delete<T>(T entity) where T : DbEntity
    {
        return await Delete<T>(entity.Id);
    }
    public async Task<bool> Add<T>(T entity) where T : DbEntity
    {
        entity.Id = default(int);
        await Context.Set<T>().AddAsync(entity);
        await Context.SaveChangesAsync();
        return true;
    }
    public async Task<bool> Update<T>(int id, T entity) where T : DbEntity, ICloneable
    {
        try
        {
            T? _entity = await Context.Set<T>().FindAsync(id);
            if (_entity == null) return false;
            entity.Id = _entity.Id;
            Context.Entry(_entity).CurrentValues.SetValues(entity);
            await Context.SaveChangesAsync();
            return true;
        }
        catch (Exception)
        {
            //Console.WriteLine($"An error occurred while updating entity: {ex.Message}");
            return false;
        }
    }
    public async Task<bool> Update<T>(T oldEntity, T newEntity) where T : DbEntity, ICloneable
    {
        return await Update(oldEntity.Id, newEntity);
    }
    public Task<List<T>> Get<T>() where T : DbEntity
    {
        return Task.FromResult<List<T>>([.. Context.Set<T>()]);
    }
    public Task<List<T>> Get<T>(int page, int entitiesPerPage = 10) where T : DbEntity
    {
        if (page <= 0)
            throw new ArgumentException("Page number must be greater than zero.", nameof(page));

        if (entitiesPerPage <= 0)
            throw new ArgumentException("Entities per page must be greater than zero.", nameof(entitiesPerPage));

        return Task.FromResult<List<T>>([.. Context.Set<T>().Skip((page - 1) * entitiesPerPage).Take(entitiesPerPage)]);
    }
    public Task<List<TEntity>> Get<TEntity, TValue>(string propertyName, TValue propertyValue) where TEntity : DbEntity
    {
        // Получаем набор сущностей типа TEntity из контекста базы данных
        DbSet<TEntity> entities = Context.Set<TEntity>();

        // Получаем информацию о типе TEntity
        Type entityType = typeof(TEntity);

        // Проверяем, существует ли свойство с заданным именем в типе TEntity
        PropertyInfo? property = entityType.GetProperty(propertyName);

        if (property == null)
        {
            throw new ArgumentException($"Property '{propertyName}' does not exist in entity '{entityType.Name}'.", nameof(propertyName));
        }

        // Фильтруем сущности по заданному свойству и его значению
        IQueryable<TEntity> filteredEntities = entities.Where(e => EF.Property<TValue>(e, propertyName)!.Equals(propertyValue));

        // Выполняем запрос к базе данных и получаем список отфильтрованных сущностей и
        // возвращаем список отфильтрованных сущностей
        return Task.FromResult<List<TEntity>>([.. filteredEntities]);
    }
    public Task<List<TEntity>> Get<TEntity>(Dictionary<string, (object value, bool isExactMatch)> propertyFilters) where TEntity : DbEntity
    {
        // Получаем набор сущностей типа TEntity из контекста базы данных
        DbSet<TEntity> entities = Context.Set<TEntity>();

        // Фильтруем сущности по заданным свойствам и их значениям
        IQueryable<TEntity> filteredEntities = entities;

        // Получаем информацию о типе TEntity
        Type entityType = typeof(TEntity);

        foreach (var filter in propertyFilters)
        {
            // Проверяем, существует ли свойство с заданным именем в типе TEntity
            PropertyInfo? property = entityType.GetProperty(filter.Key);

            if (property == null)
            {
                throw new ArgumentException($"Property '{filter.Key}' does not exist in entity '{entityType.Name}'.", nameof(propertyFilters));
            }

            // Применяем фильтрацию в зависимости от значения isExactMatch
            if (property.PropertyType == typeof(string) && !filter.Value.isExactMatch)
            {
                string filterValue = (string)filter.Value.value;
                filteredEntities = filteredEntities.Where(e => EF.Property<string>(e, filter.Key).Contains(filterValue));
            }
            else
            {
                filteredEntities = filteredEntities.Where(e => EF.Property<object>(e, filter.Key)!.Equals(filter.Value.value));
            }
        }

        // Выполняем запрос к базе данных и получаем список отфильтрованных сущностей и
        // возвращаем список отфильтрованных сущностей
        return Task.FromResult<List<TEntity>>(filteredEntities.ToList());
    }

}
