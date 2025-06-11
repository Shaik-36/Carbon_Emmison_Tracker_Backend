using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using CarbonPulse.API.DTOs;
using CarbonPulse.API.Interfaces;
using CarbonPulse.API.Models;
using Microsoft.IdentityModel.Tokens;

namespace CarbonPulse.API.Services
{
    public class AuthService : IAuthService
    {
        // ✅ Public static accessor for emission service
        public static List<User> Users => users;

        // In-memory storage (replace with database later)
        private static List<User> users = new();

        private readonly IConfiguration _config;

        public AuthService(IConfiguration config)
        {
            _config = config;
        }

        public Task<UserDto> Register(UserRegisterDto request)
        {
            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var user = new User
            {
                Username = request.Username.ToLower(),
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

            users.Add(user);

            var token = CreateToken(user);

            return Task.FromResult(new UserDto
            {
                Username = user.Username,
                Token = token
            });
        }

        public Task<UserDto> Login(UserLoginDto request)
        {
            var user = users.FirstOrDefault(u => u.Username == request.Username.ToLower());

            if (user == null || !VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
                throw new UnauthorizedAccessException("Invalid credentials.");

            var token = CreateToken(user);

            return Task.FromResult(new UserDto
            {
                Username = user.Username,
                Token = token
            });
        }

        public Task<bool> UserExists(string username)
        {
            bool exists = users.Any(u => u.Username == username.ToLower());
            return Task.FromResult(exists);
        }

        // ✅ Password Hashing
        private void CreatePasswordHash(string password, out byte[] hash, out byte[] salt)
        {
            using var hmac = new HMACSHA512();
            salt = hmac.Key;
            hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }

        private bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            using var hmac = new HMACSHA512(storedSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(storedHash);
        }

        // ✅ JWT Generation
        private string CreateToken(User user)
        {
            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _config.GetSection("AppSettings:Token").Value!
            ));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
