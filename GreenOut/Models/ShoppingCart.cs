namespace GreenOut.Models
{
    public class ShoppingCart
    {
        public int CartID { get; set; }

        public int AccountID { get; set; }
        public Account Account { get; set; }
    }
}
