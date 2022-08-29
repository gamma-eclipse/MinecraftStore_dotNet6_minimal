using JWT_Minimal_API.Application.Models;

namespace JWT_Minimal_API.Application.Repositories
{
    public interface IUserRepository
    {
        public List<User> GetUsers() => new();
    }
}
