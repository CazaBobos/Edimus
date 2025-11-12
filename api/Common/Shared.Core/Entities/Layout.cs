using Dawn;
using Shared.Core.Domain;

namespace Shared.Core.Entities;

public class Layout : Entity<int>
{
    public override int Id { get; protected set; }
    public string Name { get; protected set; } = string.Empty;
    public int PremiseId { get; protected set; }
    public virtual Premise? Premise { get; protected set; }
    public virtual List<Table>? Tables { get; protected set; }
    public virtual List<Sector>? Sectors { get; protected set; }
    public virtual List<LayoutCoord>? Boundaries { get; protected set; }

    protected Layout() { }
    public Layout(int premiseId, string name)
    {
        PremiseId = Guard.Argument(() => premiseId).Positive();
        Name = Guard.Argument(() => name).NotNull().NotEmpty();
        Enabled = true;
    }
}
