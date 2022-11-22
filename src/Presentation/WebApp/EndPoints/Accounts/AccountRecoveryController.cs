using GenogramSystem.Core.Interfaces.Conductors.Accounts;
using GenogramSystem.Core.Interfaces.Emails.EmailHandler;
using GenogramSystem.Core.Interfaces.Emails.Templates;
using GenogramSystem.Core.Interfaces.Utility;
using GenogramSystem.Core.Models.Configuration;
using GenogramSystem.Core.Models.Entities.Users;
using GenogramSystem.WebApp.Models.Dtos.Accounts;

namespace GenogramSystem.WebApp.EndPoints.Accounts;
[Route("api/1.0/accountrecovery")]
public class AccountRecoveryController : GenogramSystemController
{
    #region Properties
    public IAccountConductor                        AccountConductor            { get; }
    public IRepositoryConductor<AccountRecovery>    AccountRecoveryRepository   { get; }
    public EmailConfiguration                       EmailConfiguration          { get; } 
    public IEmailHandler                            EmailHandler                { get; }
    public IWebHostEnvironment                      Environment                 { get; }
    public IHtmlTemplate                            HtmlTemplate                { get; }
    public IUserAgentConductor                      UserAgentConductor          { get; }
    public IRepositoryConductor<User>               UserRepository              { get; }
    #endregion

    #region Contructor
    public AccountRecoveryController(
        IRepositoryConductor<AccountRecovery>   accountRecoveryRepository,
        IAccountConductor                       accountConductor,
        EmailConfiguration                      emailConfiguration,
        IEmailHandler                           emailHandler,
        IWebHostEnvironment                     environment,
        IHtmlTemplate                           htmlTemplate,
        IUserAgentConductor                     userAgentConductor,
        IRepositoryConductor<User>              userRepository)
    {
        AccountRecoveryRepository   = accountRecoveryRepository;
        AccountConductor            = accountConductor;
        EmailConfiguration          = emailConfiguration;
        EmailHandler                = emailHandler;
        Environment                 = environment;
        HtmlTemplate                = htmlTemplate;
        UserAgentConductor          = userAgentConductor;
        UserRepository              = userRepository;
    }
    #endregion

    [HttpPost("forgetpassword")]
    public IActionResult ForgetPassword([FromBody] ForgetPasswordDto dto)
    {
        var userResult = UserRepository.FindAll(w => w.EmailAddress == dto.EmailAddress && w.DeletedOn == null && w.IsAccountActivated && w.IsActive);
        if (userResult.HasErrors)
        {
            return InternalError<ForgetPasswordDto>(userResult.Errors);
        }
        var user = userResult.ResultObject.FirstOrDefault();
        if (user == null)
        {
            return InternalError<ForgetPasswordDto>("Invalid Email Address");
        }
        var accountRecovery = new AccountRecovery
        {
            CreatedById     = user.Id,
            ResetLink       = $"{user.UniqueId}/{user.SecurityStamp}/{user.EmailAddress}",
            ResetLinkSentAt = DateTime.Now,
            UserId          = user.Id,
        };
        var accountRecoveryCreeateResult = AccountRecoveryRepository.Create(accountRecovery, user.Id);
        if (accountRecoveryCreeateResult.HasErrors)
        {
            return InternalError<ForgetPasswordDto>(accountRecoveryCreeateResult.Errors);
        }

        var accountActivationLink = $"{EmailConfiguration.Templates.ResetPasswordLink}/{user.UniqueId}/{user.SecurityStamp}/{user.EmailAddress}";
        var (ipAddress, operatingSystem, browser, device) = UserAgentConductor.GetUserAgent(HttpContext);
        Dictionary<string, string> substitutions = new()
        {
            { "Name",               user.FirstName },
            { "PasswordResetUrl",   accountActivationLink },
            { "OperatingSystem",    operatingSystem},
            { "BrowserName",        browser},
            { "IPAddress",          ipAddress},
            { "Device",             device}
        };
        string emailbody = HtmlTemplate.ResetPassword(substitutions);
        bool emailSent = EmailHandler.Send(emailbody, "Password Recovery", new string[] { user.EmailAddress });

        accountRecovery.EmailSent = emailSent;
        var accountRecoveryUpdateResult = AccountRecoveryRepository.Update(accountRecovery, user.Id);
        if (accountRecoveryUpdateResult.HasErrors)
        {
            return InternalError<ForgetPasswordDto>(accountRecoveryUpdateResult.Errors);
        }

        return Ok(true);
    }

    [HttpPost("resetpassword")]
    public IActionResult ValidateEmailLink([FromBody] ValidateEmailLinkDto dto)
    {
        var accountRecoveryResult = AccountConductor.ValidateEmailLink(dto.EmailAddress, dto.Resetlink);
        if (accountRecoveryResult.HasErrors)
        {
            return InternalError<ForgetPasswordDto>(accountRecoveryResult.Errors);
        }
        return Ok(true);
    }

    [HttpPut("resetpassword")]
    public IActionResult ResetPasswordByEmailLink([FromBody] ResetPasswordWithEmail dto)
    {
        var accountRecoveryResult = AccountConductor.ResetPasswordByEmailLink(dto.EmailAddress, dto.Resetlink, dto.Password);
        if (accountRecoveryResult.HasErrors)
        {
            return InternalError<ForgetPasswordDto>(accountRecoveryResult.Errors);
        }
        return Ok(true);
    }

    #region Activate Account

    [HttpPost("accountactivation")]
    public IActionResult AccountActivation([FromBody] ValidateEmailLinkDto dto)
    {
        var accountRecoveryResult = AccountConductor.IsActivationLinkValid(dto.EmailAddress, dto.Resetlink);
        if (accountRecoveryResult.HasErrors)
        {
            return InternalError<ForgetPasswordDto>(accountRecoveryResult.Errors);
        }
        return Ok(true);
    }

    [HttpPut("accountactivation")]
    public IActionResult AccountActivation([FromBody] ResetPasswordWithEmail dto)
    {
        var accountRecoveryResult = AccountConductor.ActivateAccount(dto.EmailAddress, dto.Resetlink, dto.Password);
        if (accountRecoveryResult.HasErrors)
        {
            return InternalError<ForgetPasswordDto>(accountRecoveryResult.Errors);
        }
        return Ok(true);
    }
    #endregion
}