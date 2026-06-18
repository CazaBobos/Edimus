using Mediator;

namespace Categories.Core.Features.CreateCategory;

public class CreateCategoryRequest : IRequest<CreateCategoryResponse>
{
    public int CompanyId { get; set; }
    public string Name { get; set; } = string.Empty;
}