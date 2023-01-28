using CleanArchitectureTemplate.Core.Enumerations;

namespace CleanArchitectureTemplate.WebApp.Extensions.Controllers;
public class CleanArchitectureTemplateClaimsPrincipal
{
    public virtual UserRole    UserRole                { get; set; }
    public virtual long        UserId                  { get; set; }
}