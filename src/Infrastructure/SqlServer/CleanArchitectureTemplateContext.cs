using CleanArchitectureTemplate.Core.Interfaces.Data;
using CleanArchitectureTemplate.SqlServer.Extensions;
using CleanArchitectureTemplate.SqlServer.Maps.Audits;
using CleanArchitectureTemplate.SqlServer.Maps.Enquiries;
using CleanArchitectureTemplate.SqlServer.Maps.Notifications;
using CleanArchitectureTemplate.SqlServer.Maps.Users;

namespace CleanArchitectureTemplate.SqlServer;
public class CleanArchitectureTemplateContext : DataContext<User>, ICleanArchitectureTemplateContext
{
    #region Properties
    public DbSet<AccountRecovery>       AccountRecoveries       { get; set; }
    public DbSet<Counter>               Counters                { get; set; }
    public DbSet<Enquiry>               Enquiries               { get; set; }
    public DbSet<Notification>          Notifications           { get; set; }
    public DbSet<UserLogin>             UserLogins              { get; set; }
    #endregion

    #region Constructor
    public CleanArchitectureTemplateContext(string connectionString, ILoggerFactory loggerFactory)
        : base(connectionString, loggerFactory)
    {
    }

    public CleanArchitectureTemplateContext(IConnection connection, ILoggerFactory loggerFactory)
        : base(connection, loggerFactory)
    {
    }

    #endregion

    #region ICleanArchitectureTemplateContext Implementation
    IQueryable<AccountRecovery>         ICleanArchitectureTemplateContext.AccountRecoveries        => AccountRecoveries;
    IQueryable<ChangeLog>               ICleanArchitectureTemplateContext.ChangeLogs               => ChangeLogs;
    IQueryable<Counter>                 ICleanArchitectureTemplateContext.Counters                 => Counters;
    IQueryable<Enquiry>                 ICleanArchitectureTemplateContext.Enquiries                => Enquiries;
    IQueryable<Notification>            ICleanArchitectureTemplateContext.Notifications            => Notifications;
    IQueryable<UserLogin>               ICleanArchitectureTemplateContext.UserLogins               => UserLogins;
    #endregion

    public override void ConfigureMappings(ModelBuilder modelBuilder)
    {
        modelBuilder.AddMapping(new AccountRecoveryMap());
        modelBuilder.AddMapping(new ChangeLogMap());
        modelBuilder.AddMapping(new CounterMap());
        modelBuilder.AddMapping(new EnquiryMap());
        modelBuilder.AddMapping(new NotificationMap());
        modelBuilder.AddMapping(new UserMap());
        modelBuilder.AddMapping(new UserLoginMap());

        base.ConfigureMappings(modelBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}