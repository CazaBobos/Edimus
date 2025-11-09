using MediatR;
using Shared.Core.Abstractions;
using Shared.Core.Entities;

namespace Tables.Core.Features.CreateTable;

public class CreateTableRequest : IRequest<CreateTableResponse>
{
    public int LayoutId { get; set; }
    public TableStatus Status { get; set; } = TableStatus.Free;
    public int PositionX { get; set; }
    public int PositionY { get; set; }
    public List<(int, int)> Surface { get; set; } = new() { (0, 0) };
    public IUserRecord? User { get; set; }
}