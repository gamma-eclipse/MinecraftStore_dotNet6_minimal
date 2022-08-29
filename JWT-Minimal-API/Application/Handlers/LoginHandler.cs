using JWT_Minimal_API.Application.Commands;
using JWT_Minimal_API.Application.Services;
using MediatR;
using Serilog;

namespace JWT_Minimal_API.Application.Handlers
{
    public class LoginHandler:IRequestHandler<LoginCommand,string>
    {
        private readonly IUserService usersService;

        public LoginHandler(IUserService usersService)
        {
            this.usersService = usersService;
        }
        public Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            Log.Information($"Attempt login for user {request.user.Username}");

            if (string.IsNullOrEmpty(request.user.Username) || string.IsNullOrEmpty(request.user.Password))
                return Task.FromResult("Invalid user credentials");

            var loggedInUser = usersService.GetUser(request.user);

            if (loggedInUser is null) return Task.FromResult("User not found");

            var tokenString = usersService.GenerateToken(loggedInUser);
            return Task.FromResult(tokenString);
        }
    }
}
