using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Test.Services;

namespace Test.Controllers
{
    [Route("authorization")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly ILogger<AuthorizationController> _logger;
        private readonly DatabaseController _db;

        public AuthorizationController(ILogger<AuthorizationController> logger, DatabaseController db)
        {
            _db = db;
            _logger = logger;
        }

        [HttpGet]
        [Route("login")]
        public async Task<IActionResult> Login(string userId)
        {

            if (!Guid.TryParse(userId, out var userGuid))
            {
                return BadRequest();
            }

            try
            {
                var user = await _db.GetUserByGuid(new Guid(userId));
                if (user == null)
                {
                    return NotFound();
                }
                await _db.SignInUser(user);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Cannot process login: ");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("registration")]
        public async Task<IActionResult> Registration()
        {
            try
            {
                return Ok(await _db.CreateUser());
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Cannot process registration: ");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
