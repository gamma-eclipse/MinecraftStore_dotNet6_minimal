using MediatR;

namespace JWT_Minimal_API.Application.Commands
{
    public class ConfirmResetPasswordCommand:IRequest<string>//responce -> jwt token and status? -> new dto
    {
        public ConfirmResetPasswordCommand()
        {
            
        }
    }
}
