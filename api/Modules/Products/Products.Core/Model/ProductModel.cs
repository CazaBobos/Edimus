using Shared.Core.Domain;

namespace Products.Core.Model;

public class ProductModel
{
    public int Id { get; set; }
    public int? ParentId { get; set; }
    public int? CategoryId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int? ImageId { get; set; }
    public List<AuditRecord> History { get; set; } = new();
    public bool Enabled { get; set; }
}
