using Mediator;

namespace Companies.Core.Features.RestoreCompany;

public class RestoreCompanyRequest : IRequest<RestoreCompanyResponse>
{
    public int Id { get; set; }
}