namespace Premises.Input;

public class GetPremisesInput
{
    public int? Limit { get; set; }
    public int? Page { get; set; }
    public int? CompanyId { get; set; }
    public bool? Enabled { get; set; }
}
