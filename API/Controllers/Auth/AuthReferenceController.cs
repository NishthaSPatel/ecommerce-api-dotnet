using Services.IService.Auth;
using Microsoft.AspNetCore.Mvc;
using Services.DTO.ResponseDTO.Auth;
using Services.DTO.RequestDTO.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNet.OData;

#nullable disable

namespace API.Controllers.Auth
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthReferenceController(IAuthReferenceService authReferenceService) : ControllerBase
    {
        private readonly IAuthReferenceService _authReferenceService = authReferenceService;

        #region UserType

        [Tags("UserType")]
        [HttpGet("UserTypes")]
        public List<UserTypeDTO> GetUserTypes([FromQuery] string[] entities)
        {
            return _authReferenceService.GetUserTypes(entities);
        }

        // GET: api/AuthReference/UserType/5
        [Tags("UserType")]
        [HttpGet("UserType/{id}")]
        public UserTypeDTO GetUserType(long id, [FromQuery] string[] entities)
        {
            var userTypeDTO = _authReferenceService.GetUserType(id, entities);
            return userTypeDTO;
        }

        // POST: api/AuthReference/UserType
        [Tags("UserType")]
        [HttpPost("UserType")]
        [Authorize]
        public async Task<ActionResult<UserTypeDTO>> CreateUserType(UserTypeModel userTypeModel)
        {
            return await _authReferenceService.CreateUserType(userTypeModel);
        }

        // PUT: api/AuthReference/UserType/5
        [Tags("UserType")]
        [HttpPut("UserType/{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateUserType(long id, UserTypeModel userTypeModel)
        {
            await _authReferenceService.UpdateUserType(id, userTypeModel);
            return NoContent();
        }

        // DELETE: api/AuthReference/UserType/5
        [Tags("UserType")]
        [HttpDelete("UserType/{id}")]
        [Authorize]
        public async Task<IActionResult> DeactivateUserType(long id)
        {
            await _authReferenceService.DeactivateUserType(id);
            return NoContent();
        }

        #endregion

        #region RoleType

        [Tags("RoleType")]
        [HttpGet("RoleTypes")]
        [EnableQuery]
        public List<RoleTypeDTO> GetRoleTypes([FromQuery] string[] entities)
        {
            return _authReferenceService.GetRoleTypes(entities);
        }

        // GET: api/AuthReference/RoleType/5
        [Tags("RoleType")]
        [HttpGet("RoleType/{id}")]
        public RoleTypeDTO GetRoleType(long id, [FromQuery] string[] entities)
        {
            var roleTypeDTO = _authReferenceService.GetRoleType(id, entities);
            return roleTypeDTO;
        }

        // PUT: api/AuthReference/RoleType/5
        [Tags("RoleType")]
        [HttpPut("RoleType/{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateRoleType(long id, RoleTypeModel roleTypeModel)
        {
            await _authReferenceService.UpdateRoleType(id, roleTypeModel);
            return NoContent();
        }

        // POST: api/AuthReference/RoleType
        [Tags("RoleType")]
        [HttpPost("RoleType")]
        [Authorize]
        public async Task<ActionResult<RoleTypeDTO>> CreateRoleType(RoleTypeModel roleTypeModel)
        {
            return await _authReferenceService.CreateRoleType(roleTypeModel);
        }

        // DELETE: api/AuthReference/RoleType/5
        [Tags("RoleType")]
        [HttpDelete("RoleType/{id}")]
        [Authorize]
        public async Task<IActionResult> DeactivateRoleType(long id)
        {
            await _authReferenceService.DeactivateRoleType(id);
            return NoContent();
        }

        #endregion
    }
}