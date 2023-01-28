namespace CleanArchitectureTemplate.SqlServer.Maps.Notifications;

public class NotificationMap : Map<Notification>
{
    public override void Configure(EntityTypeBuilder<Notification> entity)
    {
        entity
            .ToTable("Notifications");

        entity
            .Property(e => e.Message)
            .IsRequired()
            .HasMaxLength(StaticConfiguration.MAX_LENGTH);
    }
}
