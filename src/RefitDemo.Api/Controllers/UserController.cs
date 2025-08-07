using Microsoft.AspNetCore.Mvc;
using RefitDemo.Api.Models;

namespace RefitDemo.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private static readonly List<UserDto> Users = new()
        {
            new UserDto { Id = 1, Username = "alice" },
            new UserDto { Id = 2, Username = "bob" }
        };

        [HttpGet]
        public IEnumerable<UserDto> Get() => Users;

        [HttpPost]
        public IActionResult Register(UserDto user)
        {
            user.Id = Users.Count + 1;
            Users.Add(user);
            return CreatedAtAction(nameof(Get), new { id = user.Id }, user);
        }
    }
}
