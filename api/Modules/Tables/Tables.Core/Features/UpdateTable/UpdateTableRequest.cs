using MediatR;
using Shared.Core.Abstractions;
using Shared.Core.Entities;

namespace Tables.Core.Features.UpdateTable;

public class UpdateTableRequest : IRequest<UpdateTableResponse>
{
    public int Id { get; set; }
    public TableStatus? Status { get; set; }
    public int? PositionX { get; set; }
    public int? PositionY { get; set; }
    public List<(int, int)>? Surface { get; set; }
    public IUserRecord? User { get; set; }
}