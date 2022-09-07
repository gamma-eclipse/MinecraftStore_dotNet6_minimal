using JWT_Minimal_API.Application.Dtos;
using MediatR;

namespace JWT_Minimal_API.Application.Commands
{
    public class LoginCommand:IRequest<string>
    {
        public UserCredentialsData user;
        public LoginCommand(UserCredentialsData user)
        {
            this.user = user;
        }
    }
}
