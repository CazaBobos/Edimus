using MediatR;

namespace Tables.Core.Features.LinkTable;

public class LinkTableRequest : IRequest<LinkTableResponse>
{
    public string TableId { get; set; } = string.Empty;
}