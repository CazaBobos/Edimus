using Dawn;
using Shared.Core.Domain;
using System.Text.Json;

namespace Shared.Core.Entities;

public class Wall : Entity<int>
{
    public override int Id { get; protected set; }
    public int LayoutId { get; protected set; }
    public virtual Layout? Layout { get; protected set; }
    public WallType Type { get; protected set; }
    public string Surface { get; protected set; } = string.Empty;

    protected Wall() { }
    public Wall(int layoutId, WallType type, List<(int, int)> surface)
    {
        LayoutId = Guard.Argument(() => layoutId).Positive();
        Type = type;
        Surface = JsonSerializer.Serialize(surface);
        Enabled = true;
    }
}
