using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace PDP.Web.API.Security
{
    public class CurrentUser : ICurrentUser
    {
        private readonly IHttpContextAccessor httpContext;

        public CurrentUser(IHttpContextAccessor httpContext)
        {
            this.httpContext = httpContext;
        }

        public int Id => int.Parse(httpContext.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

        public string Role => httpContext.HttpContext.User.FindFirstValue(ClaimTypes.Role);

        public string Username => httpContext.HttpContext.User.FindFirstValue(ClaimTypes.Name);
    }
}