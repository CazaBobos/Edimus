using MediatR;
using Shared.Core.Abstractions;

namespace Companies.Core.Features.RemoveCompany;

public class RemoveCompanyRequest : IRequest<RemoveCompanyResponse>
{
    public int Id { get; set; }
    public IUserRecord? User { get; set; }
}