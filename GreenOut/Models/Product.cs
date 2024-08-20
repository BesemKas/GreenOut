using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GreenOut.Models
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }
        public string Name { get; set; }

        public decimal Price { get; set; }
        public string Description { get; set; }
        public string ImageURL { get; set; }

        public int Stock { get; set; }

        public int CategoryID { get; set; }

      
        public Category Category { get; set; }
    }
}
