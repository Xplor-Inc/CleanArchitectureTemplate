using Microsoft.AspNetCore.Hosting;
using GenogramSystem.Core.Interfaces.Emails.Templates;
using GenogramSystem.Core.Interfaces.Utility;
using GenogramSystem.Core.Models.Configuration;

namespace Emails.Templates;
public class HtmlTemplate : IHtmlTemplate
{
    public EmailConfiguration   EmailConfiguration  { get; }
    public IWebHostEnvironment  Environment         { get; }
    public IUserAgentConductor  UserAgentConductor  { get; }

    public HtmlTemplate(
        EmailConfiguration      emailConfiguration,
        IWebHostEnvironment     environment,
        IUserAgentConductor     userAgentConductor)
    {
        EmailConfiguration  = emailConfiguration;
        Environment         = environment;
        UserAgentConductor  = userAgentConductor;
    }

    public string ResetPassword(Dictionary<string, string> substitutions)
    {
        return GetHtml(EmailConfiguration.Templates.ResetPasswordTemplate, substitutions);
    }

    public string EnquiryThanks(Dictionary<string, string> substitutions)
    {
        return GetHtml(EmailConfiguration.Templates.EnquiryTemplate, substitutions);
    }

    public string AccountActivation(Dictionary<string, string> substitutions)
    {
        return GetHtml(EmailConfiguration.Templates.AccountActivationTemplate, substitutions);
    }

    private string GetHtml(string path, Dictionary<string, string> substitutions)
    {
        string templatePath = $"{Environment.WebRootPath}/{path}";

        string template = File.ReadAllText(templatePath);

        substitutions.ToList().ForEach(x => template = template.Replace($"{{{{{x.Key}}}}}", x.Value));

        return template;

    }
}