using Basket.API.Entities;
using Basket.API.GrpcServices;
using Basket.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _basketRepository;
        private readonly DiscountGrpcService _grpcService;

        public BasketController(IBasketRepository basketRepository, DiscountGrpcService grpcService)
        {
            _basketRepository = basketRepository;
            _grpcService = grpcService;
        }

        [HttpGet(Name = "GetBasket")]
        public async Task<ActionResult<ShoppingCart>> GetBasket([FromQuery] string userName)
        {
            if (String.IsNullOrWhiteSpace(userName)) return BadRequest();
            var result = await _basketRepository.GetBasket(userName);

            if (result is not null)
            {
                foreach (var item in result.ShoppingCartItems)
                {
                    var coupon = await _grpcService.GetDiscount(item.ProductName);
                    if (coupon == null) continue;

                    item.Price -= coupon.Amount;
                }
            }

            return Ok(result ?? new ShoppingCart(userName));
        }

        [HttpPost(Name = "UpdateBasket")]
        public async Task<ActionResult<ShoppingCart>> UpdateBasket([FromQuery] string userName, [FromBody] ShoppingCart shoppingCart)
        {
            if (String.IsNullOrWhiteSpace(userName) || userName != shoppingCart.UserName) return BadRequest();
            var result = await _basketRepository.UpdateBasket(shoppingCart);
            return Ok(result);
        }

        [HttpDelete]
        public async Task<ActionResult<ShoppingCart>> DeleteBasket([FromQuery] string userName)
        {
            await _basketRepository.DeleteBasket(userName);
            return Ok();
        }
    }
}
