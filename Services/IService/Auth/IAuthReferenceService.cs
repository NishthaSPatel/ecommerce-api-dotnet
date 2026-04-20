using Services.DTO.RequestDTO.Auth;
using Services.DTO.ResponseDTO.Auth;

namespace Services.IService.Auth
{
    public interface IAuthReferenceService
    {
        #region UserType

        List<UserTypeDTO> GetUserTypes(string[] entities);
        UserTypeDTO GetUserType(long id, string[] entities);
        Task<UserTypeDTO> CreateUserType(UserTypeModel userTypeModel);
        Task<bool> UpdateUserType(long id, UserTypeModel userTypeModel);
        Task<bool> DeactivateUserType(long id);

        #endregion

        #region RoleType

        List<RoleTypeDTO> GetRoleTypes(string[] entities);
        RoleTypeDTO GetRoleType(long id, string[] entities);
        Task<RoleTypeDTO> CreateRoleType(RoleTypeModel roleTypeModel);
        Task<bool> UpdateRoleType(long id, RoleTypeModel roleTypeModel);
        Task<bool> DeactivateRoleType(long id);

        #endregion
    }
}