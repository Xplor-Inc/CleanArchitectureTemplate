namespace CleanArchitectureTemplate.Core.Interfaces.Data;
public interface ICleanArchitectureTemplateContext : IContext
{
    IQueryable<AccountRecovery>         AccountRecoveries           { get; }
    IQueryable<ChangeLog>               ChangeLogs                  { get; }
    IQueryable<Counter>                 Counters                    { get; }
    IQueryable<Enquiry>                 Enquiries                   { get; }
    IQueryable<Notification>            Notifications               { get; }
    IQueryable<UserLogin>               UserLogins                  { get; }    
}