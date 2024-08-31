using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiUsers.Domain.Dtos;
using WebApiUsers.Domain.Models;

namespace WebApiUsers.Application.Ports.Secundary
{
    public interface IUserInformationSecundaryPort
    {
        Task<int> CreateUser(User user);
        Task<(List<User>,int)> GetUsers(SearchDto search);
        Task<User> GetUserById(int id);
        Task<User> UpdateUser(User user);
        Task DeleteUser(int id);
    }
}
