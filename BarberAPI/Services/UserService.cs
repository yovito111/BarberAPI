using BarberAPI.Dto;
using BarberAPI.Dtos;
using BarberAPI.Models;
using BarberAPI.Repositories;

namespace BarberAPI.Services
{
    public class UserService
    {
        private readonly UserRepository _userRepository;
        private readonly ILogger<UserService> logger;

        public UserService(UserRepository userRepository, ILogger<UserService> logger)
        {
            _userRepository = userRepository;
            this.logger = logger;
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            try
            {
                logger.LogInformation("Estamos obteniendo el listado de usuarios desde la capa de servicios");
                var users = await _userRepository.GetAllAsync();
                return users.Select(user => new UserDto
                {
                    Id = user.Id,
                    Username = user.Username,
                    PasswordHash = user.PasswordHash,
                    Email = user.Email

                });

            }
            catch (Exception ex)
            { 
               logger.LogError("Se presentó una excepción en la capa de servicios", ex.Message);
                return Enumerable.Empty<UserDto>();
            }

        }

        public async Task<UserDto> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                return null;
            }

            return new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email
                
            };
        }

        public async Task<UserCreationDTO> CreateUserAsync(UserCreationDTO userDto)
        {

            var user = new UserModel
            {
                Username = userDto.Username,
                PasswordHash = userDto.PasswordHash,
                Email = userDto.Email
            };

            await _userRepository.AddAsync(user);
            userDto.Id = user.Id;

            return userDto;
        }

        public async Task<UserCreationDTO> UpdateUserAsync(int id, UserCreationDTO userDto)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                return null;
            }

            user.Username = userDto.Username;
            user.PasswordHash = userDto.PasswordHash;
            user.Email = userDto.Email;

            await _userRepository.UpdateAsync(user);

            return userDto;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                return false;
            }

            await _userRepository.DeleteAsync(user);

            return true;
        }
    }
}

