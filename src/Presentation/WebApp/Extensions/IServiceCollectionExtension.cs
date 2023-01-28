using CleanArchitectureTemplate.Core.Interfaces.Data;
using CleanArchitectureTemplate.Core.Models.Configuration;
using CleanArchitectureTemplate.Core.Models.Entities.Users;
using CleanArchitectureTemplate.Core.Models.Security;
using CleanArchitectureTemplate.WebApp.Utilities;

namespace CleanArchitectureTemplate.WebApp.Extensions;
public static class IServiceCollectionExtension
{
    public static void AddCookieAuthentication(this IServiceCollection services, IConfigurationRoot configuration)
    {
        var cookieConfig = configuration.GetSection("Authentication").GetSection("Cookie").Get<CookieAuthenticationConfiguration>();
        if(cookieConfig is null)
        {
            throw new NullReferenceException("Cookies is not configured");
        }
        services.AddSingleton((sp) => cookieConfig);

        var cookie          = new CookieBuilder()
        {
            Name        = cookieConfig.CookieName,
            SameSite    = SameSiteMode.Strict,
        };
        var cookieEvents = new CookieAuthenticationEvents
        {
            OnRedirectToAccessDenied = context =>
            {
                context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return Task.CompletedTask;
            },
            OnValidatePrincipal = PrincipalValidator.ValidateAsync
        };

        services.AddAuthentication(cookieConfig.AuthenticationScheme)
            .AddCookie(cookieConfig.AuthenticationScheme, options =>
            {
                options.Cookie = cookie;
                options.Events = cookieEvents;
            });
    }

    public static void AddContexts(this IServiceCollection services, string connectionString)
    {
        var loggerFactory = new Serilog.Extensions.Logging.SerilogLoggerFactory(Log.Logger, false);
        services.AddDbContext<CleanArchitectureTemplateContext>(ServiceLifetime.Scoped);
        services.AddScoped((sp) => new CleanArchitectureTemplateContext(connectionString, loggerFactory));
        services.AddScoped<DataContext<User>>((sp) => new CleanArchitectureTemplateContext(connectionString, loggerFactory));
        services.AddScoped<IDataContext<User>>((sp) => new CleanArchitectureTemplateContext(connectionString, loggerFactory));
        services.AddScoped<IContext>((sp) => new CleanArchitectureTemplateContext(connectionString, loggerFactory));
        services.AddScoped<ICleanArchitectureTemplateContext>((sp) => new CleanArchitectureTemplateContext(connectionString, loggerFactory));
    }

    public static void AddConfigurationFiles(this IServiceCollection services, IConfigurationRoot configuration)
    {
        var fileConfiguration = configuration.GetSection("StaticFileConfiguration").Get<StaticFileConfiguration>();
        if (fileConfiguration is null)
        {
            throw new NullReferenceException("FileConfiguration[StaticFileConfiguration] node is not configured");
        }
        services.AddSingleton((sp) => fileConfiguration);
    }
}