using GenogramSystem.Core.Interfaces.Emails.EmailHandler;
using GenogramSystem.Core.Interfaces.Emails.Templates;
using GenogramSystem.Emails.Email;
using Emails.Templates;
using Microsoft.Extensions.DependencyInjection;

namespace GenogramSystem.Emails.Extensions.Middleware;
public static class IServiceColletionsStartup
{
    public static void AddEmailHandler(this IServiceCollection services)
    {
        services.AddScoped<IEmailHandler,           EmailHandler>();
        services.AddScoped<IHtmlTemplate,           HtmlTemplate>();
    }
}