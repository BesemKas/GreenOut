namespace GreenOut.Models
{
    public class Product
    {
        public int PorductID { get; set; }
        public string Name { get; set; }

        public decimal Price { get; set; }
        public string Description { get; set; }

        public int Stock { get; set; }

        public int CategoryID { get; set; }

        public Category Category { get; set; }
    }
}
