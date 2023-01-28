using CleanArchitectureTemplate.Core.Enumerations;

namespace CleanArchitectureTemplate.WebApp.Filters;

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