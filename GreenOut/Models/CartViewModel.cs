namespace GreenOut.Models
{
    public class CartViewModel
    {
        
        public ShoppingCart Cart { get; set; }
        public IEnumerable<CartItem> CartItems { get; set; }
    }
}
