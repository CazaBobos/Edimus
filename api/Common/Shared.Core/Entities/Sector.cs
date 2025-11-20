using Dawn;
using Shared.Core.Domain;
namespace Shared.Core.Entities;

public class Sector : AggregateRoot<int>
{
    public override int Id { get; protected set; }
    public int LayoutId { get; protected set; }
    public virtual Layout? Layout { get; protected set; }
    public string Name { get; protected set; } = string.Empty;
    public string Color { get; protected set; } = string.Empty;
    public int PositionX { get; protected set; }
    public int PositionY { get; protected set; }
    public virtual List<SectorCoord>? Surface { get; protected set; }

    protected Sector() { }
    public Sector(int layoutId, int positionX, int positionY, string name, string color, List<(int, int)>? surface = null)
    {
        LayoutId = Guard.Argument(() => layoutId).Positive();
        PositionX = Guard.Argument(() => positionX).NotNegative();
        PositionY = Guard.Argument(() => positionY).NotNegative();
        Name = ValidateName(name);
        Color = ValidateName(color);

        if (surface is not null)
        {
            Guard.Argument(() => surface).MinCount(1).Require(surface => surface.Any(s => s.Item1 == 0 && s.Item2 == 0));
            Surface = surface!.Select(s => new SectorCoord(s.Item1, s.Item2, Id)).ToList();
        }
        Enabled = true;
    }

    public void Update(string? name, string? color, int? positionX, int? positionY, List<(int, int)>? surface)
    {
        var affectedMembers = new List<string>();

        if (name is not null && name != Name)
        {
            Name = ValidateName(name);
            affectedMembers.Add(nameof(Name));
        }
        if (color is not null && color != Color)
        {
            Color = ValidateName(color);
            affectedMembers.Add(nameof(Color));
        }
        if (positionX is not null && positionX != PositionX)
        {
            PositionX = (int)Guard.Argument(() => positionX).NotNegative();
            affectedMembers.Add(nameof(PositionX));
        }
        if (positionY is not null && positionY != PositionY)
        {
            PositionY = (int)Guard.Argument(() => positionY).NotNegative();
            affectedMembers.Add(nameof(PositionY));
        }
        if (surface is not null)
        {
            Guard.Argument(() => surface).Require(surface => surface.Any(s => s.Item1 == 0 && s.Item2 == 0));
            Surface?.Clear();
            var newSurface = surface.Select(s => new SectorCoord(s.Item1, s.Item2, Id)).ToList();
            Surface?.AddRange(newSurface);
            affectedMembers.Add(nameof(Surface));
        }
        //if (affectedMembers.Count != 0) AddHistory(user, AuditOperation.Updated, affectedMembers);
    }

    private string ValidateName(string name) => Guard.Argument(() => name).NotNull().NotEmpty().NotWhiteSpace().DoesNotContain("  ");
}
