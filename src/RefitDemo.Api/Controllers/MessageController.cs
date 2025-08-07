using Microsoft.AspNetCore.Mvc;
using RefitDemo.Api.Models;

namespace RefitDemo.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MessageController : ControllerBase
    {
        private static readonly List<MessageDto> Messages = new();

        [HttpGet]
        public IEnumerable<MessageDto> Get() => Messages;

        [HttpPost]
        public IActionResult Send(MessageDto message)
        {
            message.Id = Messages.Count + 1;
            message.SentAt = DateTime.UtcNow;
            Messages.Add(message);
            return CreatedAtAction(nameof(Get), new { id = message.Id }, message);
        }
    }
}
