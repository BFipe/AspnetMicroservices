using Discount.API.Entities;
using Discount.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Discount.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
        private readonly IDiscountRepository _discountRepository;

        public DiscountController(IDiscountRepository discountRepository) 
        {
            _discountRepository = discountRepository;
        }

        [HttpGet("GetDiscount")]
        public async Task<ActionResult<Coupon>> GetDiscount(string productName)
        {
            var coupon = await _discountRepository.GetDiscount(productName);
            return Ok(coupon);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDiscount([FromBody] Coupon coupon)
        {
            var result = await _discountRepository.CreateDiscount(coupon);
            if (result) return Ok();
            else return BadRequest(); 
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDiscount([FromBody] Coupon coupon)
        {
            var result = await _discountRepository.UpdateDiscount(coupon);
            if (result) return Ok();
            else return BadRequest();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteDiscount(string productName)
        {
            var result = await _discountRepository.DeleteDiscount(productName);
            if (result) return Ok();
            else return BadRequest();
        }
    }
}
