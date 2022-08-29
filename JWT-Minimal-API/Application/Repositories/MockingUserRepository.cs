using JWT_Minimal_API.Application.Models;

namespace JWT_Minimal_API.Application.Repositories
{
    public class MockingUserRepository: IUserRepository
    {
        private readonly List<User> Users = new()
        {
            new User { Username = "admin", EmailAddress = "test.admin@mail.com", Password = "admin", GivenName = "Bart", Surname = "Adminer", Role = "Administrator" },
            new User { Username = "normal1", EmailAddress = "test.normal@mail.com", Password = "Pa$$w0rd", GivenName = "Normie", Surname = "Smith", Role = "Normal" },
        };
        public List<User> GetUsers()
        {
            return Users;
        }
    }
}
