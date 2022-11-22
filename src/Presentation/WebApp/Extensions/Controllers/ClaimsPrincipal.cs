using GenogramSystem.Core.Enumerations;

namespace GenogramSystem.WebApp.Extensions.Controllers;
public class GenogramSystemClaimsPrincipal
{
    public virtual UserRole    UserRole                { get; set; }
    public virtual long        UserId                  { get; set; }
}