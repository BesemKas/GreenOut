using System.ComponentModel.DataAnnotations;

namespace GreenOut.Models
{
    public class ShoppingCart
    {
        [Key]
        public int CartID { get; set; }

        public string AccountID { get; set; }
        public Account Account { get; set; }
    }
}
