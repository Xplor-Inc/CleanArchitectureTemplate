using GenogramSystem.Core.Enumerations;

namespace GenogramSystem.WebApp.Models.Dtos.Users;
public class UserDto : AuditableDto
{
    public DateTimeOffset?  AccountActivateDate     { get; set; }
    public string           EmailAddress            { get; set; } = default!;
    public string           FirstName               { get; set; } = default!;
    public Gender           Gender                  { get; set; }
    public bool             IsActive                { get; set; }
    public string           LastName                { get; set; } = default!;
    public UserRole         Role                    { get; set; }
    public string?          ImagePath               { get; set; } = default!;
}
