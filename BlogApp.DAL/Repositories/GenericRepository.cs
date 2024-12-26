using BlogApp.Core.Entities.Common;
using BlogApp.Core.Repositories;
using BlogApp.DAL.Context;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.DAL.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity, new()
{
    private readonly BlogDbContext _context;
    protected DbSet<T> Table;
    public GenericRepository(BlogDbContext context)
    {
        _context = context;
        Table = context.Set<T>();
    }

    public async Task AddAsync(T entity) 
        => await Table.AddAsync(entity);

    public void Delete(T entity)
        => Table.Remove(entity);

    public async Task DeleteAsync(int id)
    {
        T? entity = await GetByIdAsync(id);
        Delete(entity!);
    }

    public IQueryable<T> GetAll() 
        => Table.AsQueryable();

    public async Task<T?> GetByIdAsync(int id)
        => await Table.FindAsync(id);

    public IQueryable<T> GetWhere(Func<T, bool> expression)
        => Table.Where(expression).AsQueryable();

    public async Task<bool> IsExistsAsync(int id) 
        => await Table.AnyAsync(x => x.Id == id);

    public async Task<int> SaveAsync() 
        => await _context.SaveChangesAsync();
}
