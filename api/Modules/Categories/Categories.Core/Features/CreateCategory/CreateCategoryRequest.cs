using MediatR;
using Shared.Core.Abstractions;

namespace Categories.Core.Features.CreateCategory;

public class CreateCategoryRequest : IRequest<CreateCategoryResponse>
{
    public int CompanyId { get; set; }
    public string Name { get; set; } = string.Empty;
    public IUserRecord User { get; set; } = null!;
}