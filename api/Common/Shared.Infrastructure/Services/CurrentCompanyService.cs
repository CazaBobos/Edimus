using Shared.Core.Abstractions;

namespace Shared.Infrastructure.Services;

public class CurrentCompanyService : ICurrentCompanyService
{
    public List<int> AllowedCompanyIds { get; set; } = [];
}
