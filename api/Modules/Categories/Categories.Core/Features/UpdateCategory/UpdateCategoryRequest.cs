using MediatR;
using Shared.Core.Abstractions;

namespace Categories.Core.Features.UpdateCategory;

public class UpdateCategoryRequest : IRequest<UpdateCategoryResponse>
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public IUserRecord User { get; set; } = null!;
}