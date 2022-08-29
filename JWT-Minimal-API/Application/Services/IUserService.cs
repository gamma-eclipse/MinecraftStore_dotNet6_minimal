using JWT_Minimal_API.Application.Dtos;
using JWT_Minimal_API.Application.Models;

namespace JWT_Minimal_API.Application.Services
{
    public interface IUserService
    {
        public User? GetUser(UserCredentials userCredentials);
        string GenerateToken(User loggedInUser);
        User? GetUserClaims(HttpContext httpContext);
    }
}
