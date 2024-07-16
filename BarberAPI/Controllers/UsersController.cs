using BarberAPI.Dto;
using BarberAPI.Dtos;
using BarberAPI.Middleware;
using BarberAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace BarberAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly ILogger<UsersController> logger;
        private readonly IExceptionHandler exceptionHandler;

        public UsersController(UserService userService, ILogger<UsersController> logger,
            IExceptionHandler exceptionHandler)
        {
            _userService = userService;
            this.logger = logger;
            this.exceptionHandler = exceptionHandler;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
                var users = await _userService.GetAllUsersAsync();
                return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUser(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<UserCreationDTO>> CreateUser(UserCreationDTO userDto)
        {

            var createdUser = await _userService.CreateUserAsync(userDto);
            return CreatedAtAction(nameof(GetUser), new { id = createdUser.Id }, createdUser);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UserCreationDTO userDto)
        {
            if (id != userDto.Id)
            {
                return BadRequest();
            }

            var updatedUser = await _userService.UpdateUserAsync(id, userDto);
            if (updatedUser == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var result = await _userService.DeleteUserAsync(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
