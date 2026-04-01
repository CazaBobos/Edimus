using Mediator;

namespace Tables.Core.Features.GetTable;

public class GetTableRequest : IRequest<GetTableResponse>
{
    public int Id { get; set; }
}
