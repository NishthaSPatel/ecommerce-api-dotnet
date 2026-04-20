using System.Data.SqlTypes;
using AutoMapper;
using DataAccess.Models.Auth;
using DataAccess.Repositories.IRepository.Auth;
using Services.IService.Auth;
using Services.DTO.ResponseDTO.Auth;
using Services.DTO.RequestDTO.Auth;
using System.Net.Http.Headers;
using DataAccess.Helper;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;

#nullable disable

namespace Services.Service.Auth
{
    public class AuthReferenceService(IMapper mapper, IAuthReferenceRepository authReferenceRepository, HttpClient client, IConfiguration configuration) : IAuthReferenceService
    {
        private readonly IMapper _mapper = mapper;
        private readonly IAuthReferenceRepository _authReferenceRepository = authReferenceRepository;
        private readonly HttpClient _client = client;
        private readonly IConfiguration _configuration = configuration;

        #region UserType

        public List<UserTypeDTO> GetUserTypes(string[] entities)
        {
            var response = _authReferenceRepository.GetAll<UserType>(x => !x.IsDeleted, entities);
            var UserTypeList = _mapper.Map<List<UserTypeDTO>>(response);

            return UserTypeList;
        }

        public UserTypeDTO GetUserType(long id, string[] entities)
        {
            var response = _authReferenceRepository.GetAsync<UserType>(id, entities).FirstOrDefault(x => !x.IsDeleted);
            if (response != null)
                return _mapper.Map<UserTypeDTO>(response);
            else
                return null;
        }

        public async Task<UserTypeDTO> CreateUserType(UserTypeModel UserTypeModel)
        {
            var UserTypeMapper = _mapper.Map<UserType>(UserTypeModel);
            var response = await _authReferenceRepository.CreateAsync(UserTypeMapper) ?? throw new SqlTypeException();
            var UserTypeDTO = _mapper.Map<UserTypeDTO>(response);

            return UserTypeDTO;
        }

        public async Task<bool> UpdateUserType(long id, UserTypeModel UserTypeModel)
        {
            if (id != UserTypeModel.Id)
                throw new InvalidDataException();

            var UserType = _mapper.Map<UserType>(UserTypeModel);
            return await _authReferenceRepository.UpdateAsync(id, UserType);
        }

        public async Task<bool> DeactivateUserType(long id)
        {
            if (id <= 0)
                throw new InvalidDataException();

            return await _authReferenceRepository.DeactivateUserType(id);
        }

        #endregion

        #region RoleType

        public List<RoleTypeDTO> GetRoleTypes(string[] entities)
        {
            var response = _authReferenceRepository.GetAll<RoleType>(x => !x.IsDeleted, entities);
            var roleTypeList = _mapper.Map<List<RoleTypeDTO>>(response);

            return roleTypeList;
        }

        public RoleTypeDTO GetRoleType(long id, string[] entities)
        {
            var response = _authReferenceRepository.GetAsync<RoleType>(id, entities).FirstOrDefault(x => !x.IsDeleted);
            if (response != null)
                return _mapper.Map<RoleTypeDTO>(response);
            else
                return null;
        }

        public async Task<RoleTypeDTO> CreateRoleType(RoleTypeModel roleTypeModel)
        {
            RoleTypeDTO roleTypeDTO = new();
            var authRole = new CreateRoleParam
            {
                name = roleTypeModel.Name,
                description = roleTypeModel.Description
            };

            _client.DefaultRequestHeaders.Authorization
                        = new AuthenticationHeaderValue("Bearer", await AuthToken.GetToken(_configuration, _client));

            var roles = await _client.GetAsync($"https://{_configuration["Auth0:Domain"]}/api/v2/roles");
            roles.EnsureSuccessStatusCode();
            var roleContent = await roles.Content.ReadAsStringAsync();

            dynamic rolesData = Newtonsoft.Json.JsonConvert.DeserializeObject(roleContent);

            bool isExistsAuthRoleType = false;
            string authRoleId = null;

            foreach (var roleData in rolesData)
            {
                if (roleData.name == roleTypeModel.Name)
                {
                    authRoleId = roleData.id;
                    isExistsAuthRoleType = true;
                }
            }

            var roleTypes = _authReferenceRepository.GetAll<RoleType>(x => !x.IsDeleted, null);

            var isExistsRoleType = roleTypes.Where(x => x.Name == roleTypeModel.Name && x.IsDeleted == false).FirstOrDefault();
            if (isExistsAuthRoleType == false && isExistsRoleType == null)
            {
                var json = JsonConvert.SerializeObject(authRole);

                _client.DefaultRequestHeaders.Authorization
                        = new AuthenticationHeaderValue("Bearer", await AuthToken.GetToken(_configuration, _client));
                var postResponse = await _client.PostAsync($"https://{_configuration["Auth0:Domain"]}/api/v2/roles", new StringContent(json, Encoding.UTF8, "application/json"));

                JObject responseJson = JObject.Parse(postResponse.Content.ReadAsStringAsync().Result);

                var authRoleID = responseJson.GetValue("id").ToString();

                roleTypeModel.AuthRoleId = authRoleID;
            }
            else if (isExistsAuthRoleType == true && isExistsRoleType == null)
            {
                roleTypeModel.AuthRoleId = authRoleId;
            }
            else
            {
                return roleTypeDTO;
            }

            if (roleTypeModel.AuthRoleId != null)
            {
                var roleTypeMapper = _mapper.Map<RoleType>(roleTypeModel);
                var response = await _authReferenceRepository.CreateAsync(roleTypeMapper) ?? throw new SqlTypeException();
                roleTypeDTO = _mapper.Map<RoleTypeDTO>(response);
            }

            return roleTypeDTO;
        }

        public async Task<bool> UpdateRoleType(long id, RoleTypeModel roleTypeModel)
        {
            if (id != roleTypeModel.Id)
                throw new InvalidDataException();

            var existing = _authReferenceRepository.GetAsync<RoleType>(id).FirstOrDefault(x => !x.IsDeleted);
            var authRoleID = existing.AuthRoleId;
            roleTypeModel.AuthRoleId = authRoleID;

            var authRole = new CreateRoleParam
            {
                name = roleTypeModel.Name,
                description = roleTypeModel.Description
            };
            var json = JsonConvert.SerializeObject(authRole);
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await AuthToken.GetToken(_configuration, _client));
            await _client.PatchAsync($"https://{_configuration["Auth0:Domain"]}/api/v2/roles/{authRoleID}", new StringContent(json, Encoding.UTF8, "application/json"));

            var roleType = _mapper.Map<RoleType>(roleTypeModel);
            return await _authReferenceRepository.UpdateAsync(id, roleType);
        }

        public async Task<bool> DeactivateRoleType(long id)
        {
            if (id <= 0)
                throw new InvalidDataException();

            var roleType = _authReferenceRepository.GetAsync<RoleType>(id).FirstOrDefault(x => !x.IsDeleted);

            _client.DefaultRequestHeaders.Authorization
                        = new AuthenticationHeaderValue("Bearer", await AuthToken.GetToken(_configuration, _client));
            await _client.DeleteAsync($"https://{_configuration["Auth0:Domain"]}/api/v2/roles/{roleType.AuthRoleId}");

            return await _authReferenceRepository.DeactivateRoleType(id);
        }

        #endregion
    }
}