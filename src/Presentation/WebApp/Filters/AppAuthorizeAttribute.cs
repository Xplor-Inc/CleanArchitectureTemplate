using GenogramSystem.Core.Enumerations;

namespace GenogramSystem.WebApp.Filters;

public class AppAuthorizeAttribute : TypeFilterAttribute
{
    public AppAuthorizeAttribute()
        : base(typeof(AuthorizationFilter)) =>
        Arguments = new[] { new AuthorizationRequirement() };

    public AppAuthorizeAttribute(UserRole role)
        : base(typeof(AuthorizationFilter)) =>
        Arguments = new[] { new AuthorizationRequirement(role) };
}
