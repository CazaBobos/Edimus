using Mediator;

namespace Categories.Core.Features.RestoreCategory;

public class RestoreCategoryRequest : IRequest<RestoreCategoryResponse>
{
    public int Id { get; set; }
}