namespace Products.Input;

public class CreateProductInput
{
    public int? Parent { get; set; }
    public int? Category { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
}
