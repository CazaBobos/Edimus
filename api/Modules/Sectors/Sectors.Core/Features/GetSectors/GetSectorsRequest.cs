using MediatR;
using Shared.Core.Abstractions;

namespace Sectors.Core.Features.GetManySectors;

public class GetSectorsRequest : IRequest<GetSectorsResponse>
{
    public int? Limit { get; set; }
    public int? Page { get; set; }
    public int? LayoutId { get; set; }
    public string? Name { get; set; }
    public bool? Enabled { get; set; }
    public IUserRecord? User { get; set; }
}