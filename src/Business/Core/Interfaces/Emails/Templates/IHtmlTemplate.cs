namespace GenogramSystem.Core.Interfaces.Emails.Templates;
public interface IHtmlTemplate
{
    /// <summary>
    /// This is an Account Activation template. It required belows Dictionary as substitutions(Key, Value) pair <br />
    /// Name <br />
    /// AccountActivationLink 
    /// </summary>
    /// <param name="substitutions"></param>
    /// <returns></returns>
    string AccountActivation(Dictionary<string, string> substitutions);

    /// <summary>
    /// This is an account accativation template. It required belows Dictionary as substitutions(Key, Value) pair <br />
    /// Name  <br />
    /// Email <br />
    /// Subject <br />
    /// Message <br />
    /// OperatingSystem <br />
    /// BrowserName <br />
    /// IPAddress <br />
    /// Device  
    /// </summary>
    /// <param name="substitutions"></param>
    /// <returns></returns>
    string EnquiryThanks(Dictionary<string, string> substitutions);

    /// <summary>
    /// This is Reset Password template. It required belows Dictionary as substitutions(Key, Value) pair <br />
    /// PasswordResetUrl <br />
    /// Name <br />
    /// OperatingSystem <br />
    /// BrowserName <br />
    /// IPAddress <br />
    /// Device <br />
    /// </summary>
    /// <param name="substitutions"></param>
    /// <returns></returns>
    string ResetPassword(Dictionary<string, string> substitutions);

}
