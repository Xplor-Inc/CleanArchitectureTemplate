using GenogramSystem.SqlServer.Repositories;
using GenogramSystem.Core.Interfaces.Data;

namespace GenogramSystem.SqlServer.Extensions;
public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddSqlRepository(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        return services;
    }
}