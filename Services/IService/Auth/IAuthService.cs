using Services.DTO.RequestDTO.Auth;
using Services.DTO.ResponseDTO.Auth;

namespace Services.IService.Auth
{
    public interface IAuthService
    {
        #region User

        List<UserDTO> GetUsers(string[] entities);
        UserDTO GetUser(long id, string[] entities);
        Task<UserDTO> GetCurrentUser(string ssoIdentifier);
        Task<UserDTO> CreateUser(UserModel userModel);
        Task<bool> UpdateUser(long id, UserModel userModel);
        Task<bool> DeactivateUser(long id);

        #endregion

        #region Role

        List<RoleDTO> GetRoles(string[] entities);
        RoleDTO GetRole(long id, string[] entities);
        Task<RoleDTO> CreateRole(RoleModel roleModel);
        Task<RoleDTO> UpdateRole(long id, RoleModel roleModel);
        Task<bool> DeactivateRole(long id);

        #endregion
    }
}