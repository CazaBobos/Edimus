namespace Shared.Core.Domain;
public abstract class AggregateRoot<TIdentity> : Entity<TIdentity>
{
    protected AggregateRoot(TIdentity id) : base(id)
    {
    }

    protected AggregateRoot()
    {
    }
}