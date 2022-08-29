using HealthChecks.UI.Client;
using JWT_Minimal_API.DTO;
using JWT_Minimal_API.Helpers;
using JWT_Minimal_API.Models;
using JWT_Minimal_API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
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

app.MapGet("/", () => "Hello in JWT .NET6 Minimal API!")
    .ExcludeFromDescription();

app.MapGet("/user",
(HttpContext httpContext, IUserService service) => GetUserClaims(httpContext, service))
    .Produces<User>(statusCode: 200, contentType: "application/json");

app.MapPost("/login", [AllowAnonymous]
(UserLogin user, IUserService service) => Login(user, service))
    .Accepts<UserLogin>("application/json")
    .Produces<string>(statusCode: 200, contentType: "application/json");

IResult Login(UserLogin user, IUserService service)
{

    Log.Information($"Attempt login for user {user.Username}");

    if (!string.IsNullOrEmpty(user.Username) && !string.IsNullOrEmpty(user.Password))
    {

        var loggedInUser = service.GetUser(user);

        if (loggedInUser is null) return Results.NotFound("User not found");

        var tokenString = service.GenerateToken(loggedInUser);

        return Results.Ok(tokenString);

    }

    return Results.BadRequest("Invalid user credentials");

}

IResult GetUserClaims(HttpContext httpContext, IUserService service)
{

    Log.Information($"Getting user claims");

    var userInfo = service.GetUserClaims(httpContext);
   
    if (userInfo is not null)
    {
        return Results.Ok(userInfo);
    }

    return Results.BadRequest();

}

app.Run();
