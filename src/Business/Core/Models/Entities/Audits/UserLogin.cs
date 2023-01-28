using CleanArchitectureTemplate.Core.Models.Entities.Users;

namespace CleanArchitectureTemplate.Core.Models.Entities.Audits;
public class UserLogin : Entity
{
    public new long?    CreatedById        { get; set; }
    public string?      Email              { get; set; }
    public bool         IsLoginSuccess     { get; set; }
    public bool         IsValidUser        { get; set; }
    public long?        UserId             { get; set; }
    public string       VisitorId          { get; set; } = default!;

    public virtual User? User          { get; set; } 

}