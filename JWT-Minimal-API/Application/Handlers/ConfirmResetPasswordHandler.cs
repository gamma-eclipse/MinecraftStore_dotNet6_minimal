using JWT_Minimal_API.Application.Commands;
using MediatR;

namespace JWT_Minimal_API.Application.Handlers
{
    public class ConfirmResetPasswordHandler:IRequestHandler<ConfirmResetPasswordCommand,string>
    {
        public Task<string> Handle(ConfirmResetPasswordCommand request, CancellationToken cancellationToken)
        {
            //get previous pass reset data from db, replace pass in Users db, generate token
            throw new NotImplementedException();
        }
    }
}
