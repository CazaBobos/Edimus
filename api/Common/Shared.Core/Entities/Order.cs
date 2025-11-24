using Dawn;

namespace Shared.Core.Entities;
public class Order
{
    public int ProductId { get; protected set; }
    public virtual Product? Product { get; protected set; }
    public int TableId { get; protected set; }
    public virtual Table? Table { get; protected set; }
    public int Amount { get; protected set; }

    protected Order() { }
    public Order(int productId, int tableId, int amount)
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