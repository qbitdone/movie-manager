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

            CreateMap<DirectorRequest, Director>();
            CreateMap<Director, DirectorResponse>();

            CreateMap<GenreRequest, Genre>();
            CreateMap<Genre, GenreResponse>();

            CreateMap<MovieRequest, Movie>();
            CreateMap<Movie, MovieResponse>();

            CreateMap<ActorRequest, Actor>();
            CreateMap<Actor, ActorResponse>();
        }
    }
}
