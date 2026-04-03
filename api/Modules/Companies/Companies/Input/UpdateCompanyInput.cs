namespace Companies.Input;
public class UpdateCompanyInput
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Slogan { get; set; }
    public bool? ReactiveStock { get; set; }
    public bool? PublicPrices { get; set; }
    public bool? PublicOrders { get; set; }
}
