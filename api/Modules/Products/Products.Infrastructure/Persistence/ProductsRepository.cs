using Products.Core.Abstractions;
using Shared.Core.Entities;
using Shared.Infrastructure.Persistence;

namespace Products.Infrastructure.Persistence;

public class ProductsRepository : Repository<Product, int>, IProductsRepository
{
    public ProductsRepository(DatabaseContext context) : base(context)
    {
    }
}