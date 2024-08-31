using WebApiUsers.Application.Ports.Primary;
using WebApiUsers.Application.Ports.Secundary;
using WebApiUsers.Domain.Dtos;
using WebApiUsers.Domain.Models;

namespace WebApiUsers.Application.UseCases
{
    public class UserInformationApplication : IUserInformationPrimaryPort
    {
        #region mensajes
        /// <summary>
        /// Estas variables serian manejadas en un recurso el cual contendria el mensaje segun el idioma que se necesite
        /// </summary>
        private readonly string MessageGenericError = "Lo sentimos por favor intente nuevamente mas tarde";
        private readonly string MessageErrorUserNoFound = "Usuario no encontrado";
        private readonly string MessageStateResponseOk = "OK";
        #endregion

        readonly IUserInformationSecundaryPort UserInformationSecundaryPort;
        public UserInformationApplication(IUserInformationSecundaryPort userInformationSecundaryPort)
        {
            UserInformationSecundaryPort = userInformationSecundaryPort;
        }
        public async Task<ResponseDto<UserInformationDto>> HandleAddUserAsync(CreateUserDto userDto)
        {
            try
            {
                var mapper = MapperConfig.InitializeAutomapper();
                User user = mapper.Map<User>(userDto);

                int resultId = await UserInformationSecundaryPort.CreateUser(user);

                UserInformationDto userInformation = mapper.Map<UserInformationDto>(userDto);
                userInformation.Id = resultId;

                return new ResponseDto<UserInformationDto> { Data = userInformation, Message = MessageStateResponseOk, Response = "201" };
            }
            catch (Exception)
            {
                return new ResponseDto<UserInformationDto> { Data = null, Message = MessageGenericError, Response = "500" };
            }
        }

        public async Task<ResponseDto<UserInformationDto>> HandleDeleteUserAsync(int id)
        {
            try
            {

                User user = await UserInformationSecundaryPort.GetUserById(id);
                if (user == null)
                {
                    return new ResponseDto<UserInformationDto> { Data = new UserInformationDto(), Message = MessageErrorUserNoFound, Response = "404" };
                }

                await UserInformationSecundaryPort.DeleteUser(user.Id);
                var mapper = MapperConfig.InitializeAutomapper();
                UserInformationDto userInformation = mapper.Map<UserInformationDto>(user);

                return new ResponseDto<UserInformationDto> { Data = userInformation, Message = MessageStateResponseOk, Response = "200" };
            }
            catch (Exception)
            {
                return new ResponseDto<UserInformationDto> { Data = null, Message = MessageGenericError, Response = "500" };
            }
        }

        public async Task<ResponseDto<UserInformationDto>> HandleGetUserByIdAsync(int id)
        {
            try
            {
                User user = await UserInformationSecundaryPort.GetUserById(id);

                if (user == null)
                {
                    return new ResponseDto<UserInformationDto> { Data = new UserInformationDto(), Message = MessageErrorUserNoFound, Response = "404" };
                }

                var mapper = MapperConfig.InitializeAutomapper();
                UserInformationDto searchUserDto = mapper.Map<UserInformationDto>(user);

                return new ResponseDto<UserInformationDto> { Data = searchUserDto, Message = MessageStateResponseOk, Response = "200" };
            }
            catch (Exception)
            {
                return new ResponseDto<UserInformationDto> { Data = null, Message = MessageGenericError, Response = "500" };
            }
        }

        public async Task<ResponseDto<List<UserInformationDto>>> HandleGetUsersAsync(SearchDto search)
        {
            try
            {
                (List<User> lsUsers, int pageCount) = await UserInformationSecundaryPort.GetUsers(search);

                var mapper = MapperConfig.InitializeAutomapper();
                List<UserInformationDto> lsSearchUsersDto = mapper.Map<List<UserInformationDto>>(lsUsers);

                return new ResponseDto<List<UserInformationDto>> { Data = lsSearchUsersDto, Message = MessageStateResponseOk, Response = "200", PageCount = pageCount };
            }
            catch (Exception)
            {
                return new ResponseDto<List<UserInformationDto>> { Data = null, Message = MessageGenericError, Response = "500" };
            }
        }

        public async Task<ResponseDto<UserInformationDto>> HandleUpdateUserAsync(UpdateUserDto userUpdate)
        {
            try
            {
                var mapper = MapperConfig.InitializeAutomapper();


                User userLastInfo = await UserInformationSecundaryPort.GetUserById(userUpdate.Id);

                if (userLastInfo.FirstName != userUpdate.FirstName)
                    userLastInfo.FirstName = userUpdate.FirstName;

                if (userLastInfo.SecondName != userUpdate.SecondName)
                    userLastInfo.SecondName = userUpdate.SecondName;

                if (userLastInfo.FirstLastName != userUpdate.FirstLastName)
                    userLastInfo.FirstLastName = userUpdate.FirstLastName;

                if (userLastInfo.SecondLastName != userUpdate.SecondLastName)
                    userLastInfo.SecondLastName = userUpdate.SecondLastName;

                if (userLastInfo.BornDate != userUpdate.BornDate)
                    userLastInfo.BornDate = userUpdate.BornDate;

                if (userLastInfo.Sueldo != userUpdate.Sueldo)
                    userLastInfo.Sueldo = userUpdate.Sueldo;



                await UserInformationSecundaryPort.UpdateUser(userLastInfo);

                UserInformationDto userInformation = mapper.Map<UserInformationDto>(userUpdate);

                return new ResponseDto<UserInformationDto> { Data = userInformation, Message = MessageStateResponseOk, Response = "200" };
            }
            catch (Exception)
            {
                return new ResponseDto<UserInformationDto> { Data = null, Message = MessageGenericError, Response = "500" };
            }
        }

    }
}
