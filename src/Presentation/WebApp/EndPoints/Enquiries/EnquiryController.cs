using GenogramSystem.Core.Interfaces.Emails.EmailHandler;
using GenogramSystem.Core.Interfaces.Emails.Templates;
using GenogramSystem.Core.Interfaces.Utility;

namespace GenogramSystem.WebApp.EndPoints.Enquiries;

[Route("api/1.0/enquiries")]
public class EnquiryController : GenogramSystemController
{
    private IRepositoryConductor<Enquiry>    EnquiryConductor   { get; }
    private IEmailHandler                    EmailHandler       { get; }
    private IHtmlTemplate                    HtmlTemplate       { get; }
    private IMapper                          Mapper             { get; }
    public IUserAgentConductor               UserAgentConductor { get; }

    public EnquiryController(
        IRepositoryConductor<Enquiry>   enquiryConductor,
        IEmailHandler                   emailHandler,
        IHtmlTemplate                   htmlTemplate,
        IMapper                         mapper,
        IUserAgentConductor             userAgentConductor)
    {
        EnquiryConductor    = enquiryConductor;
        EmailHandler        = emailHandler;
        HtmlTemplate        = htmlTemplate;
        Mapper              = mapper;
        UserAgentConductor  = userAgentConductor;
    }


    [AppAuthorize]
    [HttpGet]
    public IActionResult Index(
        string?     searchText,
        string      sortBy      = "CreatedOn",
        string      sortOrder   = "DESC",
        int         skip        = 0,
        int         take        = 5)
    {
        Expression<Func<Enquiry, bool>> predicate = e => true;

        if (!string.IsNullOrEmpty(searchText))
        {
            predicate = predicate.AndAlso(e => e.Name.Contains(searchText) 
                                            || e.Email.Contains(searchText)
                                            || e.Message.Contains(searchText));
        }

        var enquiryResult = EnquiryConductor.FindAll(filter: predicate, e => e.OrderBy(sortBy, sortOrder), skip: skip, take: take);
        if (enquiryResult.HasErrors)
        {
            return InternalError<EnquiryDto>(enquiryResult.Errors);
        }
        var enquiries = enquiryResult.ResultObject.ToList();
        var rowCount = EnquiryConductor.FindAll(filter: predicate).ResultObject.Select(e => e.Id).Count();
        var dtos = Mapper.Map<List<EnquiryDto>>(enquiries);
        return Ok(dtos, rowCount);
    }

    [HttpPost]
    public IActionResult Post([FromBody] EnquiryDto dto)
    {       
        var enquiry = Mapper.Map<Enquiry>(dto);
        var visitorId = GetVisitorId();
        enquiry.VisitorId = visitorId;
        var createResult = EnquiryConductor.Create(enquiry, CurrentUserId);
        if (createResult.HasErrors)
        {
            return InternalError<EnquiryDto>(createResult.Errors);
        }

        SaveCount(visitorId, false);

        var (ipAddress, operatingSystem, browser, device) = UserAgentConductor.GetUserAgent(HttpContext);
        Dictionary<string, string> substitutions = new()
        {
            { "Name",               enquiry.Name },
            { "Email",              enquiry.Email },
            { "Message",            enquiry.Message },
            { "OperatingSystem",    operatingSystem },
            { "BrowserName",        browser },
            { "IPAddress",          ipAddress },
            { "Device",             device }
        };
        string emailbody = HtmlTemplate.EnquiryThanks(substitutions);

        EmailHandler.Send(emailbody, "New Inquiry", new string[] { dto.Email });
        emailbody = emailbody.Replace("Thank you for contacting us! We will be in touch shortly", "")
                             .Replace(enquiry.Name, "Team");
        var reponse = EmailHandler.Send(emailbody, "New Inquiry");

        return Ok(reponse);
    }

    [AppAuthorize]
    [HttpPut("{id:Guid}")]
    public IActionResult Put(Guid id, [FromBody] EnquiryResolutionDto dto)
    {
        var enquiryResult = EnquiryConductor.FindAll(e=>e.UniqueId == id);
        if (enquiryResult.HasErrors)
        {
            return InternalError<EnquiryDto>(enquiryResult.Errors);
        }
        var enquiry = enquiryResult.ResultObject.FirstOrDefault();
        if (enquiry == null)
        {
            return InternalError<EnquiryDto>("Invalid enquiry");
        }

        enquiry.IsResolved   = true;
        enquiry.Resolution   = dto.Resolution;

        var updateResult = EnquiryConductor.Update(enquiry, CurrentUserId);
        if (updateResult.HasErrors)
        {
            return InternalError<EnquiryDto>(updateResult.Errors);
        }
        return Ok(updateResult.ResultObject);
    }

    [AppAuthorize]
    [HttpDelete("{id:Guid}")]
    public IActionResult Delete(Guid id)
    {
        var enquiryResult = EnquiryConductor.FindAll(e => e.UniqueId == id);
        if (enquiryResult.HasErrors)
        {
            return InternalError<EnquiryDto>(enquiryResult.Errors);
        }
        var enquiry = enquiryResult.ResultObject.FirstOrDefault();
        if (enquiry == null)
        {
            return InternalError<EnquiryDto>("Invalid enquiry");
        }

        var updateResult = EnquiryConductor.Delete(enquiry, CurrentUserId);
        if (updateResult.HasErrors)
        {
            return InternalError<EnquiryDto>(updateResult.Errors);
        }
        return Ok(updateResult.ResultObject);
    }
}