using HealthChecks.UI.Client;
using JWT_Minimal_API.Application.Commands;
using JWT_Minimal_API.Application.Dtos;
using JWT_Minimal_API.Application.Mapping;
using JWT_Minimal_API.Application.Models.Db;
using JWT_Minimal_API.Application.Queries;
using JWT_Minimal_API.Application.Services;
using JWT_Minimal_API.Configuration;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Mvc;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddJWTAuthentication(builder.Configuration);
builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
        .Build();
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerJWTAuth();
builder.Services.AddHealthChecks();
builder.Services.AddHealthChecksUI().AddInMemoryStorage();

builder.Services.AddScoped<IUserService, UserService>();


builder.Services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddAutoMapper(typeof(TempAppMappingProfile));

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("Logs/log.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

var app = builder.Build();

Log.Information("JWT Minimal API started...");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Minimal API V1");
    });
}

app.UseAuthorization();
app.UseAuthentication();
app.UseHttpsRedirection();

app.MapHealthChecks("/health", new HealthCheckOptions()
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
}).WithMetadata(new AllowAnonymousAttribute());

app.MapHealthChecksUI().WithMetadata(new AllowAnonymousAttribute());

app.MapGet("/user", async (IMediator _mediator,HttpContext _context) =>
        {
            var query = new GetAuthorizedUserQuery(_context);
            var response = await _mediator.Send(query);
            
            //some mapping

            return Results.Ok(response);
        })
    .Produces<User>(statusCode: 200, contentType: "application/json");

app.MapPost("/login", [AllowAnonymous] async ([FromBody]UserCredentials user,IMediator _mediator) =>
    {
        var command = new LoginCommand(user);
        var result = await _mediator.Send(command);
        return result;
    })
    .Accepts<UserCredentials>("application/json")
    .Produces<string>(statusCode: 200, contentType: "application/json");


app.Run();
