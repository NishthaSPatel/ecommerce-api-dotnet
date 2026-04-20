using AutoMapper;
using DataAccess.Models.Auth;
using Services.DTO.RequestDTO.Auth;
using Services.DTO.ResponseDTO.Auth;

namespace Services.Mapping
{
    public class AuthMapperProfile : Profile
    {
        public AuthMapperProfile()
        {
            #region Auth Reference

            CreateMap<UserType, UserTypeModel>().ReverseMap();
            CreateMap<UserType, UserTypeDTO>().ReverseMap();

            CreateMap<RoleType, RoleTypeModel>().ReverseMap();
            CreateMap<RoleType, RoleTypeDTO>().ReverseMap();

            #endregion

            #region Auth

            CreateMap<User, UserModel>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();

            CreateMap<Role, RoleModel>().ReverseMap();
            CreateMap<Role, RoleDTO>().ReverseMap();

            #endregion
        }
    }
}