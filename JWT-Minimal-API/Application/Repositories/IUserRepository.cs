using JWT_Minimal_API.Application.Models.Db;

namespace JWT_Minimal_API.Application.Repositories
{
    public interface IUserRepository:IRepository<User>
    {
        public User GetByUsername(string name);
    }
}
