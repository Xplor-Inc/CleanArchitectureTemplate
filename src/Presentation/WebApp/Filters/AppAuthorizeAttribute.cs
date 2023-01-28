using CleanArchitectureTemplate.Core.Enumerations;

namespace CleanArchitectureTemplate.WebApp.Filters;

public class AppAuthorizeAttribute : TypeFilterAttribute
{
    public AppAuthorizeAttribute()
        : base(typeof(AuthorizationFilter)) =>
        Arguments = new[] { new AuthorizationRequirement() };

    public AppAuthorizeAttribute(UserRole role)
        : base(typeof(AuthorizationFilter)) =>
        Arguments = new[] { new AuthorizationRequirement(role) };
}
