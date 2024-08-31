using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiUsers.Domain.Dtos;
using WebApiUsers.Domain.Models;

namespace WebApiUsers.Domain
{
    public class MapperConfig
    {
        public static Mapper InitializeAutomapper()
        {
            var config = new MapperConfiguration(cfg =>
            {                
                cfg.CreateMap<User, CreateUserDto>();
                cfg.CreateMap<User, UpdateUserDto>();
            });

            var mapper = new Mapper(config);
            return mapper;

        }
    }
}
