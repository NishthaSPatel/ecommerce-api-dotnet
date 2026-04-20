using System.Data.SqlTypes;
using AutoMapper;
using DataAccess.Models.Auth;
using DataAccess.Repositories.IRepository.Auth;
using Services.IService.Auth;
using Services.DTO.ResponseDTO.Auth;
using Services.DTO.RequestDTO.Auth;
using Stripe;
using Services.DTO.RequestDTO.Stripe;
using DataAccess.Repositories.IRepository.Stripe;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using DataAccess.Helper;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Identity;

#nullable disable

namespace Services.Service.Auth
{
    public class AuthService(IMapper mapper, IAuthRepository authRepository, IAuthReferenceRepository authReferenceRepository, IStripeRepository stripeRepository, HttpClient client, IConfiguration configuration) : IAuthService
    {
        private readonly IMapper _mapper = mapper;
        private readonly IAuthRepository _authRepository = authRepository;
        private readonly IAuthReferenceRepository _authReferenceRepository = authReferenceRepository;
        private readonly IStripeRepository _stripeRepository = stripeRepository;
        private readonly HttpClient _client = client;
        private readonly IConfiguration _configuration = configuration;

        #region User

        public List<UserDTO> GetUsers(string[] entities)
        {
            var user = _authRepository.GetAll<User>(x => !x.IsDeleted, entities);
            var UserList = _mapper.Map<List<UserDTO>>(user);

            return UserList;
        }

        public UserDTO GetUser(long id, string[] entities)
        {
            var user = _authRepository.GetAsync<User>(id, entities).FirstOrDefault(x => !x.IsDeleted);
            var mappedUser = _mapper.Map<UserDTO>(user);

            if (user != null)
                return mappedUser;
            else
                return null;
        }

        public async Task<UserDTO> GetCurrentUser(string ssoIdentifier)
        {
            var user = await _authRepository.GetCurrentUser(ssoIdentifier);
            return _mapper.Map<UserDTO>(user);
        }

        public async Task<UserDTO> CreateUser(UserModel userModel)
        {
            var authRole = await _authReferenceRepository.GetRoleTypeByName(userModel.Role ?? string.Empty);
            var user = await _authRepository.GetUserBySSOIdentifier(userModel.SsoIdentifier);
            var userDTO = _mapper.Map<UserDTO>(user);
            if (user == null)
            {
                var index = userModel.SsoIdentifier.IndexOf('|');
                var socialLoginName = userModel.SsoIdentifier.Substring(0, index);
                if (socialLoginName == "google-oauth2")
                    userModel.IsGoogleLogin = true;
                else if (socialLoginName == "facebook")
                    userModel.IsFacebookLogin = true;
                else if (socialLoginName == "apple")
                    userModel.IsAppleLogin = true;
                else if (socialLoginName == "windowslive")
                    userModel.IsMicrosoftLogin = true;

                var userMapper = _mapper.Map<User>(userModel);
                user = await _authRepository.CreateAsync(userMapper) ?? throw new SqlTypeException();

                string[] entities = [];
                var customer = _stripeRepository.GetAsync<DataAccess.Models.Stripe.Customer>(user.Id, entities).FirstOrDefault(x => !x.IsDeleted);
                if (customer == null)
                {
                    var options = new CustomerCreateOptions
                    {
                        Name = $"{userModel.Name}",
                        Description = $"Customer {userModel.Description}",
                        Metadata = new Dictionary<string, string>
                        {
                            { "userId", user.Id.ToString() },
                        },
                        Email = userModel.Email
                    };

                    var stripeService = new CustomerService();
                    var stripeCustomer = await stripeService.CreateAsync(options, requestOptions: null);

                    var customerModel = new CustomerModel
                    {
                        Id = user.Id,
                        StripeCustomerId = stripeCustomer.Id
                    };

                    var customerMapper = _mapper.Map<DataAccess.Models.Stripe.Customer>(customerModel);
                    await _authRepository.CreateAsync(customerMapper);

                    if (authRole != null)
                    {
                        RoleModel roleModel = new()
                        {
                            RoleTypeId = authRole.Id,
                            UserId = user.Id
                        };

                        await CreateRole(roleModel);
                    }
                }
            }
            else
            {
                if (authRole != null)
                {
                    var response = user.Roles.Any(x => x.RoleType.Name.Equals(userModel.Role, StringComparison.CurrentCultureIgnoreCase));
                    if (!response)
                    {
                        RoleModel roleModel = new()
                        {
                            RoleTypeId = authRole.Id,
                            UserId = user.Id
                        };

                        await CreateRole(roleModel);
                    }
                }
            }

            UserDTO newUserDTO = new()
            {
                Id = user.Id,
                Name = user.Name,
                Description = user.Description,
                UserTypeId = user.UserTypeId,
                SsoIdentifier = user.SsoIdentifier,
                IsGoogleLogin = user.IsGoogleLogin,
                IsFacebookLogin = user.IsFacebookLogin,
                IsAppleLogin = user.IsAppleLogin,
                IsMicrosoftLogin = user.IsMicrosoftLogin,
                IsDeleted = user.IsDeleted
            };

            return newUserDTO;
        }

        public async Task<bool> UpdateUser(long id, UserModel userModel)
        {
            if (id != userModel.Id)
                throw new InvalidDataException();

            var User = _mapper.Map<User>(userModel);
            return await _authRepository.UpdateAsync(id, User);
        }

        public async Task<bool> DeactivateUser(long id)
        {
            if (id <= 0)
                throw new InvalidDataException();

            return await _authRepository.DeactivateUser(id);
        }

        #endregion

        #region Role

        public List<RoleDTO> GetRoles(string[] entities)
        {
            var user = _authRepository.GetAll<Role>(x => !x.IsDeleted, entities);
            var roleList = _mapper.Map<List<RoleDTO>>(user);

            return roleList;
        }

        public RoleDTO GetRole(long id, string[] entities)
        {
            var user = _authRepository.GetAsync<Role>(id, entities).FirstOrDefault(x => !x.IsDeleted);
            if (user != null)
                return _mapper.Map<RoleDTO>(user);
            else
                return null;
        }

        public async Task<RoleDTO> CreateRole(RoleModel roleModel)
        {
            var roleMapper = _mapper.Map<Role>(roleModel);

            var existUserRole = await _authRepository.GetRoleByUserId(roleModel.UserId, roleModel.RoleTypeId);
            if (existUserRole == null)
            {
                var authRoleID = _authRepository.GetAsync<RoleType>(roleModel.RoleTypeId).FirstOrDefault(x => !x.IsDeleted && x.AuthRoleId != null).AuthRoleId;

                var userExists = _authRepository.GetAsync<User>(roleModel.UserId).FirstOrDefault(x => !x.IsDeleted);
                if (userExists != null)
                {
                    var roles = new[] { authRoleID };

                    var newRole = new AssignRoleParam
                    {
                        roles = roles
                    };

                    var newJson = JsonConvert.SerializeObject(newRole);

                    _client.DefaultRequestHeaders.Authorization
                        = new AuthenticationHeaderValue("Bearer", await AuthToken.GetToken(_configuration, _client));
                    var postResponse = await _client.PostAsync($"https://{_configuration["Auth0:Domain"]}/api/v2/users/{userExists.SsoIdentifier}/roles",
                                                new StringContent(newJson, Encoding.UTF8, "application/json"));

                    var user = await _authRepository.CreateAsync(roleMapper) ?? throw new SqlTypeException();

                    var roleDTO = new RoleDTO()
                    {
                        Id = user.Id,
                        RoleTypeId = user.RoleTypeId,
                        UserId = user.UserId,
                        IsDeleted = user.IsDeleted
                    };

                    return roleDTO;
                }
            }

            return new RoleDTO();
        }

        public async Task<RoleDTO> UpdateRole(long id, RoleModel roleModel)
        {
            var role = _mapper.Map<Role>(roleModel);

            var entities = new[] { "RoleType" };
            var existingRoleList = _authRepository.GetAll<Role>(x => !x.IsDeleted, entities);

            if (existingRoleList.Count != 0)
            {
                var roleAlreadyGiven = existingRoleList.Where(x => x.RoleTypeId == role.RoleTypeId && x.UserId == role.UserId).FirstOrDefault();
                if (roleAlreadyGiven != null)
                    throw new Exception("Can not assign same role to same user.");

                var oldRoleType = existingRoleList.Where(x => x.Id == id && !x.IsDeleted && x.RoleType.AuthRoleId != null).FirstOrDefault().RoleTypeId;

                var roleExists = _authRepository.GetAsync<RoleType>(oldRoleType).FirstOrDefault(x => !x.IsDeleted);

                if (roleExists != null)
                {
                    var userExists = _authRepository.GetAsync<User>(role.UserId).FirstOrDefault(x => !x.IsDeleted);
                    if (userExists != null)
                    {
                        _client.DefaultRequestHeaders.Authorization
                                = new AuthenticationHeaderValue("Bearer", await AuthToken.GetToken(_configuration, _client));

                        //Delete assignment of role.
                        var oldRole = new[] { roleExists.AuthRoleId };
                        var oldRoles = new AssignRoleParam
                        {
                            roles = oldRole
                        };

                        var oldJson = JsonConvert.SerializeObject(oldRoles);
                        var httpMessage = new HttpRequestMessage(HttpMethod.Delete, $"https://{_configuration["Auth0:Domain"]}/api/v2/users/{userExists.SsoIdentifier}/roles")
                        {
                            Content = new StringContent(oldJson, Encoding.UTF8, "application/json")
                        };
                        var result = await _client.SendAsync(httpMessage);

                        //Add assignment of role.
                        var newRoleType = _authRepository.GetAsync<RoleType>(role.RoleTypeId).FirstOrDefault(x => !x.IsDeleted && x.AuthRoleId != null).AuthRoleId;
                        var newRole = new[] { newRoleType };
                        var newRoles = new AssignRoleParam
                        {
                            roles = newRole
                        };

                        var newJson = JsonConvert.SerializeObject(newRoles);
                        var postResponse = await _client.PostAsync($"https://{_configuration["Auth0:Domain"]}/api/v2/users/{userExists.SsoIdentifier}/roles", new StringContent(newJson, Encoding.UTF8, "application/json"));
                    }
                }
            }
            await _authRepository.UpdateAsync(id, role);
            var roleDTO = _mapper.Map<RoleDTO>(role);
            return roleDTO;
        }

        public async Task<bool> DeactivateRole(long id)
        {
            if (id <= 0)
                throw new InvalidDataException();

            var role = _authRepository.GetAsync<Role>(id).FirstOrDefault(x => !x.IsDeleted);

            if (role != null)
            {
                var auth0RoleId = _authRepository.GetAsync<RoleType>(role.RoleTypeId).FirstOrDefault(x => !x.IsDeleted && x.AuthRoleId != null).AuthRoleId;

                var oldRole = new[] { auth0RoleId };
                var oldRoles = new AssignRoleParam
                {
                    roles = oldRole
                };
                var oldJson = JsonConvert.SerializeObject(oldRoles);
                var userExists = _authRepository.GetAsync<User>(role.UserId).FirstOrDefault(x => !x.IsDeleted);

                _client.DefaultRequestHeaders.Authorization
                                = new AuthenticationHeaderValue("Bearer", await AuthToken.GetToken(_configuration, _client));
                var httpMessage = new HttpRequestMessage(HttpMethod.Delete, $"https://{_configuration["Auth0:Domain"]}/api/v2/users/{userExists.SsoIdentifier}/roles")
                {
                    Content = new StringContent(oldJson, Encoding.UTF8, "application/json")
                };
                var result = await _client.SendAsync(httpMessage);

                await _authRepository.DeactivateAsync<Role>(id);
                return true;
            }
            return false;
        }

        #endregion
    }
}