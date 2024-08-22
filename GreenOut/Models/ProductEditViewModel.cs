using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GreenOut.Models
{
    public class ProductEditViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public decimal Price { get; set; }
        public string Description { get; set; }
        public string ImageURL { get; set; }

        public int Stock { get; set; }

        public int CategoryID { get; set; }
        public Category? Category { get; set; }

      
    }
}
