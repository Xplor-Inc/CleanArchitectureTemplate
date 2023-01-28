using CleanArchitectureTemplate.Core.Models.Entities.Users;

namespace CleanArchitectureTemplate.Core.Interfaces.Data;
public interface IDataContext<TUser> : IContext
        where TUser : User
{
    IQueryable<User>    Users   { get; }
}