using Dawn;
using Shared.Core.Domain;

namespace Shared.Core.Entities;
public class Request : Entity<int>
{
    public int ProductId { get; protected set; }
    public virtual Product? Product { get; protected set; }
    public int TableId { get; protected set; }
    public virtual Table? Table{ get; protected set; }
    public int Quantity{ get; protected set; }

    protected Request() { }
    public Request(int productId, int tableId, int quantity)
    {
        ProductId = Guard.Argument(() => productId).Positive();
        TableId = Guard.Argument(() => tableId).Positive();
        Quantity = Guard.Argument(() => quantity).Positive();
        Enabled = true;
    }

    public void Update(int productId)
    {
        ProductId = Guard.Argument(() => productId).Positive();
    }
}