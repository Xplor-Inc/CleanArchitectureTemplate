using GenogramSystem.Core.Enumerations;
using System.Security.Claims;

namespace GenogramSystem.WebApp.Filters;

public class AuthorizationFilter : IAuthorizationFilter
{
    readonly AuthorizationRequirement Requirement;
    public AuthorizationFilter(AuthorizationRequirement requirement)
    {
        Requirement = requirement;
    }
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (context is null || context.HttpContext is null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        //_requirement.
        // If the user is not authenticated, return a 401
        if (!context.HttpContext.User?.Identity?.IsAuthenticated ?? false)
        {
            context.Result = new UnauthorizedResult();
            return;
        }
        var claim = context.HttpContext.User?.Claims.FirstOrDefault(e => e.Type == ClaimTypes.Role)?.Value;
        if (claim is null)
        {
            context.Result = new UnauthorizedResult();
            return;
        }
        if (claim != UserRole.Admin.ToString() && claim != Requirement.Role.ToString())
        {
            context.Result = new UnauthorizedResult();
            return;
        }
    }
}