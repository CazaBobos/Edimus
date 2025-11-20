namespace Companies.Core.Model;

public class LayoutModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Height { get; set; }
    public int Width { get; set; }
    public virtual List<LayoutCoordModel>? Boundaries { get; set; }
}
