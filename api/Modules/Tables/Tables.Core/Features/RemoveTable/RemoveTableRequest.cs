using MediatR;
using Shared.Core.Abstractions;

namespace Tables.Core.Features.RemoveTable;

public class RemoveTableRequest : IRequest<RemoveTableResponse>
{
    public int Id { get; set; }
    public IUserRecord? User { get; set; }
}