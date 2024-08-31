using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiUsers.Application.Ports.Primary;
using WebApiUsers.Domain.Dtos;

namespace WebUserApiCodeFirst.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        readonly IUserInformationPrimaryPort UserInformation;

        public UserController(IUserInformationPrimaryPort userInformation)
        {
            UserInformation = userInformation;
        }

        /// <summary>
        /// Get all users.
        /// </summary> 
        /// <remarks>       
        /// <description>
        /// Returns Retrieve a list of all users.
        /// </description>
        /// </remarks>
        /// <response code="200">Returns Retrieve a list of all users.</response>
        /// <returns>Retrieve a list of all users.</returns>
        [HttpGet()]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAll(string? firstName, string? firstLastName, int pageNumber, int pageSize)
        {
            var message = await UserInformation.HandleGetUsersAsync(new SearchDto { FirstName = firstName, FirstLastName = firstLastName, PageSize = pageSize, PageNumber = pageNumber });

            if (message.Response != StatusCodes.Status200OK.ToString())
            {
                return BadRequest(message);
            }

            return this.StatusCode(StatusCodes.Status200OK, message);
        }

        // GET: Users/Details/5
        /// <summary>
        /// Get specific user.
        /// </summary>
        /// <remarks>       
        /// <description>
        ///   Returns Retrieve a user.
        /// </description>
        /// </remarks>
        /// <param name="id"></param>
        /// <response code="200">Retrieve a user.</response>
        /// <returns>Retrieve a user.</returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get(int id)
        {
            var message = await UserInformation.HandleGetUserByIdAsync(id);

            if (message.Response == StatusCodes.Status404NotFound.ToString())
            {
                return NotFound(message);
            }

            if (message.Response != StatusCodes.Status200OK.ToString())
            {
                return BadRequest(message);
            }



            return this.StatusCode(StatusCodes.Status200OK, message);
        }

        // POST: Users/Create
        /// <summary>
        /// Create a new user.
        /// </summary>
        /// <remarks>       
        /// <description>
        ///   Create a new user with the provided details.
        /// </description>
        /// </remarks>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateUserDto user)
        {
            var message = await UserInformation.HandleAddUserAsync(user);
            if (message.Response != StatusCodes.Status201Created.ToString())
            {
                return BadRequest(message);
            }

            return new ObjectResult(message) { StatusCode = StatusCodes.Status201Created };
        }

        /// <summary>
        /// Update a user.
        /// </summary>
        /// <remarks>       
        /// <description>
        ///   Update the details of an existing user.
        /// </description>
        /// </remarks>
        /// <param name="id">Id identifier of user</param>
        /// <param name="user">Object with information of the user</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateUserDto user)
        {
            if (id != user.Id)
            {
                return BadRequest("Invalid Id");
            }


            var message = await UserInformation.HandleUpdateUserAsync(user);

            if (message.Response != StatusCodes.Status200OK.ToString())
            {
                return BadRequest(message);
            }

            return this.StatusCode(StatusCodes.Status200OK, message);
        }

        // Delete: Users/Delete/5
        /// <summary>
        /// Delete a user.
        /// </summary>
        /// <remarks>       
        /// <description>
        ///   Delete a user by their ID.
        /// </description>
        /// </remarks>
        /// <param name="id">Id identifier of user</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            var message = await UserInformation.HandleDeleteUserAsync(id);

            if (message.Response != StatusCodes.Status200OK.ToString())
            {
                return BadRequest(message);
            }

            return this.StatusCode(StatusCodes.Status200OK, message);
        }
    }
}
