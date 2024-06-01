
using Basket.API.Data;

using Discount.Grpc;

namespace Basket.API.Basket.StoreBasket;

public record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketResult>;
public record StoreBasketResult(string UserName);

public class StoreBasketCommandHandler
    (IBasketRepository repository, DiscountProtoService.DiscountProtoServiceClient discountProto)
    : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
    {
        // call Discount.Grpc to calculate latest prices 
        await DeductDiscount(command.Cart, cancellationToken);

        // store basket to db (use Marten upsert)
        await repository.StoreBasket(command.Cart, cancellationToken);

        // update cache
        // return new StoreBasketResult("swn");
        return new StoreBasketResult(command.Cart.UserName);


    }

    private async Task DeductDiscount(ShoppingCart cart, CancellationToken cancellationToken)
    {
        foreach (var item in cart.Items)
        {
            var coupon = await discountProto.GetDiscountAsync(
                new GetDiscountRequest { ProductName = item.ProductName }, cancellationToken: cancellationToken
            );
            item.Price -= coupon.Amount;
        }
    }
}

