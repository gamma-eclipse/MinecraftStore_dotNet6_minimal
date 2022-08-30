using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JWT_Minimal_API.Application.Dtos;
using JWT_Minimal_API.Application.Models.Db;
using JWT_Minimal_API.Application.Repositories;
using Microsoft.IdentityModel.Tokens;

namespace JWT_Minimal_API.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IConfiguration _config;
        public UserService(IConfiguration configuration)
        {
            _config = configuration;
        }
        public User? GetUserByCredentials(UserCredentials userCredentials)
        {
            IUserRepository userRepository = new MockingUserRepository();
            var user = userRepository.GetAll()
                .FirstOrDefault(o => o.Username.Equals(userCredentials.Username, StringComparison.OrdinalIgnoreCase) && o.Password.Equals(userCredentials.Password));
            return user;
        }
        public string GenerateUserJWTToken(User loggedInUser)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, loggedInUser.Username),
                new Claim(ClaimTypes.Email, loggedInUser.EmailAddress),
                new Claim(ClaimTypes.GivenName, loggedInUser.GivenName),
                new Claim(ClaimTypes.Surname, loggedInUser.Surname),
                new Claim(ClaimTypes.Role, loggedInUser.Role)
            };
            var token = new JwtSecurityToken
            (
                issuer: _config["JWT:Issuer"],
                audience: _config["JWT:Audience"],
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"])), SecurityAlgorithms.HmacSha256),
                claims: claims,
                expires: DateTime.UtcNow.AddDays(2),
                notBefore: DateTime.UtcNow
            );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return tokenString;
        }
        public User GetUserClaims(HttpContext httpContext)
        {
            if (httpContext.User.Identity is not ClaimsIdentity identity) return null;
            var userClaims = identity.Claims.ToList();
            var user = new User()
            {
                Username = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value,
                GivenName = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.GivenName)?.Value,
                Surname = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Surname)?.Value,
                EmailAddress = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Email)?.Value,
                Role = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Role)?.Value
            };
            return user;

        }
    }
}
