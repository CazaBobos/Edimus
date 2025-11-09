using MediatR;
using Shared.Core.Abstractions;

namespace Companies.Core.Features.RestoreCompany;

public class RestoreCompanyRequest : IRequest<RestoreCompanyResponse>
{
    public int Id { get; set; }
    public IUserRecord? User { get; set; }
}