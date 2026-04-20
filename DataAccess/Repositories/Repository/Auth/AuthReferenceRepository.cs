using DataAccess.Context;
using DataAccess.Models.Auth;
using DataAccess.Repositories.IRepository.Auth;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DataAccess.Repositories.Repository.Auth
{
    public class AuthReferenceRepository(CoreFunctionalityContext context) : RepositoryBase(context), IAuthReferenceRepository
    {
        #region UserType

        public async Task<bool> DeactivateUserType(long id)
        {
            var userType = await _context.UserTypes.FirstOrDefaultAsync(x => x.Id == id);
            if (userType != null)
            {
                userType.IsDeleted = true;
                _context.Entry(userType).State = EntityState.Modified;

                var userList = await _context.Users.Where(x => x.UserTypeId == id).ToListAsync();

                foreach (var user in userList)
                {
                    user.IsDeleted = true;
                }
            }
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<long> GetUserTypeByName(string name)
        {
            return await _context.UserTypes.Where(x => x.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase) && !x.IsDeleted).Select(x => x.Id).FirstOrDefaultAsync();
        }

        #endregion

        #region RoleType

        public async Task<bool> DeactivateRoleType(long id)
        {
            var roleType = await _context.RoleTypes.Include(x => x.Roles).FirstOrDefaultAsync(x => x.Id == id);
            if (roleType != null)
            {
                roleType.IsDeleted = true;
                _context.Entry(roleType).State = EntityState.Modified;

                var roleList = roleType.Roles.Where(x => x.RoleTypeId == id && !x.IsDeleted).ToList();
                foreach (var role in roleList)
                {
                    role.IsDeleted = true;
                }
            }
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<RoleType> GetRoleTypeByName(string name)
        {
            return await _context.RoleTypes.FirstOrDefaultAsync(x => x.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase) && x.AuthRoleId != null && !x.IsDeleted) ?? null;
        }

        #endregion
    }
}