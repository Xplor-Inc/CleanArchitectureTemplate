namespace CleanArchitectureTemplate.WebApp.EndPoints.Audits
{
    [Route("api/1.0/audit/changelog")]
    [AppAuthorize]
    public class ChangeLogController : CleanArchitectureTemplateController
    {
        #region Properties
        private IRepositoryConductor<ChangeLog> ChangeLogRepo   { get; }
        public IRepositoryConductor<User>       UserRepo        { get; }
        #endregion

        #region Constructor
        public ChangeLogController(
            IRepositoryConductor<ChangeLog> changeLogRepo,
            IRepositoryConductor<User>      userRepo)
        {
            ChangeLogRepo   = changeLogRepo;
            UserRepo        = userRepo;
        }
        #endregion


        [HttpGet]
        public IActionResult Index(long primaryKey, string entityName)
        {
            Expression<Func<ChangeLog, bool>> predicate = (e) => e.EntityName == entityName && e.PrimaryKey == primaryKey;
            
            var changeLogsResult = ChangeLogRepo.FindAll(filter: predicate, e => e.OrderBy("CreatedOn", "DESC"),includeProperties: "CreatedBy");
            if (changeLogsResult.HasErrors)
            {
                return InternalError<NotificationDto>(changeLogsResult.Errors);
            }
            var changeLogs = changeLogsResult.ResultObject.ToList();
            if(changeLogs.Count > 0)
            {
                var userProperties = changeLogs.Where(e => e.PropertyName == "UpdatedById")
                                   .Select(s => s.PropertyName).ToList();
                if (userProperties.Count > 0)
                {
                    var users = UserRepo.FindAll().ResultObject.ToList();
                    foreach (var change in changeLogs)
                    {
                        if (change.PropertyName == "UpdatedById")
                        {
                            var oldUser = users.FirstOrDefault(e => e.Id.ToString() == change.OldValue);
                            change.OldValue = $"{oldUser?.FirstName} {oldUser?.LastName}";
                            
                            var newUser = users.FirstOrDefault(e => e.Id.ToString() == change.NewValue);
                            change.NewValue = $"{newUser?.FirstName} {newUser?.LastName}";
                        }
                    }
                }
            }
            return Ok(changeLogs);
        }


    }
}
