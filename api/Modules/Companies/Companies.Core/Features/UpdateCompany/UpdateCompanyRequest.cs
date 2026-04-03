using Mediator;
using Shared.Core.Abstractions;

namespace Companies.Core.Features.UpdateCompany;

public class UpdateCompanyRequest : IRequest<UpdateCompanyResponse>
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Slogan { get; set; }
    public bool? ReactiveStock { get; set; }
    public bool? PublicPrices { get; set; }
    public bool? PublicOrders { get; set; }
    public IUserRecord? User { get; set; }
}