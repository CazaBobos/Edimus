using MediatR;
using Shared.Core.Abstractions;

namespace Companies.Core.Features.CreateCompany;

public class CreateCompanyRequest : IRequest<CreateCompanyResponse>
{
    public string Name { get; set; } = string.Empty;
    public string Slogan { get; set; } = string.Empty;
    public string? Acronym { get; set; }
    public IUserRecord? User { get; set; }
}