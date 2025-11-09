using Shared.Core.Domain;
using System.Linq.Expressions;

namespace Shared.Core.Persistence;

public interface IRepository<TAggregate, in TIdentity> where TAggregate : Entity<TIdentity>
{
    IQueryable<TAggregate> AsQueryable();
    Task<TAggregate?> GetById(TIdentity id, CancellationToken cancellationToken = default);
    Task<TAggregate?> FindOne(Expression<Func<TAggregate, bool>> predicate, CancellationToken cancellationToken = default);
    Task<bool> Add(TAggregate aggregate, CancellationToken cancellationToken = default);
    Task<bool> Update(TAggregate aggregate, CancellationToken cancellationToken = default);
    Task<bool> Remove(TAggregate aggregate, CancellationToken cancellationToken = default);
    Task RemoveRange(IEnumerable<TAggregate> aggregates);
    Task SaveChanges(CancellationToken cancellationToken = default);
}
