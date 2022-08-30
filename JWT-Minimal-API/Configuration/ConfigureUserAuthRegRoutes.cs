using JWT_Minimal_API.Application.Commands;
using JWT_Minimal_API.Application.Dtos;
using JWT_Minimal_API.Application.Models.Db;
using JWT_Minimal_API.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JWT_Minimal_API.Configuration
{
    public static class UserAuthenticationRegistrationRoutes
    {
        public static IEndpointRouteBuilder AddUserEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/user", async (IMediator _mediator, HttpContext _context) =>
                {
                    var query = new GetAuthorizedUserQuery(_context);
                    var response = await _mediator.Send(query);

                    //some mapping

                    return Results.Ok(response);
                })
                .Produces<User>(statusCode: 200, contentType: "application/json");

            app.MapPost("/login", [AllowAnonymous] async ([FromBody] UserCredentials user, IMediator _mediator) =>
                {
                    var command = new LoginCommand(user);
                    var result = await _mediator.Send(command);
                    return result;
                })
                .Accepts<UserCredentials>("application/json")
                .Produces<string>(statusCode: 200, contentType: "application/json");
            return app;
        }
    }
}
