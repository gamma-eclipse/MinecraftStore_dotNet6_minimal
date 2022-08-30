using JWT_Minimal_API.Application.Commands;
using JWT_Minimal_API.Application.Dtos;
using JWT_Minimal_API.Application.Services;
using MediatR;
using Serilog;
using System.Net;
using JWT_Minimal_API.Application.Validation;

namespace JWT_Minimal_API.Application.Handlers
{
    public class LoginHandler:IRequestHandler<LoginCommand,string>
    {

        private readonly IUserService usersService;
        private readonly UserCredentialsValidator _userCredentialsValidator;

        public LoginHandler(IUserService usersService)
        {
            this.usersService = usersService;
            _userCredentialsValidator = new UserCredentialsValidator();
        }
        public Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var credentials = request.user;
            Log.Information($"Attempt login for user {credentials.Username}");

            var userCredentialsValidationResult = _userCredentialsValidator.Validate(credentials);

            if (!userCredentialsValidationResult.IsValid)
                return Task.FromResult("Invalid user credentials");
            

            var loggedInUser = usersService.GetUserByCredentials(credentials);

            if (loggedInUser is null) return Task.FromResult("User not found or password is not valid");

            var tokenString = usersService.GenerateUserJWTToken(loggedInUser);
            return Task.FromResult(tokenString);
        }
    }
}
