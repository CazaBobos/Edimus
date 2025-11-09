using Shared.Core.Entities;
using Shared.Core.Persistence;

namespace Products.Core.Abstractions;

public interface IProductsRepository : IRepository<Product, int>
{
}