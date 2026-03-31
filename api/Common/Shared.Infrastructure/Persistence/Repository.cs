using Mediator;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Domain;
using Shared.Core.Persistence;
using System.Linq.Expressions;

namespace Shared.Infrastructure.Persistence;

public abstract class Repository<TAggregate, TIdentity> : IRepository<TAggregate, TIdentity>, IDisposable
    where TAggregate : Entity<TIdentity>
{
    protected readonly DatabaseContext _context;
    private readonly IPublisher _publisher;

    protected Repository(DatabaseContext context, IPublisher publisher)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _publisher = publisher;
    }

    public IQueryable<TAggregate> AsQueryable()
    {
        return _context.Set<TAggregate>().AsQueryable();
    }

    public async Task<TAggregate?> GetById(TIdentity id, CancellationToken cancellationToken = default)
    {
        return await _context.Set<TAggregate>().FindAsync(new object[] { id! }, cancellationToken);
    }

    public async Task<TAggregate?> FindOne(Expression<Func<TAggregate, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await _context.Set<TAggregate>().FirstOrDefaultAsync(predicate, cancellationToken);
    }

    public async Task<bool> Add(TAggregate aggregate, CancellationToken cancellationToken = default)
    {
        await _context.Set<TAggregate>().AddAsync(aggregate, cancellationToken);
        var result = await _context.SaveChangesAsync(cancellationToken) > 0;
        await DispatchDomainEvents(aggregate, cancellationToken);
        return result;
    }

    public async Task<bool> Update(TAggregate aggregate, CancellationToken cancellationToken = default)
    {
        _context.Set<TAggregate>().Update(aggregate);
        var result = await _context.SaveChangesAsync(cancellationToken) > 0;
        await DispatchDomainEvents(aggregate, cancellationToken);
        return result;
    }

    public async Task<bool> Remove(TAggregate aggregate, CancellationToken cancellationToken = default)
    {
        _context.Set<TAggregate>().Remove(aggregate);
        var result = await _context.SaveChangesAsync(cancellationToken) > 0;
        await DispatchDomainEvents(aggregate, cancellationToken);
        return result;
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

    private async Task DispatchDomainEvents(TAggregate aggregate, CancellationToken cancellationToken)
    {
        if (aggregate is not AggregateRoot<TIdentity> root || root.DomainEvents.Count == 0) return;

        foreach (var domainEvent in root.DomainEvents)
            await _publisher.Publish(domainEvent, cancellationToken);

        root.ClearDomainEvents();
    }
}
