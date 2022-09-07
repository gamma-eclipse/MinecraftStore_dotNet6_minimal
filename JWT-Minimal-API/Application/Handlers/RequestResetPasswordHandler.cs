using JWT_Minimal_API.Application.Commands;
using MediatR;

namespace JWT_Minimal_API.Application.Handlers
{
    public class RequestResetPasswordHandler : IRequestHandler<RequestResetPasswordComand, string>
    {
        public Task<string> Handle(RequestResetPasswordComand request, CancellationToken cancellationToken)
        {
            //generation of reset-token randomly saving to db with expiration -> returning something
            throw new NotImplementedException();
        }
    }
}
