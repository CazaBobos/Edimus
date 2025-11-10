namespace Shared.Core.Entities;
public class Consumption
{
    public int ProductId { get; protected set; }
    public virtual Product? Product { get; protected set; }
    public int IngredientId { get; protected set; }
    public virtual Ingredient? Ingredient { get; protected set; }
    public int Amount { get; protected set; }

    protected Consumption() { }
    public Consumption(int productId, int ingredientId, int amount)
    {
        ProductId = productId;
        IngredientId = ingredientId;
        Amount = amount;
    }
}
