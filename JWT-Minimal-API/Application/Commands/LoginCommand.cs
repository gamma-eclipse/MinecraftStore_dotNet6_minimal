using JWT_Minimal_API.Application.Dtos;
using MediatR;

namespace JWT_Minimal_API.Application.Commands
{
    public class LoginCommand:IRequest<string>
    {
        public UserCredentials user;
        public LoginCommand(UserCredentials user)
        {
            this.user = user;
        }
    }
}
