using AutoMapper;
using relivnet.domain.entities;
using relivnet.infraestructure.application.models;


namespace relivnet.infraestructure.application.profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserModel, UserEntity>()
                .ReverseMap();

            CreateMap<UserModel, UserEntity>()
                .ReverseMap();
        }
    }
}

