using System.Security.Claims;

using CustomerAndMatterService.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

public class JwtEmailValidationMiddleware
{
    private readonly RequestDelegate _next;

    public JwtEmailValidationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, IAuthenticationService authenticationService)
    {
        // Only check if user is authenticated
        if (context.User.Identity?.IsAuthenticated == true)
        {
            var email = context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            if (!string.IsNullOrEmpty(email))
            {
                var lawyer = await authenticationService.GetLawyerByEmailAsync(email);
                if (lawyer == null)
                {
                    // If the email is not found in the database, return 401 Unauthorized
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("Unauthorized: Email not found in database.");
                    return;
                }

                //This adds the lawyer object from the database so there is access to the Id, etc.
                context.Items["Lawyer"] = lawyer;
            }
        }

        await _next(context);
    }
}