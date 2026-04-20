using DataAccess.Models.Auth;

namespace DataAccess.Repositories.IRepository.Auth
{
    public interface IAuthRepository : IRepositoryBase
    {
        #region User

        Task<bool> DeactivateUser(long id);
        Task<User> GetCurrentUser(string ssoIdentifier);
        Task<User> GetUserBySSOIdentifier(string ssoIdentifier);
        Task<User> CreateUser(User user);

        #endregion

        #region Role

        Task<Role> GetRoleByUserId(long userId, long roleTypeId);

        #endregion
    }
}