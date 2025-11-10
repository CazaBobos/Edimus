using Dawn;

namespace Shared.Core.Entities;
public class Request
{
    public int ProductId { get; protected set; }
    public virtual Product? Product { get; protected set; }
    public int TableId { get; protected set; }
    public virtual Table? Table{ get; protected set; }
    public int Amount{ get; protected set; }

    protected Request() { }
    public Request(int productId, int tableId, int amount)
    {
        ProductId = Guard.Argument(() => productId).Positive();
        TableId = Guard.Argument(() => tableId).Positive();
        Amount = Guard.Argument(() => amount).Positive();
    }

    public void Update(int productId)
    {
        ProductId = Guard.Argument(() => productId).Positive();
    }
}