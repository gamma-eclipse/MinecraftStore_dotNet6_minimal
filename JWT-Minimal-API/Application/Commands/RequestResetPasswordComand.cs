using MediatR;

namespace JWT_Minimal_API.Application.Commands
{
    public class RequestResetPasswordComand:IRequest<string>
    {
        public RequestResetPasswordComand()
        {
            
        }
    }
}
