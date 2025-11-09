using MediatR;
using Shared.Core.Abstractions;

namespace Categories.Core.Features.RemoveCategory;

public class RemoveCategoryRequest : IRequest<RemoveCategoryResponse>
{
    public int Id { get; set; }
    public IUserRecord User { get; set; } = null!;
}