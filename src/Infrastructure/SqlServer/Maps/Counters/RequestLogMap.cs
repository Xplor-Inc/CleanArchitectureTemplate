using GenogramSystem.Core.Models.Entities.RequestLogs;

namespace GenogramSystem.SqlServer.Maps.Enquiries;
public class RequestLogMap : Map<RequestLog>
{
    public override void Configure(EntityTypeBuilder<RequestLog> entity)
    {
        entity
            .ToTable("RequestLogs");
      
        entity
            .Property(e => e.Page)
            .HasMaxLength(StaticConfiguration.COMMAN_LENGTH);

        entity
            .Property(e => e.Search)
            .HasMaxLength(StaticConfiguration.COMMAN_LENGTH);
    }
}