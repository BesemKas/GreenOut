using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GreenOut.Models
{
    public class OrderItem
    {
        public int OrderItemID { get; set; }

        public int ProductID { get; set; }
        public Product Product { get; set; } //for productID

        [ForeignKey(nameof(Order))]
        public int OrderID { get; set; }
        public Order? Order { get; set; } //for the cartID

        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0")]
        public int Quantity { get; set; }
    }
}
