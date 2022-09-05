using JWT_Minimal_API.Application.Mapping;
using JWT_Minimal_API.Application.Services;
using JWT_Minimal_API.Configuration;
using MediatR;
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

builder.Services.AddSingleton<IUserService, UserService>();//for Test


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

app.AddUserEndpoints();

app.Run();
