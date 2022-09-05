using AutoMapper;
using JWT_Minimal_API.Application.Dtos;
using JWT_Minimal_API.Application.Repositories;
using JWT_Minimal_API.Application.Services;
using MediatR;

namespace JWT_Minimal_API.Application.Commands
{
    public class RegistrationCommand : IRequest<string>
    {
        public readonly UserRegistrationData RegistrationData;
        public readonly IMapper Mapper;
        public readonly IUserService UserService;
        public readonly IMediator Mediator;

        public RegistrationCommand(UserRegistrationData registrationData, IMapper mapper, IUserService userService, IMediator mediator)
        {
            RegistrationData = registrationData;
            Mapper = mapper;
            UserService = userService;
            Mediator = mediator;
        }
    }
}
