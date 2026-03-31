using Mediator;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.Core.Domain;

public abstract class AggregateRoot<TIdentity> : Entity<TIdentity>
{
    private readonly List<INotification> _domainEvents = [];

    [NotMapped]
    public IReadOnlyList<INotification> DomainEvents => _domainEvents.AsReadOnly();

    protected void AddDomainEvent(INotification domainEvent) => _domainEvents.Add(domainEvent);
    public void ClearDomainEvents() => _domainEvents.Clear();

    protected AggregateRoot(TIdentity id) : base(id) { }
    protected AggregateRoot() { }
}
