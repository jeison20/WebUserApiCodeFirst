using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiUsers.Domain.Dtos;

namespace WebApiUsers.Application.Ports.Primary
{
    public interface IUserInformationPrimaryPort
    {
        Task<ResponseDto<UserInformationDto>> HandleGetUserByIdAsync(int id);
        Task<ResponseDto<List<UserInformationDto>>> HandleGetUsersAsync(SearchDto search);
        Task<ResponseDto<UserInformationDto>> HandleAddUserAsync(CreateUserDto user);
        Task<ResponseDto<UserInformationDto>> HandleUpdateUserAsync(UpdateUserDto user);
        Task<ResponseDto<UserInformationDto>> HandleDeleteUserAsync(int id);
    }
}
