using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace KRD.Repo;

public interface IRepository <T> where T : class
{
    /*Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetByKeysAsync(params object[] keys);
    Task<EntityEntry<T>> Create(T entity);
    void Update(T entity);
    void Delete(T entity);
    Task SaveChangesAsync();*/
    IQueryable<T> GetQueryable();
    Task<List<T>> GetAllAsync();
    Task<T?> GetByIdAsync(int id);
    Task AddAsync(T entity);
    void Update(T entity);
    void Delete(T entity);
    Task SaveChangesAsync();
}