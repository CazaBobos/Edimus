using MediatR;
using Shared.Core.Abstractions;
using Shared.Core.Entities;

namespace Tables.Core.Features.GetManyTables;

public class GetTablesRequest : IRequest<GetTablesResponse>
{
    public int? Limit { get; set; }
    public int? Page { get; set; }
    public int? LayoutId { get; set; }
    public TableStatus? Status { get; set; }
    public bool? Enabled { get; set; }
    public IUserRecord? User { get; set; }
}