namespace GenogramSystem.WebApp.Models.Dtos.Enquiries;
public class EnquiryDto : AuditableDto
{
    public string   Email         { get; set; } = default!;
    public string   Name          { get; set; } = default!;
    public string   Message       { get; set; } = default!;
    public string?  Resolution    { get; set; } = default!;
    public bool     IsResolved    { get; set; } = default!;
}