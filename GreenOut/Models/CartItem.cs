using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GreenOut.Models
{
    public class CartItem
    {
        public int CartItemID { get; set; }

        public int ProductID { get; set; }
        public Product Product { get; set; } //for productID

        [ForeignKey(nameof(ShoppingCart))]
        public int CartID { get; set; }

        public ShoppingCart? ShoppingCart { get; set; }


        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0")]
        public int Quantity { get; set; }
        
    }
}
