namespace GenogramSystem.Core.Models.Entities.RequestLogs;

public class RequestLog : Entity
{
    public new long?        CreatedById     { get; set; }
    public string           Page            { get; set; } = default!;
    public string?          Search          { get; set; } = default!;
    public Guid             VisitorId       { get; set; } = default!;

}
