using Categories.Core.Model;

namespace Categories.Core.Features.GetManyCategories;

public class GetCategoriesResponse
{
    public int Count { get; set; }
    public int? Limit { get; set; }
    public int? Page { get; set; }
    public List<CategoryModel> Categories { get; set; } = new();
}