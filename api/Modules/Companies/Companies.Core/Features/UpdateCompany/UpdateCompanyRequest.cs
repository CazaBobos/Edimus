using MediatR;
using Shared.Core.Abstractions;

namespace Companies.Core.Features.UpdateCompany;

public class UpdateCompanyRequest : IRequest<UpdateCompanyResponse>
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Slogan { get; set; } = string.Empty;
    public IUserRecord? User { get; set; }
}