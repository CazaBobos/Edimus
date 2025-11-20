using MediatR;
using Shared.Core.Abstractions;
using Shared.Core.Entities;
using Tables.Core.Model;

namespace Tables.Core.Features.CreateTable;

public class CreateTableRequest : IRequest<CreateTableResponse>
{
    public int LayoutId { get; set; }
    public TableStatus Status { get; set; } = TableStatus.Free;
    public int PositionX { get; set; }
    public int PositionY { get; set; }
    public List<TableCoordModel> Surface { get; set; } = new();
    public IUserRecord? User { get; set; }
}