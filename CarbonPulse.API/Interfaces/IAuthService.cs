using CarbonPulse.API.DTOs;
using CarbonPulse.API.Models;

namespace CarbonPulse.API.Interfaces
{
    public interface IAuthService
    {
        Task<UserDto> Register(UserRegisterDto request);
        Task<UserDto> Login(UserLoginDto request);
        Task<bool> UserExists(string username);
    }
}
