namespace CleanArchitectureTemplate.WebApp;
public abstract class CleanArchitectureTemplateController : ControllerController
{
    protected string VisitorCookiesName { get { return "ai-c"; } }

    protected Guid GetVisitorId()
    {
        var vId = Guid.NewGuid();
        var visitorId = HttpContext.Request.Cookies[VisitorCookiesName];
        if (visitorId == null)
        {
            visitorId = $"{vId:N}";
            HttpContext.Response.Cookies.Append(VisitorCookiesName, visitorId, options: new CookieOptions { });
        }
        else { vId = Guid.Parse(visitorId); }
        return vId;
    }

    protected CleanArchitectureTemplateContext SaveCount(Guid visitorId, bool tracking)
    {
        var dbContext = HttpContext.RequestServices.GetRequiredService<CleanArchitectureTemplateContext>();
        var counter = dbContext.Counters.FirstOrDefault(e => e.VisitorId == visitorId);
        if (counter != null)
        {
            if (tracking)
                counter.Tracking += 1;
            else
                counter.EnquirySent += 1;
            dbContext.SaveChanges();
        }
        return dbContext;
    }

    protected virtual CleanArchitectureTemplateClaimsPrincipal? CleanArchitectureTemplateClaims { get; set; }
    protected virtual UserRole? CurrentRoleType => CleanArchitectureTemplateClaims != null ? CleanArchitectureTemplateClaims.UserRole : User.RoleType();
    protected virtual long      CurrentUserId   => CleanArchitectureTemplateClaims != null ? CleanArchitectureTemplateClaims.UserId : User.UserId();
}