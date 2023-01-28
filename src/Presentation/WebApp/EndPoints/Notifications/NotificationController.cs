namespace CleanArchitectureTemplate.WebApp.EndPoints.Notifications;

[AppAuthorize]
[Route("api/1.0/notifications")]
public class NotificationController : CleanArchitectureTemplateController
{
    #region Properties
    private IMapper                             Mapper           { get; }
    private IRepositoryConductor<Notification>  NotificationRepo { get; }
    #endregion

    #region Constructor
    public NotificationController(
        IMapper                             mapper,
        IRepositoryConductor<Notification>  notificationRepo)
    {
        Mapper           = mapper;
        NotificationRepo = notificationRepo;
    }
    #endregion


    [HttpGet]
    public IActionResult Index(bool includeDeleted, bool showAsNotification)
    {
        Expression<Func<Notification, bool>> predicate = (e) => true;
        var date = DateTimeOffset.Now;
        if (showAsNotification)
        {
            predicate = predicate.AndAlso(e => e.StartAt   <= date
                                            && e.ExpireAt  >= date);
        }
        if(!includeDeleted)
        {
            predicate = predicate.AndAlso(e => e.DeletedOn == null);
        }
        var notificationResult = NotificationRepo.FindAll(filter: predicate, e => e.OrderBy("ExpireAt", "DESC"));
        if (notificationResult.HasErrors)
        {
            return InternalError<NotificationDto>(notificationResult.Errors);
        }
        var notifications = notificationResult.ResultObject.ToList();
        var dtos = Mapper.Map<List<NotificationDto>>(notifications);
        return Ok(dtos);
    }


    [AppAuthorize(UserRole.Admin)]
    [HttpPost]
    public IActionResult Post([FromBody] NotificationDto dto)
    {       
        var notification = Mapper.Map<Notification>(dto);
        var createResult = NotificationRepo.Create(notification, CurrentUserId);
        if (createResult.HasErrors)
        {
            return InternalError<NotificationDto>(createResult.Errors);
        }

        return Ok(!createResult.HasErrors);
    }


    [AppAuthorize(UserRole.Admin)]
    [HttpPut("{id:Guid}")]
    public IActionResult Put(Guid id, [FromBody] NotificationDto dto)
    {
        var notificationResult = NotificationRepo.FindAll(e => e.UniqueId == id);
        if (notificationResult.HasErrors)
        {
            return InternalError<NotificationDto>(notificationResult.Errors);
        }
        var notification = notificationResult.ResultObject.FirstOrDefault();
        if (notification == null)
        {
            return InternalError<NotificationDto>("Notification not found");
        }
        notification.Message     = dto.Message;
        notification.ExpireAt    = dto.ExpireAt;
        notification.StartAt     = dto.StartAt;
        notification.IsPermanent = dto.IsPermanent;

        var updateResult = NotificationRepo.Update(notification, CurrentUserId);
        if (updateResult.HasErrors)
        {
            return InternalError<NotificationDto>(updateResult.Errors);
        }
        return Ok(updateResult.ResultObject);
    }


    [AppAuthorize(UserRole.Admin)]
    [HttpDelete("{id:Guid}")]
    public IActionResult Delete(Guid id)
    {
        var notificationResult = NotificationRepo.FindAll(e => e.UniqueId == id);
        if (notificationResult.HasErrors)
        {
            return InternalError<NotificationDto>(notificationResult.Errors);
        }
        var notification = notificationResult.ResultObject.FirstOrDefault();
        if (notification == null)
        {
            return InternalError<NotificationDto>("Notification not found");
        }

        var deleteResult = NotificationRepo.Delete(notification, CurrentUserId);
        if (deleteResult.HasErrors)
        {
            return InternalError<NotificationDto>(deleteResult.Errors);
        }

        return Ok(deleteResult.ResultObject);
    }
}