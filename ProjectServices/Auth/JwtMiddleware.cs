using Microsoft.AspNetCore.Http;
using ProjectCore.Services;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectServices.Auth
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IUserService userService, IJwtUtils jwtUtils)
        {
            string token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            int? userId = jwtUtils.ValidateToken(token);

            if (userId != null)
            {
                // attach user to context on successful jwt validation
                context.Items["User"] = await userService.GetByIdAsync(userId.Value);
            }
            await _next(context);
        }
    }
}