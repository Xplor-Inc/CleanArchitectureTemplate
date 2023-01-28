using CleanArchitectureTemplate.SqlServer.Repositories;
using CleanArchitectureTemplate.Core.Interfaces.Data;

namespace CleanArchitectureTemplate.SqlServer.Extensions;
public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddSqlRepository(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        return services;
    }
}