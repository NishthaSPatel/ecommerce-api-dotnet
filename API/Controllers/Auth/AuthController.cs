using Services.IService.Auth;
using Microsoft.AspNetCore.Mvc;
using Services.DTO.ResponseDTO.Auth;
using Services.DTO.RequestDTO.Auth;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json.Linq;

#nullable disable

namespace API.Controllers.Auth
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(IAuthService authService, IConfiguration configuration) : ControllerBase
    {
        private readonly IAuthService _authService = authService;
        private readonly IConfiguration _configuration = configuration;

        #region User

        [Tags("User")]
        [HttpGet("Users")]
        public List<UserDTO> GetUsers([FromQuery] string[] entities)
        {
            return _authService.GetUsers(entities);
        }

        // PUT: api/AuthReference/User/5
        [Tags("User")]
        [HttpPut("User/{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateUser(long id, UserModel userModel)
        {
            await _authService.UpdateUser(id, userModel);
            return NoContent();
        }

        // POST: api/AuthReference/User
        [Tags("User")]
        [HttpPost("User")]
        public async Task<ActionResult<UserDTO>> CreateUser(UserModel userModel)
        {
            return await _authService.CreateUser(userModel);
        }

        [Tags("User")]
        [HttpPost("JSONR")]
        public JObject CreateJSONR()
        {
            JObject levelSwitches = _configuration.GetSection("Logging").Get<JObject>();
            return levelSwitches;
        }

        // DELETE: api/AuthReference/User/5
        [Tags("User")]
        [HttpDelete("User/{id}")]
        [Authorize]
        public async Task<IActionResult> DeactivateUser(long id)
        {
            await _authService.DeactivateUser(id);
            return NoContent();
        }

        [Tags("User")]
        [HttpGet("GetCurrentUser")]
        [Authorize]
        public async Task<IActionResult> GetCurrentUser()
        {
            var ssoIdentifier = User.Claims.FirstOrDefault(x => x.Properties.FirstOrDefault().Value == "sub")?.Value;

            if (string.IsNullOrEmpty(ssoIdentifier))
                return BadRequest("SSO identifier not found in claims.");

            var user = await _authService.GetCurrentUser(ssoIdentifier);

            return Ok(user);
        }

        #endregion
    }
}