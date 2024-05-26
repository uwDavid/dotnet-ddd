
using Basket.API.Data;

namespace Basket.API.Basket.StoreBasket;

public record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketResult>;
public record StoreBasketResult(string UserName);

public class StoreBasketCommandHandler
    (IBasketRepository repository)
    : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
    {
        ShoppingCart cart = command.Cart;
        // store basket to db (use Marten upsert)
        await repository.StoreBasket(command.Cart, cancellationToken);

        // update cache
        // return new StoreBasketResult("swn");
        return new StoreBasketResult(command.Cart.UserName);
    }
}