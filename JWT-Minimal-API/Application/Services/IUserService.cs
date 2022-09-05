using JWT_Minimal_API.Application.Dtos;
using JWT_Minimal_API.Application.Models.Db;

namespace JWT_Minimal_API.Application.Services
{
    public interface IUserService
    {
        public User? GetUserByCredentials(UserCredentials userCredentials);
        string GenerateUserJWTToken(User loggedInUser);
        User? GetUserClaims(HttpContext httpContext);
        void AddUser(User user);
    }
}
