using ProjectCore.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ProjectCore.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetAsync(int ID);
        Task<User> GetSingleByFilterAsync(Expression<Func<User, bool>> filter);
        Task<IEnumerable<User>> GetManyByFilterAsync(Expression<Func<User, bool>> filter);
        Task<IEnumerable<User>> GetAllAsync();
        Task<int> CreateAsync(User entity);
        Task UpdateAsync(User entity);
        Task DeleteAsync(int ID);
    }
}
