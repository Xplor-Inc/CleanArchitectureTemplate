using CleanArchitectureTemplate.SqlServer.Maps;

namespace CleanArchitectureTemplate.SqlServer.Extensions;
public static class ModelBuilderExtensions
{
    public static void AddMapping<TEntity>(this ModelBuilder builder, Map<TEntity> map) where TEntity : class
    {
        builder.Entity<TEntity>(map.Configure);
    }
}