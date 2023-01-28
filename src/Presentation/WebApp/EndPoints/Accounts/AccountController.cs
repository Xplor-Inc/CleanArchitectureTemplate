using CleanArchitectureTemplate.Core.Interfaces.Conductors.Accounts;
using CleanArchitectureTemplate.Core.Models.Security;
using CleanArchitectureTemplate.WebApp.Models.Dtos.Accounts;
using CleanArchitectureTemplate.WebApp.Models.Dtos.Users;
using CleanArchitectureTemplate.WebApp.Utilities;
using System.Security.Claims;

namespace CleanArchitectureTemplate.WebApp.Endpoints;

[Route("api/1.0/account")]
public class AccountController : CleanArchitectureTemplateController
{
    #region Properties
    public IAccountConductor                    AccountConductor        { get; }
    public CookieAuthenticationConfiguration    CookieAuthentication    { get; }
    public ILogger<AccountController>           Logger                  { get; }
    public IMapper                              Mapper                  { get; }
    #endregion

    #region Contructor
    public AccountController(
        IAccountConductor                   accountConductor,
        CookieAuthenticationConfiguration   cookieAuthentication,
        ILogger<AccountController>          logger,
        IMapper                             mapper)
    {
        AccountConductor        = accountConductor;
        CookieAuthentication    = cookieAuthentication;
        Logger                  = logger;       
        Mapper                  = mapper;
    }
    #endregion

    #region Login     
    [HttpPost("authenticate")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        var loginResult = AccountConductor.Authenticate(loginDto.EmailAddress, loginDto.Password);
        if (loginResult.HasErrors)
        {
            var error = loginResult.GetErrors();
            Logger.LogWarning("User login failed for {EmailAddress}, Error: {error}", loginDto.EmailAddress, error);
            return InternalError<LoginDto>(loginResult.Errors);
        }
        var user    = loginResult.ResultObject;
        if (user == null) { return InternalError<LoginDto>(loginResult.Errors); }
        var userDto = Mapper.Map<UserDto>(user);
        Logger.LogInformation("User loggedIn successfully with email {EmailAddress}.", loginDto.EmailAddress);
        return await CreateCookies(userDto, user.SecurityStamp);
    }

    #endregion

    #region Change Password     
    [HttpPost("changepassword")]
    [AppAuthorize]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto dto)
    {
        var loginResult = AccountConductor.ChangePassword(CurrentUserId, dto.OldPassword, dto.NewPassword);
        if (loginResult.HasErrors)
        {
            return InternalError<LoginDto>(loginResult.Errors);
        }
        var user = loginResult.ResultObject;
        if (user == null) { return InternalError<LoginDto>(loginResult.Errors); }
        var userDto = Mapper.Map<UserDto>(user);
        return await CreateCookies(userDto, user.SecurityStamp);
    }

    #endregion

    [HttpPut("logout")]
    public async Task<IActionResult> Logout()
    {
        try
        {
            if (User.IsAuthenticated())
            {
                var logoutResult = AccountConductor.UpdateLogout(CurrentUserId);
                if (logoutResult.HasErrors)
                {
                    var error = logoutResult.GetErrors();
                    Logger.LogWarning("User login failed for {CurrentUserId}, Error: {error}", CurrentUserId, error);
                }
            }
            await HttpContext.SignOutAsync(CookieAuthentication.AuthenticationScheme);
            return Ok(true);
        }
        catch (Exception ex)
        {
            return InternalError<UserDto>(ex.Message);
        }
    }
    private async Task<IActionResult> CreateCookies(UserDto user, string securityStamp)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Email,                 user.EmailAddress),
            new Claim(ClaimTypes.Gender,                user.Gender.ToString()),
            new Claim(ClaimTypes.Role,                  user.Role.ToString()),
            new Claim(ClaimTypes.PrimarySid,            user.Id.ToString()),
            new Claim(ClaimTypes.Name,                  $"{user.FirstName} {user.LastName}"),
            new Claim(ClaimTypes.AuthenticationInstant, securityStamp)
        };
        var userPrincipal   = new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthentication.AuthenticationScheme));
        var properties      = AuthenticationUtils.GetAuthenticationProperties();
        await HttpContext.SignInAsync(CookieAuthentication.AuthenticationScheme, userPrincipal, properties);

        return Ok(user);
    }
}