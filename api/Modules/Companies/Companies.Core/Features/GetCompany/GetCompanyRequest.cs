using MediatR;

namespace Companies.Core.Features.GetCompany;

public class GetCompanyRequest : IRequest<GetCompanyResponse>
{
    public int Id { get; set; }
}