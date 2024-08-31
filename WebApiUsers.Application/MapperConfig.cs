using AutoMapper;
using WebApiUsers.Domain.Dtos;
using WebApiUsers.Domain.Models;

namespace WebApiUsers.Application
{
    public class MapperConfig
    {
        public static Mapper InitializeAutomapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, CreateUserDto>().ReverseMap();
                cfg.CreateMap<User, UpdateUserDto>().ReverseMap();
                cfg.CreateMap<User, UserInformationDto>().ReverseMap();
                cfg.CreateMap<UserInformationDto, UpdateUserDto>().ReverseMap();
                cfg.CreateMap<UserInformationDto, CreateUserDto>().ReverseMap();

            });

            var mapper = new Mapper(config);
            return mapper;

        }
    }
}
