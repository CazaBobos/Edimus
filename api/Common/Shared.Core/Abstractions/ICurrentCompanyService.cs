namespace Shared.Core.Abstractions;

public interface ICurrentCompanyService
{
    List<int> AllowedCompanyIds { get; set; }
}
