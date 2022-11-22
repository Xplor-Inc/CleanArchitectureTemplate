using GenogramSystem.Core.Enumerations;

namespace GenogramSystem.WebApp;
public abstract class GenogramSystemController : ControllerController
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

    protected GenogramSystemContext SaveCount(Guid visitorId, bool tracking)
    {
        var dbContext = HttpContext.RequestServices.GetRequiredService<GenogramSystemContext>();
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

    protected virtual GenogramSystemClaimsPrincipal? GenogramSystemClaims { get; set; }
    protected virtual UserRole? CurrentRoleType => GenogramSystemClaims != null ? GenogramSystemClaims.UserRole : User.RoleType();
    protected virtual long      CurrentUserId   => GenogramSystemClaims != null ? GenogramSystemClaims.UserId : User.UserId();
}