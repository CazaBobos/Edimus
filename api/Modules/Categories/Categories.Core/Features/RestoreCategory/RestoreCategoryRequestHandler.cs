using Categories.Core.Abstractions;
using MediatR;
using Shared.Core.Exceptions;

namespace Categories.Core.Features.RestoreCategory;

public class RestoreCategoryRequestHandler : IRequestHandler<RestoreCategoryRequest, RestoreCategoryResponse>
{
    private readonly ICategoriesRepository _categoriesRepository;

    public RestoreCategoryRequestHandler(ICategoriesRepository categoriesRepository)
    {
        _categoriesRepository = categoriesRepository;
    }

    public async Task<RestoreCategoryResponse> Handle(RestoreCategoryRequest request, CancellationToken cancellationToken)
    {
        var category = await _categoriesRepository.GetById(request.Id, cancellationToken);

        if (category is null) throw new HttpNotFoundException();

        category.Restore();

        await _categoriesRepository.Update(category, cancellationToken);

        return new RestoreCategoryResponse();
    }
}