using Products.Core.Model;

namespace Products.Input;

public class CreateProductInput
{
    public int CompanyId { get; set; }
    public int? ParentId { get; set; }
    public int? CategoryId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
}
