using MediatR;
using Shared.Core.Abstractions;

namespace Categories.Core.Features.RestoreCategory;

public class RestoreCategoryRequest : IRequest<RestoreCategoryResponse>
{
    public int Id { get; set; }
    public IUserRecord User { get; set; } = null!;
}