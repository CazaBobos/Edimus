using AutoMapper;
using Categories.Core.Abstractions;
using Categories.Core.Model;
using MediatR;
using Shared.Core.Entities;
using Shared.Core.Exceptions;

namespace Categories.Core.Features.UpdateCategory;

public class UpdateCategoryRequestHandler : IRequestHandler<UpdateCategoryRequest, UpdateCategoryResponse>
{
    private readonly ICategoriesRepository _categoriesRepository;

    public UpdateCategoryRequestHandler(ICategoriesRepository categoriesRepository)
    {
        _categoriesRepository = categoriesRepository;
    }

    public async Task<UpdateCategoryResponse> Handle(UpdateCategoryRequest request, CancellationToken cancellationToken)
    {
        var category = await _categoriesRepository.GetById(request.Id, cancellationToken);

        if (category is null) throw new HttpNotFoundException();

        category.Update(request.Name);

        await _categoriesRepository.Update(category, cancellationToken);

        return new UpdateCategoryResponse();
    }
}