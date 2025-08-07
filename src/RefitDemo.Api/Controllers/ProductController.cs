using Microsoft.AspNetCore.Mvc;
using RefitDemo.Api.Models;

namespace RefitDemo.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private static readonly List<ProductDto> Products = new()
        {
            new ProductDto { Id = 1, Name = "Laptop", Price = 999.99M },
            new ProductDto { Id = 2, Name = "Phone", Price = 499.99M }
        };

        [HttpGet]
        public IEnumerable<ProductDto> Get() => Products;

        [HttpPost]
        public IActionResult Add(ProductDto product)
        {
            product.Id = Products.Count + 1;
            Products.Add(product);
            return CreatedAtAction(nameof(Get), new { id = product.Id }, product);
        }
    }
}
