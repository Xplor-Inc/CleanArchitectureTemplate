using GenogramSystem.Core.Models.Entities.Users;

namespace GenogramSystem.Core.Interfaces.Data;
public interface IDataContext<TUser> : IContext
        where TUser : User
{
    IQueryable<User>    Users   { get; }
}