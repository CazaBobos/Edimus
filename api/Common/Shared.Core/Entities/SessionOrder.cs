namespace Shared.Core.Entities;

public class SessionOrder
{
    public long Id { get; private set; }
    public long SessionId { get; private set; }
    public virtual TableSession? Session { get; private set; }
    public int ProductId { get; private set; }
    public string ProductName { get; private set; } = string.Empty;
    public decimal UnitPrice { get; private set; }
    public int Amount { get; private set; }

    protected SessionOrder() { }

    public SessionOrder(int productId, string productName, decimal unitPrice, int amount)
    {
        ProductId = productId;
        ProductName = productName;
        UnitPrice = unitPrice;
        Amount = amount;
    }
}
