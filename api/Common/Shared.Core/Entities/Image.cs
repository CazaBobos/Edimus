using Shared.Core.Domain;

namespace Shared.Core.Entities;
public class Image : Entity<int>
{
    public override int Id { get; protected set; }
    public int ProductId { get; protected set; }
    public virtual Product? Product { get; protected set; }
    public byte[] BLOB { get; protected set; } = Array.Empty<byte>();

    protected Image() { }
    public Image(int productId, byte[] blob)
    {
        ProductId = productId;
        BLOB = blob;
        Enabled = true;
    }
}
