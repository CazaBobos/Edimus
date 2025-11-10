using MediatR;
using Shared.Core.Abstractions;
using Shared.Core.Entities;

namespace Ingredients.Core.Features.GetManyIngredients;

public class GetIngredientsRequest : IRequest<GetIngredientsResponse>
{
    public int? Limit { get; set; }
    public int? Page { get; set; }
    public string? Name { get; set; }
    public int? MinStock { get; set; }
    public int? MaxStock { get; set; }
    public int? MinAlert { get; set; }
    public int? MaxAlert { get; set; }
    public MeasurementUnit? Unit { get; set; }
    public bool? Enabled { get; set; }
    public IUserRecord User { get; set; } = null!;
}