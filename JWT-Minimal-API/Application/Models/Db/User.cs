using System.Text.Json.Serialization;

namespace JWT_Minimal_API.Application.Models.Db
{
    public class User
    {
        public string Username { get; set; } = string.Empty;
        public string EmailAddress { get; set; } = string.Empty;
        [JsonIgnore]
        public string Password { get; set; } = string.Empty;
        public string GivenName { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public long RegistrationUnixTime { get; set; } = 0;
    }
}
