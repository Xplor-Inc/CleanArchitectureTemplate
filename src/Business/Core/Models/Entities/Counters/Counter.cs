namespace GenogramSystem.Core.Models.Entities.Counters;

public class Counter : Entity
{
    public string           Browser         { get; set; } = default!;
    public new long?        CreatedById     { get; set; }
    public string           Device          { get; set; } = default!;
    public string           IPAddress       { get; set; } = default!;
    public DateTimeOffset?  LastVisit       { get; set; }
    public string           OperatingSystem { get; set; } = default!;
    public string           Page            { get; set; } = default!;
    public string?          Search          { get; set; } = default!;
    public string           ServerName      { get; set; } = default!;
    public Guid             VisitorId       { get; set; } = default!;

    #region Analysis Data
    public int              EnquirySent     { get; set; }
    public int              Tracking        { get; set; }

    #endregion
}

