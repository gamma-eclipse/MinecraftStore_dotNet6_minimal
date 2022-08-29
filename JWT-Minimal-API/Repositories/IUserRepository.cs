using JWT_Minimal_API.Models;

namespace JWT_Minimal_API.Repositories
{
    public interface IUserRepository
    {
        public List<User> GetUsers() => new();
    }
}
