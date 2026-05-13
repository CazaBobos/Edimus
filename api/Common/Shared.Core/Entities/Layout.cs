using Dawn;
using Shared.Core.Domain;

namespace Shared.Core.Entities;

public class Layout : Entity<int>
{
    public override int Id { get; protected set; }
    public string Name { get; protected set; } = string.Empty;
    public int Height { get; protected set; }
    public int Width { get; protected set; }
    public int PremiseId { get; protected set; }
    public virtual Premise? Premise { get; protected set; }
    public virtual List<Table> Tables { get; protected set; } = [];
    public virtual List<Sector> Sectors { get; protected set; } = [];
    public virtual List<LayoutCoord> Boundaries { get; protected set; } = [];

    protected Layout() { }
    public Layout(int premiseId, string name, int height, int width)
    {
        PremiseId = Guard.Argument(() => premiseId).Positive();
        Name = Guard.Argument(() => name).NotNull().NotEmpty();
        Height = Guard.Argument(() => height).Positive();
        Width = Guard.Argument(() => width).Positive();
        Enabled = true;
    }

    public void Update(string? name, int? height, int? width)
    {
        if (name is not null) Name = Guard.Argument(() => name).NotNull().NotEmpty();
        if (height is not null) Height = Guard.Argument(() => height.Value).Positive();
        if (width is not null) Width = Guard.Argument(() => width.Value).Positive();
    }

    public void UpdateBoundaries(List<LayoutCoord> boundaries)
    {
        Boundaries.Clear();
        Boundaries.AddRange(boundaries);
    }
}
