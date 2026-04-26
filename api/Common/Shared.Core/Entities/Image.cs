using Shared.Core.Domain;

namespace Shared.Core.Entities;

public class Image : Entity<int>
{
    public override int Id { get; protected set; }
    public byte[] BLOB { get; protected set; } = Array.Empty<byte>();

    protected Image() { }
    public Image(byte[] blob)
    {
        BLOB = blob;
        Enabled = true;
    }

    public void Update(byte[] blob)
    {
        BLOB = blob;
    }
}
