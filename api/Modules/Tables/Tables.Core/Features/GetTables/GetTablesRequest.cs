using MediatR;
using Shared.Core.Abstractions;

namespace Tables.Core.Features.GetManyTables;

public class GetTablesRequest : IRequest<GetTablesResponse>
{
    public int? Limit { get; set; }
    public int? Page { get; set; }
    public long? CompanyId { get; set; }
    public bool? Enabled { get; set; }
    public IUserRecord? User { get; set; }
}