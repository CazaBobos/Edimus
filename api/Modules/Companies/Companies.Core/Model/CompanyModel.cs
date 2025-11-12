using Shared.Core.Domain;

namespace Companies.Core.Model;

public class CompanyModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Slogan { get; set; } = string.Empty;
    public string Acronym { get; set; } = string.Empty;
    public List<PremiseModel> Premises { get; set; } = new();
    public List<AuditRecord> History { get; set; } = new();
    public bool Enabled { get; set; }
}