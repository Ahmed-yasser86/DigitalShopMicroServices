using Discount.Grpc.Data;
using Discount.Grpc.Models;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Discount.Grpc.Services
{
    public class DiscountService(DiscountContext dbContext, ILogger logger) : DiscountProtoService.DiscountProtoServiceBase
    {

        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {

            var coupon = request.Adapt<Coupon>();

            if(coupon is null)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request object."));
            }

            dbContext.Add(coupon);
            await dbContext.SaveChangesAsync();
            logger.LogInformation("Discount is successfully updated. ProductName : {ProductName}", coupon.ProductName);
            return coupon.Adapt<CouponModel>();

        }

        public override Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            return base.DeleteDiscount(request, context);
        }

        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {

            var ProductDiscount = await dbContext.Coupons.FirstOrDefaultAsync(x=>x.ProductName== request.ProductName);

            if(ProductDiscount== null)
            {
                ProductDiscount = new Coupon { Amount = 0, ProductName = request.ProductName, Description = "No discount"};
            }

            logger.LogInformation("Discount is retrieved for ProductName : {productName}, Amount : {amount}", ProductDiscount.ProductName, ProductDiscount.Amount);
            var couponModel = ProductDiscount.Adapt<CouponModel>();
            return couponModel;
        }

        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var coupon = request.Adapt<Coupon>();

            if (coupon is null)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request object."));
            }

            dbContext.Remove(coupon);
            await dbContext.SaveChangesAsync();
            logger.LogInformation("Discount is successfully updated. ProductName : {ProductName}", coupon.ProductName);
            return coupon.Adapt<CouponModel>();
        }

    }
}
