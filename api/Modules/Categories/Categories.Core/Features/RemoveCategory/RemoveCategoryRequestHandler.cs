using Categories.Core.Abstractions;
using MediatR;
using Shared.Core.Exceptions;

namespace Categories.Core.Features.RemoveCategory;

public class RemoveCategoryRequestHandler : IRequestHandler<RemoveCategoryRequest, RemoveCategoryResponse>
{
    private readonly ICategoriesRepository _categoriesRepository;

    public RemoveCategoryRequestHandler(ICategoriesRepository categoriesRepository)
    {
        _categoriesRepository = categoriesRepository;
    }

    public async Task<RemoveCategoryResponse> Handle(RemoveCategoryRequest request, CancellationToken cancellationToken)
    {
        var category = await _categoriesRepository.GetById(request.Id, cancellationToken);

        if (category is null) throw new HttpNotFoundException();

        category.Remove();

        await _categoriesRepository.Update(category, cancellationToken);

        return new RemoveCategoryResponse();
    }
}