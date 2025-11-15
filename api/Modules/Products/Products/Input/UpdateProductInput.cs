namespace Products.Input;

public class UpdateProductInput
{
    public int? ParentId { get; set; }
    public int? CategoryId { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal? Price { get; set; }
    public byte[]? Image { get; set; }
    public List<int>? Tags { get; set; }
}
