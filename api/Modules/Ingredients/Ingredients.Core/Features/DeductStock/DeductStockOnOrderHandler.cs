using Ingredients.Core.Abstractions;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Notifications;

namespace Ingredients.Core.Features.DeductStock;

public class DeductStockOnOrderHandler : INotificationHandler<OrdersConfirmedNotification>
{
    private readonly IIngredientsRepository _ingredientsRepository;

    public DeductStockOnOrderHandler(IIngredientsRepository ingredientsRepository)
    {
        _ingredientsRepository = ingredientsRepository;
    }

    public async ValueTask Handle(OrdersConfirmedNotification notification, CancellationToken cancellationToken)
    {
        if (notification.Delta.Count == 0) return;

        var productIds = notification.Delta.Select(d => d.ProductId).ToList();

        var consumptions = await _ingredientsRepository.AsQueryable()
            .SelectMany(i => i.Consumptions)
            .Where(c => productIds.Contains(c.ProductId))
            .ToListAsync(cancellationToken);

        foreach (var (productId, amount) in notification.Delta)
        {
            foreach (var c in consumptions.Where(c => c.ProductId == productId))
            {
                c.Ingredient!.Deduct(c.Amount * amount);
            }
        }

        await _ingredientsRepository.SaveChanges(cancellationToken);
    }
}
