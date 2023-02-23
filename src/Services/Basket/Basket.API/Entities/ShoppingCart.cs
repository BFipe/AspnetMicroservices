namespace Basket.API.Entities
{
    public class ShoppingCart
    {
        public ShoppingCart(string userName)
        {
            if (String.IsNullOrWhiteSpace(userName)) throw new Exception("Null user name");
            UserName = userName;
        }

        public string UserName { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; } = new();

        public decimal TotalPrice()
        {
            return ShoppingCartItems.Sum(q => q.Price * q.Quantity);
        }
    }
}
