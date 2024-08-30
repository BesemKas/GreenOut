namespace GreenOut.Models
{
    public class CheckoutViewModel
    {
        public ShoppingCart Cart { get; set; }
        public IEnumerable<CartItem> CartItems { get; set; }
    }
}
