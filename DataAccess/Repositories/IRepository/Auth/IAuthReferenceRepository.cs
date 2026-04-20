
using DataAccess.Models.Auth;

namespace DataAccess.Repositories.IRepository.Auth
{
    public interface IAuthReferenceRepository : IRepositoryBase
    {
        #region UserType

        Task<long> GetUserTypeByName(string name);
        Task<bool> DeactivateUserType(long id);

        #endregion

        #region RoleType

        Task<RoleType> GetRoleTypeByName(string name);
        Task<bool> DeactivateRoleType(long id);

        #endregion
    }
}