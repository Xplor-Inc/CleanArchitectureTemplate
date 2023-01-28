using CleanArchitectureTemplate.Core.Conductors;
using CleanArchitectureTemplate.Core.Conductors.Users;
using CleanArchitectureTemplate.Core.Interfaces.Conductor;
using CleanArchitectureTemplate.Core.Interfaces.Conductors.Accounts;
using CleanArchitectureTemplate.Core.Interfaces.Utility;
using CleanArchitectureTemplate.Core.Interfaces.Utility.Security;
using CleanArchitectureTemplate.Core.Utilities;
using CleanArchitectureTemplate.Core.Utilities.Security;

namespace CleanArchitectureTemplate.Core.Extensions.Middleware;
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