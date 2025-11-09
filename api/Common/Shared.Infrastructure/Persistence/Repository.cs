using Microsoft.EntityFrameworkCore;
using Shared.Core.Domain;
using Shared.Core.Persistence;
using System.Linq.Expressions;

namespace Shared.Infrastructure.Persistence;
public abstract class Repository<TAggregate, TIdentity> : IRepository<TAggregate, TIdentity>, IDisposable where TAggregate : Entity<TIdentity>
{
    protected readonly DatabaseContext _context;

    protected Repository(DatabaseContext context) => 
        _context = context ?? throw new ArgumentNullException(nameof(context));

    public IQueryable<TAggregate> AsQueryable()
    {
        return _context.Set<TAggregate>().AsQueryable();
    }
    public async Task<TAggregate?> GetById(TIdentity id, CancellationToken cancellationToken)
    {
        return await _context.Set<TAggregate>().FindAsync(new object[] { id }, cancellationToken);
    }
    public async Task<TAggregate?> FindOne(Expression<Func<TAggregate, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await _context.Set<TAggregate>().FirstOrDefaultAsync(predicate, cancellationToken);
    }
    public async Task<bool> Add(TAggregate aggregate, CancellationToken cancellationToken)
    {
        await _context.Set<TAggregate>().AddAsync(aggregate, cancellationToken);
        return await _context.SaveChangesAsync(cancellationToken) > 0;
    }
    public async Task<bool> Update(TAggregate aggregate, CancellationToken cancellationToken)
    {
        //_context.Entry(aggregate).State = EntityState.Modified;
        _context.Set<TAggregate>().Update(aggregate);
        return await _context.SaveChangesAsync(cancellationToken) > 0;
    }
    public async Task<bool> Remove(TAggregate aggregate, CancellationToken cancellationToken)
    {
        _context.Set<TAggregate>().Remove(aggregate);
        return await _context.SaveChangesAsync(cancellationToken) > 0;
    }
    public Task RemoveRange(IEnumerable<TAggregate> aggregates)
    {
        _context.RemoveRange(aggregates);
        return Task.CompletedTask;
    }
    public Task SaveChanges(CancellationToken cancellationToken = default)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }
    public void Dispose() { }
}
