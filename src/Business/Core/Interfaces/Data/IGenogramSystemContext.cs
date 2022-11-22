using GenogramSystem.Core.Models.Entities.RequestLogs;

namespace GenogramSystem.Core.Interfaces.Data;
public interface IGenogramSystemContext : IContext
{
    IQueryable<AccountRecovery>         AccountRecoveries           { get; }
    IQueryable<ChangeLog>               ChangeLogs                  { get; }
    IQueryable<Counter>                 Counters                    { get; }
    IQueryable<Enquiry>                 Enquiries                   { get; }
    IQueryable<RequestLog>              RequestLogs                 { get; }
    IQueryable<UserLogin>               UserLogins                  { get; }    

}