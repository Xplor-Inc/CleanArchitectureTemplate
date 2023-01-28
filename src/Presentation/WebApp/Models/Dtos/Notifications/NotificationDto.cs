namespace CleanArchitectureTemplate.WebApp.Models.Dtos.Notifications;

public class NotificationDto : AuditableDto
{
    public string           Message     { get; set; } = default!;
    public DateTimeOffset   ExpireAt    { get; set; }
    public DateTimeOffset   StartAt     { get; set; }
    public bool             IsPermanent { get; set; }

}
