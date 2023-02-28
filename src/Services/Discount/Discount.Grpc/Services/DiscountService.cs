using AutoMapper;
using Discount.Grpc.Entities;
using Discount.Grpc.Protos;
using Discount.Grpc.Repositories;
using Grpc.Core;

namespace Discount.Grpc.Services
{
    public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
    {
        private readonly IDiscountRepository _discountRepository;
        private readonly ILogger<DiscountService> _logger;
        private readonly IMapper _mapper;

        public DiscountService(IDiscountRepository discountRepository, ILogger<DiscountService> logger, IMapper mapper)
        {
            _discountRepository = discountRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var coupon = await _discountRepository.GetDiscount(request.ProductName);
            if (coupon == null) throw new RpcException(new Status(StatusCode.NotFound, $"Discount for Product {request.ProductName} not found"));

            _logger.LogInformation($"Discount is retrieved for Product: {coupon.ProductName} - {coupon.Description}, Amount = {coupon.Amount}");

            var couponModel = _mapper.Map<CouponModel>(coupon); 
            return couponModel;
        }

        public override async Task<CreateResponce> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var coupon = _mapper.Map<Coupon>(request.Coupon);
            var result = await _discountRepository.CreateDiscount(coupon);
            if (result == false) throw new RpcException(new Status(StatusCode.Aborted, $"Failed creating coupon for {coupon.ProductName}"));
            
            _logger.LogInformation($"Discount is created for Product: {coupon.ProductName} - {coupon.Description}, Amount = {coupon.Amount}");
            return new CreateResponce() { Success = result };
        }

        public override async Task<UpdateResponce> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var coupon = _mapper.Map<Coupon>(request.Coupon);
            var result = await _discountRepository.UpdateDiscount(coupon);
            if (result == false) throw new RpcException(new Status(StatusCode.Aborted, $"Failed updating coupon information for {coupon.ProductName}"));

            _logger.LogInformation($"Discount is updated for Product: {coupon.ProductName} - {coupon.Description}, Amount = {coupon.Amount}");
            return new UpdateResponce() { Success = result };
        }

        public override async Task<DeleteResponce> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var result = await _discountRepository.DeleteDiscount(request.ProductName);
            if (result == false) throw new RpcException(new Status(StatusCode.Aborted, $"Failed deleting coupon for {request.ProductName}"));
            _logger.LogInformation($"Discount is deleted for Product {request.ProductName}");
            return new DeleteResponce() { Success = result };
        }
    }
}
