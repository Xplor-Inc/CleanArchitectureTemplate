using GenogramSystem.Core.Conductors;
using GenogramSystem.Core.Conductors.Users;
using GenogramSystem.Core.Interfaces.Conductor;
using GenogramSystem.Core.Interfaces.Conductors.Accounts;
using GenogramSystem.Core.Interfaces.Utility;
using GenogramSystem.Core.Interfaces.Utility.Security;
using GenogramSystem.Core.Utilities;
using GenogramSystem.Core.Utilities.Security;

namespace GenogramSystem.Core.Extensions.Middleware;
public static class IServiceColletionsStartup
{
    public static void AddUtilityResolver(this IServiceCollection services)
    {
        services.AddScoped<IAccountConductor,               AccountConductor>();
        services.AddScoped<IEncryption,                     Encryption>();
        services.AddSingleton<IHttpContextAccessor,         HttpContextAccessor>();
        services.AddScoped<IUserAgentConductor,             UserAgentConductor>();

        services.AddScoped(typeof(IRepositoryConductor<>), typeof(RepositoryConductor<>));
    }
}