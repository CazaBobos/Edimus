using Shared.Core.Domain;

namespace Categories.Core.Model;
public class CategoryModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int CompanyId { get; set; }
    public List<AuditRecord> History { get; set; } = new();
    public bool Enabled { get; set; }
}