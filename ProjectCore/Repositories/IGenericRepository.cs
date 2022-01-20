using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProjectCore.Repositories
{
    public interface IGenericRepository<TEntity>
    {
        Task<TEntity> GetAsync(int ID);
        Task<TEntity> GetByFilterAsync(Expression<Func<TEntity,bool>> filter);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task CreateAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(int ID);
    }
}
