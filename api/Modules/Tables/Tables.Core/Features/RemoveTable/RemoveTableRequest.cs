using Mediator;

namespace Tables.Core.Features.RemoveTable;

public class RemoveTableRequest : IRequest<RemoveTableResponse>
{
    public int Id { get; set; }
}