using AutoMapper;
using JWT_Minimal_API.Application.Commands;
using JWT_Minimal_API.Application.Dtos;
using JWT_Minimal_API.Application.Models.Db;
using JWT_Minimal_API.Application.Queries;
using JWT_Minimal_API.Application.Repositories;
using JWT_Minimal_API.Application.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
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

            app.MapPost("/login", [AllowAnonymous] async ([FromBody] UserCredentialsData user, IMediator _mediator) =>
                {
                    var command = new LoginCommand(user);
                    var result = await _mediator.Send(command);
                    return result;
                })
                .Accepts<UserCredentialsData>("application/json")
                .Produces<string>(statusCode: 200, contentType: "application/json");

            app.MapPost("/register", [AllowAnonymous] async ([FromBody] UserRegistrationData registrationData, IMediator _mediator,IMapper _mapper,IUserService userService) =>
                {
                    var Command = new RegistrationCommand(registrationData, _mapper,userService,_mediator);
                    var result = await _mediator.Send(Command);
                    return result;
                })
                .Accepts<UserRegistrationData>("application/json")
                .Produces<string>(statusCode: 200, contentType: "application/json");
            app.MapPost("/request-reset-password", [AllowAnonymous] async ([FromBody] UserPasswordResetData passwordResetData, IMediator _mediator, IMapper _mapper, IUserService userService) =>
                {
                    var Command = new RequestResetPasswordComand();
                    var result = await _mediator.Send(Command);
                    return result;
                })
                .Accepts<UserPasswordResetData>("application/json")
                .Produces<string>(statusCode:200,contentType:"application/json");
            app.MapPost("/confirm-reset-password", [AllowAnonymous] async ([FromBody] string code, IMediator _mediator, IMapper _mapper, IUserService userService) =>
                {
                    var Command = new ConfirmResetPasswordCommand();
                    var result = await _mediator.Send(Command);
                    return result;
                })
                .Accepts<UserPasswordResetData>("application/json")
                .Produces<string>(statusCode: 200, contentType: "application/json");
            return app;
        }
    }
}
