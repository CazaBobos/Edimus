namespace Layouts.Input;

public class CreateLayoutInput
{
    public int PremiseId { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Height { get; set; }
    public int Width { get; set; }
}
