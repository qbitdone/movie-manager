using AutoMapper;
using movie_manager.Models;

namespace movie_manager.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            CreateMap<UserRequest, User>();
            CreateMap<User, UserResponse>();
        }
    }
}
