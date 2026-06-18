using Mediator;

namespace Categories.Core.Features.UpdateCategory;

public class UpdateCategoryRequest : IRequest<UpdateCategoryResponse>
{
    public int Id { get; set; }
    public string? Name { get; set; }
}