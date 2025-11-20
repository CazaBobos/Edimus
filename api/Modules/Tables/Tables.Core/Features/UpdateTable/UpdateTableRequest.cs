using MediatR;
using Shared.Core.Abstractions;
using Shared.Core.Entities;
using Tables.Core.Model;

namespace Tables.Core.Features.UpdateTable;

public class UpdateTableRequest : IRequest<UpdateTableResponse>
{
    public int Id { get; set; }
    public TableStatus? Status { get; set; }
    public int? PositionX { get; set; }
    public int? PositionY { get; set; }
    public List<TableCoordModel>? Surface { get; set; }
    public IUserRecord? User { get; set; }
}