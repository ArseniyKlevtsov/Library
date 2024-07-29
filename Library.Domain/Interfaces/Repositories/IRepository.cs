using System.Linq.Expressions;

namespace Library.Domain.Interfaces.Repositories;

public interface IRepository<TEntity>
    where TEntity : IEntity
{
    Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken);
    Task<TEntity?> GetByIdAsync(Guid Id, CancellationToken cancellationToken);
    Task AddAsync(TEntity entity, CancellationToken cancellationToken);
    Task UpdateAsync(TEntity entity, CancellationToken cancellationToken);
    Task DeleteAsync(TEntity entity, CancellationToken cancellationToken);
    Task<TEntity?> GetByPredicateAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);
}
