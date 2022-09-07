using JWT_Minimal_API.Application.Dtos;
using JWT_Minimal_API.Application.Models.Db;

namespace JWT_Minimal_API.Application.Services
{
    public interface IUserService
    {
        public User? GetUserByCredentials(UserCredentialsData userCredentialsData);
        string GenerateUserJWTToken(User loggedInUser);
        User? GetUserClaims(HttpContext httpContext);
        void AddUser(User user);
    }
}
