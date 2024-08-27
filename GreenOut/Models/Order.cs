using System.ComponentModel.DataAnnotations.Schema;

namespace GreenOut.Models
{
    public class Order
    {
        public int OrderID { get; set; }
        public DateTime OrderDate { get; set; }

        [ForeignKey(nameof(Account))]
        public string AccountID { get; set; }
        public Account Account { get; set; }
    }
}
