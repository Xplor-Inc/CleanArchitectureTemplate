namespace CleanArchitectureTemplate.SqlServer.Maps.Enquiries;
public class CounterMap : Map<Counter>
{
    public override void Configure(EntityTypeBuilder<Counter> entity)
    {
        entity
            .ToTable("Counters");

        entity
            .Property(e => e.Browser)
            .IsRequired()
            .HasMaxLength(StaticConfiguration.COMMAN_LENGTH);

        entity
            .Property(e => e.Device)
            .IsRequired()
            .HasMaxLength(StaticConfiguration.COMMAN_LENGTH);

        entity
            .Property(e => e.IPAddress)
            .IsRequired()
            .HasMaxLength(StaticConfiguration.COMMAN_LENGTH);

        entity
            .Property(e => e.OperatingSystem)
            .HasMaxLength(StaticConfiguration.COMMAN_LENGTH);

        entity
            .Property(e => e.Page)
            .HasMaxLength(StaticConfiguration.COMMAN_LENGTH);

        entity
            .Property(e => e.Search)
            .HasMaxLength(StaticConfiguration.COMMAN_LENGTH);

        entity
            .Property(e => e.ServerName)
            .HasMaxLength(StaticConfiguration.COMMAN_LENGTH);
    }
}