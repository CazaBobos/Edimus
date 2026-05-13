namespace Premises.Core.Model;

public class PremiseModel
{
    public int Id { get; set; }
    public int CompanyId { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool Enabled { get; set; }
}
