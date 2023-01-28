namespace CleanArchitectureTemplate.WebApp.Controllers;

public class BaseController : CleanArchitectureTemplateController
{
    public override void OnActionExecuted(ActionExecutedContext context)
    {        
        base.OnActionExecuted(context);
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        base.OnActionExecuting(context);
    }
}
