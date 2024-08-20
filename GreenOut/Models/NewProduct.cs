using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace GreenOut.Models
{
    public class NewProduct
    {
        [Key]
        public int id { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }
        public Product Product { get; set; }
    }
}
