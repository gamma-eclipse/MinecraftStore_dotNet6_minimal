using JWT_Minimal_API.Application.Commands;
using JWT_Minimal_API.Application.Dtos;
using JWT_Minimal_API.Application.Models.Db;
using JWT_Minimal_API.Application.Validation;
using MediatR;

namespace JWT_Minimal_API.Application.Handlers
{
    public class RegistrationHandler:IRequestHandler<RegistrationCommand,string>
    {
        public async Task<string> Handle(RegistrationCommand request, CancellationToken cancellationToken)
        {
            //create db record, generate token, send token back

            var data = request.RegistrationData;
            var validator = new UserRegistrationDataValidator();

            if (!(await validator.ValidateAsync(data, cancellationToken)).IsValid)
            {
                return "Invalid registration data";
            }

            var User = request.Mapper.Map<User>(data);
            User.Role = "default";
            User.RegistrationUnixTime = DateTimeOffset.Now.ToUnixTimeSeconds();

            request.UserService.AddUser(User);

            var LoginCommand = new LoginCommand(request.Mapper.Map<UserCredentialsData>(data));

            var token = await request.Mediator.Send(LoginCommand, cancellationToken);

            return token;
        }
    }
}
