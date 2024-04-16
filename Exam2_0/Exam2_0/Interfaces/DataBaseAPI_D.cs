using BookContext.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookContext.Interfaces;

public class DataBaseAPI_D : IDataBaseAPI
{
    public BookContext Context { get; private set; }

    public DataBaseAPI_D()
    {
        Context = new BookContext();
    }
    public DataBaseAPI_D(BookContext context)
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
    public async Task<bool> Update<T>(int id, T entity) where T : DbEntity
    {
        try
        {
            T? _entity = await Context.Set<T>().FindAsync(id);
            if (_entity == null) return false;
            entity.Id = _entity.Id;
            Context.Entry(_entity).CurrentValues.SetValues(entity);
            Context.SaveChanges();
            return true;
        }
        catch (Exception)
        {
            //Console.WriteLine($"An error occurred while updating entity: {ex.Message}");
            return false;
        }
    }
    public async Task<bool> Update<T>(T oldEntity, T newEntity) where T : DbEntity
    {
        return await Update(oldEntity.Id, newEntity);
    }
    public Task<List<T>> Get<T>() where T : DbEntity
    {
        DbSet<T> entities = Context.Set<T>();
        return Task.FromResult<List<T>>(entities.ToList());
    }

    public Task<List<T>> Get<T>(
        int page,
        int entitiesPerPage = 10) where T : DbEntity
    {
        if (page <= 0)
            throw new ArgumentException("Page number must be greater than zero.", nameof(page));

        if (entitiesPerPage <= 0)
            throw new ArgumentException("Entities per page must be greater than zero.", nameof(entitiesPerPage));

        DbSet<T> entities = Context.Set<T>();
        return Task.FromResult<List<T>>(entities.Skip((page - 1) * entitiesPerPage).Take(entitiesPerPage).ToList());
    }

    public Task<List<TEntity>> Get<TEntity, TValue>(
        string propertyName,
        TValue propertyValue) where TEntity : DbEntity
    {
        DbSet<TEntity> entities = Context.Set<TEntity>();
        IQueryable<TEntity> filteredEntities = entities.Where(e => EF.Property<TValue>(e, propertyName)!.Equals(propertyValue));
        return Task.FromResult<List<TEntity>>(filteredEntities.ToList());
    }

    public Task<List<TEntity>> Get<TEntity, TValue>(
        string propertyName,
        TValue propertyValue,
        int page,
        int entitiesPerPage = 10) where TEntity : DbEntity
    {
        DbSet<TEntity> entities = Context.Set<TEntity>();

        // Фильтруем сущности по заданному свойству и его значению
        IQueryable<TEntity> filteredEntities = entities.Where(e => EF.Property<TValue>(e, propertyName)!.Equals(propertyValue));

        // Пропускаем страницы и выбираем количество сущностей на странице
        filteredEntities = filteredEntities.Skip((page - 1) * entitiesPerPage).Take(entitiesPerPage);

        return Task.FromResult<List<TEntity>>(filteredEntities.ToList());
    }

    public Task<List<TEntity>> Get<TEntity>(
        Dictionary<string, (object value, bool isExactMatch)> propertyFilters) where TEntity : DbEntity
    {
        DbSet<TEntity> entities = Context.Set<TEntity>();
        IQueryable<TEntity> filteredEntities = entities;

        foreach (var filter in propertyFilters)
        {
            filteredEntities = ApplyFilter(filteredEntities, filter.Key, filter.Value.value, filter.Value.isExactMatch);
        }

        return Task.FromResult<List<TEntity>>(filteredEntities.ToList());
    }

    public Task<List<TEntity>> Get<TEntity>(
        int page,
        Dictionary<string, (object value, bool isExactMatch)> propertyFilters,
        int entitiesPerPage = 10
        )
        where TEntity : DbEntity
    {
        IQueryable<TEntity> entities = Context.Set<TEntity>();

        // Применяем фильтры, если они были предоставлены
        if (propertyFilters != null && propertyFilters.Count > 0)
        {
            foreach (var filter in propertyFilters)
            {
                entities = ApplyFilter(entities, filter.Key, filter.Value.value, filter.Value.isExactMatch);
            }
        }

        // Пропускаем страницы и выбираем количество сущностей на странице
        entities = entities.Skip((page - 1) * entitiesPerPage).Take(entitiesPerPage);

        return Task.FromResult<List<TEntity>>(entities.ToList());
    }

    private IQueryable<TEntity> ApplyFilter<TEntity>(
        IQueryable<TEntity> query,
        string propertyName,
        object value,
        bool isExactMatch) where TEntity : class
    {
        if (typeof(TEntity).GetProperty(propertyName) == null)
        {
            throw new ArgumentException($"Property '{propertyName}' does not exist in entity '{typeof(TEntity).Name}'.", nameof(propertyName));
        }

        if (isExactMatch)
        {
            return query.Where(e => EF.Property<object>(e, propertyName) == value);
        }
        else
        {
            if (typeof(TEntity).GetProperty(propertyName)?.PropertyType == typeof(string))
            {
                return query.Where(e => EF.Property<string>(e, propertyName).ToLower().Contains(((string)value).ToLower()));
            }
            else
            {
                throw new ArgumentException($"Property '{propertyName}' of entity '{typeof(TEntity).Name}' is not a string property and cannot be filtered with contains operation.", nameof(propertyName));
            }
        }
    }


    // Универсальный метод для выполнения хранимых процедур с параметрами
    public async Task<List<TEntity>> ExecuteStoredProcedureAsync<TEntity>(
        string procedureName,
        Dictionary<string, object> parameters) where TEntity : class
    {
        var result = new List<TEntity>();

        try
        {
            // Создание строки представления SQL-запроса
            string sqlQuery = $"EXEC {procedureName}";

            // Формирование строкового представления параметров и создание коллекции SqlParameter
            var sqlParameters = parameters?.Select(p => new SqlParameter($"@{p.Key}", p.Value)).ToList();

            if (sqlParameters != null && sqlParameters.Any())
            {
                // Добавление параметров к SQL-запросу
                string paramList = string.Join(", ", sqlParameters.Select(p => $"{p.ParameterName}"));
                sqlQuery += $" {paramList}";
            }

            // Выполнение SQL-запроса с параметрами
            result = await Context.Set<TEntity>()
                .FromSqlRaw(sqlQuery, sqlParameters?.ToArray())
                .ToListAsync();
        }
        catch (Exception ex)
        {
            // Обработка исключений
            Console.WriteLine($"An error occurred while executing stored procedure: {ex.Message}");
        }

        return result;
    }





    // DELETE AFTER


    // BOOK INFORMATION
    public async Task<M_ShoppingCart> CreateShoppingCart(int userId)
    {
        try
        {
            var user = await Context.Users.FindAsync(userId);

            if (user != null)
            {
                // Создаем новую корзину для пользователя
                var newCart = new M_ShoppingCart
                {
                    UserId = userId,
                    User = user
                };

                // Добавляем корзину в контекст базы данных
                Context.ShoppingCarts.Add(newCart);
                await Context.SaveChangesAsync();

                return newCart;
            }
            else
            {
                // Пользователь не найден
                return null;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while creating user's shopping cart: {ex.Message}");
            return null;
        }
    }
    public async Task AddBookToUserCart(int userId, int bookId)
    {
        try
        {
            // Находим последнюю корзину пользователя
            var userCart = await Context.ShoppingCarts
                .Where(sc => sc.UserId == userId)
                .OrderByDescending(sc => sc.Id)
                .FirstOrDefaultAsync();

            if (userCart != null)
            {
                var book = await Context.Books
                    .Where(b => b.Id == bookId)
                    .OrderByDescending(b => b.Id)
                    .LastOrDefaultAsync();

                // Добавляем Book в корзину пользователя
                userCart.Books.Add(book);

                // Сохраняем изменения в базе данных
                await Context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("User's shopping cart not found.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while adding book to user's cart: {ex.Message}");
            throw;
        }
    }
    public async Task RemoveBookFromUserCart(int userId, int bookId)
    {
        try
        {
            // Находим последнюю корзину пользователя
            var userCart = await Context.ShoppingCarts
                .Where(sc => sc.UserId == userId)
                .LastOrDefaultAsync();

            if (userCart != null)
            {
                // Находим книгу в корзине пользователя
                var bookToRemove = userCart.Books.FirstOrDefault(b => b.Id == bookId);

                if (bookToRemove != null)
                {
                    // Удаляем книгу из корзины пользователя
                    userCart.Books.Remove(bookToRemove);

                    // Сохраняем изменения в базе данных
                    await Context.SaveChangesAsync();
                }
                else
                {
                    throw new Exception($"Book with ID {bookId} not found in user's shopping cart.");
                }
            }
            else
            {
                throw new Exception("User's shopping cart not found.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while removing book from user's cart: {ex.Message}");
            throw;
        }
    }

    public async Task<ICollection<M_Book>> GetBooksForSaleByGenre(int genreId)
    {
        try
        {
            return await Context.Books.Where(x => x.Genres.Any(x => x.Id == genreId)).ToListAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return null;
        }
    }
    public async Task<ICollection<M_Book>> GetPromotionBooks()
    {
        try
        {
            return await Context.Books.Where(x => x.Discount > 0).ToListAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return null;
        }
    }
    public async Task<ICollection<M_Book>> GetBooksForSaleByAuthor(int authorId)
    {
        try
        {
            return await Context.Books.Where(x => x.AuthorId == authorId).ToListAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return null;
        }
    }



    // USER INFORMATION
    public async Task<bool> CheckUserExists(string username)
    {
        try
        {
            var users = await Context.Users.ToListAsync();
            return users.Any(u => u.Username == username);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while checking if user exists: {ex.Message}");
            return false;
        }
    }
    public async Task<M_User> AuthorizeUser(string username, string userpassword)
    {
        try
        {
            var user = await Context.Users.FirstOrDefaultAsync(u => u.Username == username);

            if (user != null && user.Password == userpassword)
            {

                return user;
            }
            else
            {
                return null;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while authorizing user: {ex.Message}");
            return null;
        }
    }
    public async Task<M_User> RegisterUser(string username, string userpassword)
    {
        try
        {
            bool userExists = await CheckUserExists(username);

            if (!userExists)
            {
                var newUser = new M_User { Username = username, Password = userpassword };
                await Context.Users.AddAsync(newUser);
                await Context.SaveChangesAsync();

                await CreateShoppingCart(newUser.Id);

                return newUser;
            }
            else
            {
                // Пользователь с таким именем уже существует
                return null;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while registering user: {ex.Message}");
            return null;
        }
    }
}
