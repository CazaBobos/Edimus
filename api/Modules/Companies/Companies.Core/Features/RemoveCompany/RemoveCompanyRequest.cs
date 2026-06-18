using Mediator;

namespace Companies.Core.Features.RemoveCompany;

public class RemoveCompanyRequest : IRequest<RemoveCompanyResponse>
{
    public int Id { get; set; }
}