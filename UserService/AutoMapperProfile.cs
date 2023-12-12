using AutoMapper;
using UserService.DTOs;
using UserService.Models;

namespace UserService
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() {
            CreateMap<UpdateUserRequest, User>();
            CreateMap<User, UpdateUserRequest>();
            CreateMap<RegisterRequest, User>();
            CreateMap<User, RegisterRequest>();

            CreateMap<UserResponse, User>();
            CreateMap<User, UserResponse>();

            CreateMap<RoleResponse, Role>();
            CreateMap<Role, RoleResponse>();
            CreateMap<CreateRoleRequest, Role>();
            CreateMap<Role, CreateRoleRequest>();
            CreateMap<UpdateRoleRequest, Role>();
            CreateMap<Role, UpdateRoleRequest>();

        }
    }
}
