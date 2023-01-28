namespace CleanArchitectureTemplate.Core.Models.Entities.Notifications;

public class Notification : Auditable
{
    public string           Message     { get; set; } = default!;
    public DateTimeOffset   ExpireAt    { get; set; }
    public DateTimeOffset   StartAt     { get; set; }
    public bool             IsPermanent { get; set; }
}
