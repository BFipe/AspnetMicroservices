using Discount.Grpc.Protos;
using static Discount.Grpc.Protos.DiscountProtoService;

namespace Basket.API.GrpcServices
{
    public class DiscountGrpcService
    {
        private readonly DiscountProtoServiceClient _protoService;

        public DiscountGrpcService(DiscountProtoServiceClient protoService)
        {
            _protoService = protoService;
        }

        public async Task<CouponModel> GetDiscount(string productName)
        {
            var discountRequest = new GetDiscountRequest { ProductName = productName };
            return await _protoService.GetDiscountAsync(discountRequest);
        }
    }
}
