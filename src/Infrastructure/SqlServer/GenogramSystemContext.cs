using GenogramSystem.Core.Interfaces.Data;
using GenogramSystem.Core.Models.Entities.RequestLogs;
using GenogramSystem.SqlServer.Extensions;
using GenogramSystem.SqlServer.Maps.Audits;
using GenogramSystem.SqlServer.Maps.Enquiries;
using GenogramSystem.SqlServer.Maps.Users;

namespace GenogramSystem.SqlServer;
public class GenogramSystemContext : DataContext<User>, IGenogramSystemContext
{
    #region Properties
    public DbSet<AccountRecovery>       AccountRecoveries       { get; set; }
    public DbSet<Counter>               Counters                { get; set; }
    public DbSet<Enquiry>               Enquiries               { get; set; }
    public DbSet<RequestLog>            RequestLogs             { get; set; }
    public DbSet<UserLogin>             UserLogins              { get; set; }
    #endregion

    #region Constructor
    public GenogramSystemContext(string connectionString, ILoggerFactory loggerFactory)
        : base(connectionString, loggerFactory)
    {
    }

    public GenogramSystemContext(IConnection connection, ILoggerFactory loggerFactory)
        : base(connection, loggerFactory)
    {
    }

    #endregion

    #region IGenogramSystemContext Implementation
    IQueryable<AccountRecovery>         IGenogramSystemContext.AccountRecoveries        => AccountRecoveries;
    IQueryable<ChangeLog>               IGenogramSystemContext.ChangeLogs               => ChangeLogs;
    IQueryable<Counter>                 IGenogramSystemContext.Counters                 => Counters;
    IQueryable<Enquiry>                 IGenogramSystemContext.Enquiries                => Enquiries;
    IQueryable<RequestLog>              IGenogramSystemContext.RequestLogs              => RequestLogs;
    IQueryable<UserLogin>               IGenogramSystemContext.UserLogins               => UserLogins;

    #endregion

    public override void ConfigureMappings(ModelBuilder modelBuilder)
    {
        modelBuilder.AddMapping(new AccountRecoveryMap());
        modelBuilder.AddMapping(new ChangeLogMap());
        modelBuilder.AddMapping(new CounterMap());
        modelBuilder.AddMapping(new EnquiryMap());
        modelBuilder.AddMapping(new UserMap());
        modelBuilder.AddMapping(new UserLoginMap());
        modelBuilder.AddMapping(new RequestLogMap());

        base.ConfigureMappings(modelBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}