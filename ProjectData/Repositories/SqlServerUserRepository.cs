using Microsoft.EntityFrameworkCore;
using ProjectCore.Models;
using ProjectCore.Repositories;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProjectData.Repositories
{
    public class SqlServerUserRepository : IUserRepository
    {
        private readonly ProjectDBContext _dBContext;

        public SqlServerUserRepository(ProjectDBContext dBContext)
        {
            _dBContext = dBContext;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _dBContext.Set<User>().ToListAsync();
        }

        public async Task<User> GetAsync(int ID)
        {
            return await _dBContext.Set<User>().FirstOrDefaultAsync(user => user.ID == ID);
        }

        public async Task<User> GetByFilterAsync(Expression<Func<User, bool>> filter)
        {
            return await _dBContext.Set<User>().FirstOrDefaultAsync(filter);
        }

        public async Task CreateAsync(User entity)
        {
            await _dBContext.Users.AddAsync(entity);
            await _dBContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(User entity)
        {
            _dBContext.Users.Update(entity);
            await _dBContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int ID)
        {
            _dBContext.Users.Remove(new User { ID = ID });
            await _dBContext.SaveChangesAsync();
        }
    }
}
