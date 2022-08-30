using JWT_Minimal_API.Application.Models.Db;
using JWT_Minimal_API.Application.Queries;
using JWT_Minimal_API.Application.Services;
using MediatR;
using Microsoft.AspNetCore.Http;
using Serilog;

namespace JWT_Minimal_API.Application.Handlers
{
    public class GetAuthorizedUserHandler:IRequestHandler<GetAuthorizedUserQuery,User>
    {
        private readonly IUserService usersService;
        public GetAuthorizedUserHandler( IUserService usersService)
        {
            this.usersService = usersService;
        }

        public Task<User> Handle(GetAuthorizedUserQuery request, CancellationToken cancellationToken)
        {
            Log.Information($"Getting user claims");
             
            return Task.FromResult(usersService.GetUserClaims(request.HttpContext));
        }
    }
}
