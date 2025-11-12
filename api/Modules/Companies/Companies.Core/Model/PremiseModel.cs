namespace Companies.Core.Model;

public class PremiseModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<LayoutModel> Layouts { get; set; } = new();
}
