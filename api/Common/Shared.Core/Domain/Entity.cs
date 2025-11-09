using Shared.Core.Abstractions;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.Core.Domain;
public class Entity<TIdentity> : IEquatable<Entity<TIdentity>>
{
    public virtual TIdentity Id { get; protected set; } = default!;
    public bool Enabled { get; protected set; }

    #region Constructors
    protected Entity(TIdentity id) => Id = id;
    protected Entity() {
    }
    #endregion

    #region IEquatable implementations
    public virtual bool Equals(Entity<TIdentity>? other)
    {
        if (ReferenceEquals(null, other)) return false;

        if (ReferenceEquals(this, other)) return true;

        return Equals(Id, other.Id);
    }
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;

        if (ReferenceEquals(this, obj)) return true;

        if (obj.GetType() != GetType()) return false;

        return Equals((Entity<TIdentity>)obj);
    }
    public override int GetHashCode() => GetType().GetHashCode() * 907 + Id.GetHashCode();
    #endregion

    #region Object Overrides
    public override string ToString() => $"{GetType().Name}#[Identity={Id}]";
    #endregion
    
    public static bool operator ==(Entity<TIdentity> a, Entity<TIdentity> b)
    {
        if (ReferenceEquals(a, null) && ReferenceEquals(b, null)) return true;

        if (ReferenceEquals(a, null) || ReferenceEquals(b, null)) return false;

        return a.Equals(b);
    }
    public static bool operator !=(Entity<TIdentity> a, Entity<TIdentity> b)
    {
        return !(a == b);
    }

    public void Remove()
    {
        Enabled = false;
    }

    public void Restore()
    {
        Enabled = true;
    }
}
