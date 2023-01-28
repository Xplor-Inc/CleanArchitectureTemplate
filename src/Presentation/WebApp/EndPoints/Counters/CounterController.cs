using CleanArchitectureTemplate.Core.Interfaces.Utility;

namespace CleanArchitectureTemplate.WebApp.EndPoints.Counters;

[Route("api/1.0/counter")]
public class CounterController : CleanArchitectureTemplateController
{
    private IMapper Mapper { get; }
    private CleanArchitectureTemplateContext DbContext { get { return HttpContext.RequestServices.GetRequiredService<CleanArchitectureTemplateContext>(); } }

    public CounterController(IMapper mapper)
    {
        Mapper = mapper;
    }


    [AppAuthorize]
    [HttpGet]
    public IActionResult Index(
        string sortBy       = "CreatedOn",
        string sortOrder    = "DESC",
        int skip            = 0,
        int take            = 5)
    {

        var counters = DbContext.Counters.Skip(skip).Take(take).OrderByDescending(e => e.CreatedOn).ToList();
        var rowCount = DbContext.Counters.Count();
        var dtos     = Mapper.Map<List<CounterDto>>(counters);
       
        return Ok(dtos, rowCount);
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] CounterPostDto counter)
    {
        var visitorId = GetVisitorId();
        var findSession = DbContext.Counters.FirstOrDefault(c => c.VisitorId == visitorId);

        var userAgentConductor = HttpContext.RequestServices.GetRequiredService<IUserAgentConductor>();

        var (ipAddress, operatingSystem, browser, device) = userAgentConductor.GetUserAgent(HttpContext);
       
        if (findSession == null)
        {
            findSession = new Counter
            {
                CreatedOn       = DateTimeOffset.Now,
                Browser         = browser,
                Device          = device,
                IPAddress       = ipAddress,
                OperatingSystem = operatingSystem,
                Page            = counter.Page,
                Search          = counter.Search,
                UniqueId        = Guid.NewGuid(),
                VisitorId       = visitorId,
                ServerName      = Environment.MachineName
            };
            DbContext.Counters.Add(findSession);
        }
        else
        {
            findSession.LastVisit = DateTimeOffset.UtcNow;
        }
        await DbContext.SaveChangesAsync();

        return Ok(string.Empty);
    }
}