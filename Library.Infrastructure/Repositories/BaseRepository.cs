using Library.Domain.Entities;
using Library.Domain.Interfaces;
using Library.Domain.Interfaces.Repositories;
using Library.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Library.Infrastructure.Repositories;

public class BaseRepository<TEntity> : IRepository<TEntity>
    where TEntity : class, IEntity
{
    protected readonly LibraryDbContext _context;
    protected readonly DbSet<TEntity> _dbSet;

    public BaseRepository(LibraryDbContext context)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _dbSet.AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<TEntity?> GetByIdAsync(Guid Id, CancellationToken cancellationToken)
    {
        return await _dbSet.AsNoTracking().FirstAsync(entity => entity.Id == Id,cancellationToken);
    }

    public async Task AddAsync(TEntity entity, CancellationToken cancellationToken)
    {
        await _dbSet.AddAsync(entity, cancellationToken);
    }
    public async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken)
    {
        _context.Entry(entity).State = EntityState.Modified;
    }

    public async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken)
    {
        _dbSet.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<TEntity?> GetByPredicateAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
    {
        return await _dbSet.AsNoTracking().FirstOrDefaultAsync(predicate, cancellationToken);
    }

    public async Task<int> GetCountAsync(CancellationToken cancellationToken)
    {
        return await _dbSet.CountAsync(cancellationToken);
    }
}
