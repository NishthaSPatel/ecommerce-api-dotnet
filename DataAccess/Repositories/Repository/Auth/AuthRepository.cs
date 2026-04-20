using DataAccess.Context;
using DataAccess.Repositories.IRepository.Auth;
using Microsoft.EntityFrameworkCore;
using DataAccess.Models.Auth;

#nullable disable

namespace DataAccess.Repositories.Repository.Auth
{
    public class AuthRepository(CoreFunctionalityContext context) : RepositoryBase(context), IAuthRepository
    {
        #region User

        public async Task<User> GetUserBySSOIdentifier(string ssoIdentifier)
        {
            return await _context.Users.Include(x => x.Roles).ThenInclude(x => x.RoleType).FirstOrDefaultAsync(x => x.SsoIdentifier == ssoIdentifier && !x.IsDeleted);
        }

        public async Task<User> GetCurrentUser(string ssoIdentifier)
        {
            return await _context.Users.Where(x => x.SsoIdentifier == ssoIdentifier && !x.IsDeleted).FirstOrDefaultAsync() ?? throw new NullReferenceException("User Not Found.");
        }

        public async Task<User> CreateUser(User user)
        {
            var existingUser = await _context.Users.Where(x => !x.IsDeleted && x.SsoIdentifier == user.SsoIdentifier).FirstOrDefaultAsync();

            if (existingUser == null)
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return user;
            }

            return existingUser;
        }

        public async Task<bool> DeactivateUser(long id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user != null)
            {
                user.IsDeleted = true;
                _context.Entry(user).State = EntityState.Modified;
            }
            await _context.SaveChangesAsync();

            return true;
        }

        #endregion

        #region Role

        public async Task<Role> GetRoleByUserId(long userId, long roleTypeId)
        {
            return await _context.Roles.FirstOrDefaultAsync(x => x.UserId == userId && x.RoleTypeId == roleTypeId && !x.IsDeleted);
        }

        #endregion
    }
}