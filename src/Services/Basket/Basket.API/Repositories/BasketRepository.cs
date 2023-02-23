using Basket.API.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Basket.API.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDistributedCache _redisCache;

        public BasketRepository(IDistributedCache cache)
        {
            _redisCache = cache;
        }

        public async Task<ShoppingCart> GetBasket(string userName)
        {
            var result = await _redisCache.GetStringAsync(userName);
            if (String.IsNullOrEmpty(result)) return null;

            var shoppingCart = JsonConvert.DeserializeObject<ShoppingCart>(result);

            return shoppingCart;
        }

        public async Task<ShoppingCart> UpdateBasket(ShoppingCart shoppingCart)
        {
            var data = JsonConvert.SerializeObject(shoppingCart);
            await _redisCache.SetStringAsync(shoppingCart.UserName , data);
            return await GetBasket(shoppingCart.UserName);
        }

        public async Task DeleteBasket(string userName)
        {
            await _redisCache.RemoveAsync(userName);
        }
    }
}
