namespace JWT_Minimal_API.Application.Dtos
{
    [Serializable]
    public class UserRegistrationData
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string EmailAddress { get; set; }
    } 
}
