using KRD.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace KRD.Repo;

public class Repository <T>: IRepository<T> where T : class
{
    private readonly AppDbContext db;
    private readonly DbSet<T> _dbSet;

    public Repository(AppDbContext context)
    {
        db = context;
        _dbSet = context.Set<T>();
    }

    public async Task<List<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public IQueryable<T> GetQueryable()
    {
        return _dbSet.AsQueryable();
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<EntityEntry<T>> Create(T entity)
    {
        return await _dbSet.AddAsync(entity);
    }

    public Task AddAsync(T entity)
    {
        return _dbSet.AddAsync(entity).AsTask();
    }

    public void Update(T entity)
    {
        _dbSet.Update(entity);
    }

    public void Delete(T entity)
    {
        _dbSet.Remove(entity);
    }

    public async Task SaveChangesAsync()
    {
        await db.SaveChangesAsync();
    }
}