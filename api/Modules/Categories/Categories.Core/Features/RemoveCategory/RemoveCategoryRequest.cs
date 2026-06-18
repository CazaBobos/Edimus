using Mediator;

namespace Categories.Core.Features.RemoveCategory;

public class RemoveCategoryRequest : IRequest<RemoveCategoryResponse>
{
    public int Id { get; set; }
}