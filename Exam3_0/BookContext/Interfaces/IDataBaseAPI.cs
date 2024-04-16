
using BookContext.Models;

namespace BookContext.Interfaces;

public interface IDataBaseAPI
{
    BookContext Context { get; }
    Task<bool> Add<T>(T entity) where T : DbEntity;
    Task<bool> Delete<T>(int id) where T : DbEntity;
    Task<bool> Delete<T>(T entity) where T : DbEntity;

    Task<bool> Update<T>(int id, T entity) where T : DbEntity;
    Task<bool> Update<T>(T oldEntity, T newEntity) where T : DbEntity;

    Task<List<T>> Get<T>() where T : DbEntity;
    Task<List<T>> Get<T>(
        int page,
        int entitiesPerPage = 10
    ) where T : DbEntity;
    Task<List<TEntity>> Get<TEntity, TValue>(
        string propertyName,
        TValue propertyValue
    ) where TEntity : DbEntity;
    Task<List<TEntity>> Get<TEntity, TValue>(
        string propertyName,
        TValue propertyValue,
        int page,
        int entitiesPerPage = 10
    ) where TEntity : DbEntity;
    Task<List<TEntity>> Get<TEntity>(
        Dictionary<string, (object value, bool isExactMatch)> propertyFilters
    ) where TEntity : DbEntity;
    Task<List<TEntity>> Get<TEntity>(
        int page,
        Dictionary<string, (object value, bool isExactMatch)> propertyFilters,
        int entitiesPerPage = 10
    ) where TEntity : DbEntity;


    Task<List<TEntity>> ExecuteStoredProcedureAsync<TEntity>(string procedureName, Dictionary<string, object> parameters) where TEntity : class;


    // REMOVE AFTER
    Task AddBookToUserCart(int userId, int bookId);
    Task RemoveBookFromUserCart(int userId, int bookId);
    Task<M_ShoppingCart> CreateShoppingCart(int userId);
    Task<bool> CheckUserExists(string username);
    Task<M_User> AuthorizeUser(string username, string userpassword);
    Task<M_User> RegisterUser(string username, string userpassword);
    Task<ICollection<M_Book>> GetBooksForSaleByGenre(int genreId);
    Task<ICollection<M_Book>> GetBooksForSaleByAuthor(int authorId);
    Task<ICollection<M_Book>> GetPromotionBooks();
}
