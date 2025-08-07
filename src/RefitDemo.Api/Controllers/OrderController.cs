using Microsoft.AspNetCore.Mvc;
using RefitDemo.Api.Models;

namespace RefitDemo.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private static readonly List<OrderDto> Orders = new();

        [HttpGet]
        public IEnumerable<OrderDto> Get() => Orders;

        [HttpPost]
        public IActionResult Create(OrderDto order)
        {
            order.Id = Orders.Count + 1;
            order.CreatedAt = DateTime.UtcNow;
            Orders.Add(order);
            return CreatedAtAction(nameof(Get), new { id = order.Id }, order);
        }
    }
}
