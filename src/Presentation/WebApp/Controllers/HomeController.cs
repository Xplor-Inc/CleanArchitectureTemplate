namespace CleanArchitectureTemplate.WebApp.Controllers;

public class HomeController : BaseController
{
    public IActionResult Index()
    {
        return View();
    }
}
