using JWT_Minimal_API.Application.Models;
using MediatR;

namespace JWT_Minimal_API.Application.Queries
{
    public class GetAuthorizedUserQuery:IRequest<User>
    {
        public readonly HttpContext HttpContext;

        public GetAuthorizedUserQuery(HttpContext httpContext)
        {
            HttpContext = httpContext;
        }
    }
}
