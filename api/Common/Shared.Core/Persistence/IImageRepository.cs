using Shared.Core.Entities;

namespace Shared.Core.Persistence;

public interface IImageRepository : IRepository<Image, int>
{
    void MarkDeleted(Image image);
}
