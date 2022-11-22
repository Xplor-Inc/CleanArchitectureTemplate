namespace GenogramSystem.Core.Models.Entities.Enquiries;
public class Enquiry : Auditable
{
    public new long?        CreatedById         { get; set; }
    public EnquiryTypes     EnquiryType         { get; set; }
    public string           Email               { get; set; } = default!;
    public bool             IsResolved          { get; set; }
    public string           Message             { get; set; } = default!;
    public string           Name                { get; set; } = default!;
    public string?          Resolution          { get; set; }
    public Guid?            VisitorId           { get; set; }
}