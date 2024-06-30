using CryptoRate.Common.Events;
using CryptoRate.Common.Models;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace CryptoRate.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : Controller
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public UsersController(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] User user)
        {
            var addUserEvent = new AddUserEvent
            {
                Email = user.Email,
                Name = user.Name,
                Password = user.Password
            };
            await _publishEndpoint.Publish(addUserEvent); 
            return Accepted();
        }
    }
}
