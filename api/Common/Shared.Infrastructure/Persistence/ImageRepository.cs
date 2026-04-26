using Mediator;
using Shared.Core.Entities;
using Shared.Core.Persistence;

namespace Shared.Infrastructure.Persistence;

public class ImageRepository : Repository<Image, int>, IImageRepository
{
    public ImageRepository(DatabaseContext context, IPublisher publisher) : base(context, publisher)
    {
    }

    // Marks as removed without saving the data.
    public void MarkDeleted(Image image) => _context.Remove(image);
}
