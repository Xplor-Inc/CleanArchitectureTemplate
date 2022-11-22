using GenogramSystem.Core.Enumerations;
using GenogramSystem.Core.Interfaces.Conductors.Accounts;
using GenogramSystem.Core.Interfaces.Emails.EmailHandler;
using GenogramSystem.Core.Interfaces.Emails.Templates;
using GenogramSystem.Core.Models.Configuration;

namespace GenogramSystem.WebApp.EndPoints.Users;

[Route("api/1.0/users")]
[AppAuthorize(UserRole.Admin)]
public class UsersController : GenogramSystemController
{
    #region Properties
    private IAccountConductor           AccountConductor    { get; }
    public EmailConfiguration           EmailConfiguration  { get; }
    private IEmailHandler               EmailHandler        { get; }
    private IHtmlTemplate               HtmlTemplate        { get; }
    private ILogger<UsersController>    Logger              { get; }
    private IMapper                     Mapper              { get; }
    private StaticFileConfiguration     StaticFile          { get; }
    private IRepositoryConductor<User>  UserConductor       { get; }
    #endregion

    #region Constructor
    public UsersController(
        IAccountConductor           accountConductor,
        EmailConfiguration          emailConfiguration,
        IEmailHandler               emailHandler,
        IHtmlTemplate               htmlTemplate,
        ILogger<UsersController>    logger,
        IMapper                     mapper,
        StaticFileConfiguration     staticFile,
        IRepositoryConductor<User>  userConductor)
    {
        AccountConductor    = accountConductor;
        EmailConfiguration  = emailConfiguration;
        EmailHandler        = emailHandler;
        HtmlTemplate        = htmlTemplate;
        Logger              = logger;
        Mapper              = mapper;
        StaticFile          = staticFile;
        UserConductor       = userConductor;
    }
    #endregion


    [HttpGet]
    public IActionResult Index(
        string?     searchText,
        bool        includeDeleted,
        UserRole?   userRole,
        string      sortBy      = "FirstName",
        string      sortOrder   = "ASC",
        int         skip        = 0,
        int         take        = 5)
    {
        Expression<Func<User, bool>> predicate = e => true;

        if (!includeDeleted)
        {
            predicate = predicate.AndAlso(e => e.DeletedOn == null);
        }
       
        if (userRole.HasValue)
        {
            predicate = predicate.AndAlso(e => e.Role == userRole.Value);
        }
        if (!string.IsNullOrEmpty(searchText))
        {
            predicate = predicate.AndAlso(e => $"{e.FirstName} {e.LastName}".Contains(searchText) || e.EmailAddress.Contains(searchText));
        }

        var userResult = UserConductor.FindAll(filter: predicate, e => e.OrderBy(sortBy, sortOrder), skip: skip, take: take);
        if (userResult.HasErrors)
        {
            return InternalError<UserDto>(userResult.Errors);
        }
        var users = userResult.ResultObject.ToList();
        var rowCount = UserConductor.FindAll(filter: predicate).ResultObject.Select(e => e.Id).Count();
        var dtos = Mapper.Map<List<UserDto>>(users);
        return Ok(dtos, rowCount);
    }


    [HttpPost]
    public IActionResult Post([FromBody] UserDto dto)
    {       
        var user = Mapper.Map<User>(dto);
        user.Role       = UserRole.Member;
        user.ImagePath  = StaticFile.ProfileImageName;
        var createResult = AccountConductor.CreateAccount(user, CurrentUserId);
        if (createResult.HasErrors)
        {
            return InternalError<UserDto>(createResult.Errors);
        }

        var accountActivationLink = $"{EmailConfiguration.Templates.ResetPasswordLink}/{user.UniqueId}/{user.SecurityStamp}/{user.EmailAddress}";
        Dictionary<string, string> substitutions = new()
        {
            { "Name", user.FirstName },
            { "AccountActivationLink", accountActivationLink }
        };
        string emailbody = HtmlTemplate.AccountActivation(substitutions);
        bool emailSent = EmailHandler.Send(emailbody, "Account Activation", new string[] { user.EmailAddress });

        user.ActivationEmailSent = emailSent;
        if (emailSent)
            Logger.LogInformation("Account Activation email sent successfully to {user.EmailAddress}", user.EmailAddress);
        else
            Logger.LogInformation("Account Activation email sending error to {user.EmailAddress}", user.EmailAddress);

        var updateResult = AccountConductor.CreateAccount(user, CurrentUserId);
        if (updateResult.HasErrors)
        {
            return InternalError<UserDto>(createResult.Errors);
        }

        return Ok(createResult.ResultObject);
    }


    [HttpPut("{id:Guid}")]
    public IActionResult Put(Guid id, [FromBody] UserDto dto)
    {
        var userResult = UserConductor.FindAll(e => e.UniqueId == id);
        if (userResult.HasErrors)
        {
            return InternalError<UserDto>(userResult.Errors);
        }
        var user = userResult.ResultObject.FirstOrDefault();
        if (user == null)
        {
            return InternalError<UserDto>("Invalid user");
        }
        if (user.Id == CurrentUserId) { return InternalError<UserDto>("Please update your details in profile page"); }
        user.IsActive   = dto.IsActive;
        user.FirstName  = dto.FirstName;
        user.LastName   = dto.LastName;
        user.Role       = dto.Role;
        user.Gender     = dto.Gender;

        var updateResult = UserConductor.Update(user, CurrentUserId);
        if (updateResult.HasErrors)
        {
            return InternalError<UserDto>(updateResult.Errors);
        }
        return Ok(updateResult.ResultObject);
    }


    [HttpDelete("{id:Guid}")]
    public IActionResult Delete(Guid id)
    {
        var userResult = UserConductor.FindAll(e => e.UniqueId == id);
        if (userResult.HasErrors)
        {
            return InternalError<UserDto>(userResult.Errors);
        }
        var user = userResult.ResultObject.FirstOrDefault();
        if (user == null)
        {
            return InternalError<UserDto>("Invalid user");
        }
        if (user.Id == CurrentUserId) { return InternalError<UserDto>("You can't delete self account"); }

        var updateResult = UserConductor.Delete(user, CurrentUserId);
        if (updateResult.HasErrors)
        {
            return InternalError<UserDto>(updateResult.Errors);
        }

        return Ok(updateResult.ResultObject);
    }
}