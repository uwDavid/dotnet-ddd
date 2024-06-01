using Grpc.Core;
namespace Discount.Grpc.Services;

public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
{
    public override Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    {
        // get discount from db
        return base.GetDiscount(request, context);
    }

    public override Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
    {
        return base.CreateDiscount(request, context);
    }

    public override Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
    {
        return base.UpdateDiscount(request, context);
    }

    public override Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
    {

        // var coupon = await dbContext
        //     .Coupons
        //     .FirstOrDefaultAsync(x => x.ProductName == request.ProductName);

        // if (coupon is null)
        //     throw new RpcException(new Status(StatusCode.NotFound, $"Discount with ProductName={request.ProductName} is not found."));

        // dbContext.Coupons.Remove(coupon);
        // await dbContext.SaveChangesAsync();

        // logger.LogInformation("Discount is successfully deleted. ProductName : {ProductName}", request.ProductName);

        // return new DeleteDiscountResponse { Success = true };
        return base.DeleteDiscount(request, context);
    }
}