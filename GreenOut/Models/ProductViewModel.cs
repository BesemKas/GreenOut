using GreenOut.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

public class ProductViewModel
{
    public Product Product { get; set; }
    public IEnumerable<SelectListItem>? Categories { get; set; }

}