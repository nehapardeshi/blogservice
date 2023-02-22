using Blog.WebAPI.Models;
using BusinessLogicLayer;
using BusinessLogicLayer.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Blog.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AddUser([FromBody] AddUserRequest request)
        {
            BusinessLogicLayer.Models.User createdUser;
            //Email vaidation:
            try
            {
                createdUser = await _userService.AddUserAsync(request.FirstName, request.LastName, request.Email, request.Password);
            }
            catch (UserAlreadyExistsException ex)
            {
                return BadRequest(ex.ToString());
            }
            catch (EmailNotValidException ex)
            {
                return BadRequest(ex.ToString());
            }

            return Ok(createdUser.UserId);
        }

        [HttpPost]
        [Route("login")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(WebAPI.Models.User))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> LoginUser([FromBody] LoginInfo info)
        {
            var user = _userService.LoginUser(info.Email, info.Password);
            if (user == null)
                return NotFound($"Incorrect username or password!");

            return Ok(new User
            {
                Email = info.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Id = user.UserId
            });
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdatePassword([FromBody] PasswordResetRequest request)
        {
            try
            {
                await _userService.UpdatePasswordAsync(request.Email, request.NewPassword);
            }
            catch (UserNotFoundException ex)
            {
                return NotFound(ex.ToString());
            }
            return Ok(true);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Models.User))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = _userService.GetUser(id);

            if (user == null)
                return NotFound($"User not found with User Id {id}");

            var userModel = new Models.User
            {
                FirstName = user.FirstName,
                Email = user.Email,
                Id = user.UserId,
                LastName = user.LastName
            };
            return Ok(userModel);
        }
    }
}
