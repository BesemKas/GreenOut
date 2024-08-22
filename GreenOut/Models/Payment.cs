namespace GreenOut.Models
{
    public class Payment
    {
        public int PaymentID { get; set; }

        public int OrderID { get; set; }
        public Order Order { get; set; }

        public string AccountID { get; set; }
        public Account Account { get; set; }

        public decimal Total { get; set; }

        public DateTime PaymentDate { get; set; }
    }
}
