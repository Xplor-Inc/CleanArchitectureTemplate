using System.Net;
using System;
using UAParser;
using GenogramSystem.Core.Models.Entities.RequestLogs;

namespace GenogramSystem.WebApp.Controllers;

public class BaseController : GenogramSystemController
{
    public override void OnActionExecuted(ActionExecutedContext context)
    {
        var dbContext = HttpContext.RequestServices.GetRequiredService<GenogramSystemContext>();
        long? createdById = null;
        if (context.HttpContext.User.Identity?.IsAuthenticated ?? false)
            createdById = CurrentUserId;

        var log = new RequestLog
        {
            CreatedOn   = DateTimeOffset.Now,
            CreatedById = createdById,
            Page        = context.HttpContext.Request.Path,
            Search      = context.HttpContext.Request.QueryString.Value,
            UniqueId    = Guid.NewGuid(),
            VisitorId   = GetVisitorId()
        };
        dbContext.Add(log);
        dbContext.SaveChanges();
        base.OnActionExecuted(context);
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        base.OnActionExecuting(context);
    }
}
