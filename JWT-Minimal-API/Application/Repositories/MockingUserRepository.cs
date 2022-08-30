using JWT_Minimal_API.Application.Models.Db;

namespace JWT_Minimal_API.Application.Repositories
{
    public class MockingUserRepository: IUserRepository
    {
        private readonly List<User> Users = new()
        {
            new User { Username = "admin", EmailAddress = "test.admin@mail.com", Password = "admin", GivenName = "Bart", Surname = "Adminer", Role = "Administrator" },
            new User { Username = "normal1", EmailAddress = "test.normal@mail.com", Password = "Pa$$w0rd", GivenName = "Normie", Surname = "Smith", Role = "Normal" },
        };
        public IEnumerable<User> GetAll()
        {
            return Users;
        }

        public User GetByID(int id)
        {
            throw new NotImplementedException();
        }

        public void Add(User entity)
        {
            throw new NotImplementedException();
        }

        public void Update(User entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(User entity)
        {
            throw new NotImplementedException();
        }

        public User GetByUsername(string username)
        {
            return Users.Find(x => x.Username == username) ?? new User();
        }
    }
}
