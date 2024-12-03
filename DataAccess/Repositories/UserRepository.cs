using Domain.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(BankContext repositoryContext)
            : base(repositoryContext)
        {
        }
        public async Task<User> GetByIdWithToken(string userId) => 
            await RepositoryContext.Set<User>().Include(x => x.RefreshTokens).AsNoTracking().FirstOrDefaultAsync(x => x.ClientId == userId);
        public async Task<User> GetByEmailWithToken(string email) => 
            await RepositoryContext.Set<User>().Include(x => x.RefreshTokens).AsNoTracking().FirstOrDefaultAsync(x => x.Email == email);
    }
}