using AutoMapper;
using Categories.Core.Abstractions;
using Categories.Core.Model;
using MediatR;
using Shared.Core.Entities;

namespace Categories.Core.Features.CreateCategory;

public class CreateCategoryRequestHandler : IRequestHandler<CreateCategoryRequest, CreateCategoryResponse>
{
    private readonly ICategoriesRepository _categoriesRepository;

    public CreateCategoryRequestHandler(ICategoriesRepository categoriesRepository)
    {
        _categoriesRepository = categoriesRepository;
    }

    public async Task<CreateCategoryResponse> Handle(CreateCategoryRequest request, CancellationToken cancellationToken)
    {
        var category = new Category(request.CompanyId, request.Name);

        var existingCategory = await _categoriesRepository
            .FindOne(x => x.Id == category.Id || x.Name == category.Name);

        if (existingCategory is not null)
            throw new InvalidOperationException("The category already exists");

        await _categoriesRepository.Add(category, cancellationToken);

        return new CreateCategoryResponse
        {
            Id = category.Id
        };
    }
}