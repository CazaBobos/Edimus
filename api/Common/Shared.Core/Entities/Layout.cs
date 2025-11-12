using Dawn;
using Shared.Core.Domain;
using Shared.Core.Entities;

namespace Shared.Core.Entities;

public class Layout : Entity<int>
{
    public override int Id { get; protected set; }
    public string Name { get; protected set; } = string.Empty;
    public int EstablishmentId { get; protected set; }
    public virtual Establishment? Establishment{ get; protected set; }
    public virtual List<Table>? Tables{ get; protected set; }
    public virtual List<Wall>? Walls{ get; protected set; }
    public virtual List<Sector>? Sectors{ get; protected set; }

    protected Layout() { }
    public Layout(int establishmentId, string name)
    {
        EstablishmentId = Guard.Argument(() => establishmentId).Positive();
        Name = Guard.Argument(() => name).NotNull().NotEmpty();
        Enabled = true;
    }
}
