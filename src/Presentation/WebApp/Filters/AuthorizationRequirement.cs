using GenogramSystem.Core.Enumerations;

namespace GenogramSystem.WebApp.Filters;

public class AuthorizationRequirement : IAuthorizationRequirement
{
    public UserRole Role { get; set; }
    public AuthorizationRequirement()
    {
        Role = UserRole.Member;
    }
    public AuthorizationRequirement(UserRole  userRole)
    {
        Role = userRole;
    }
}