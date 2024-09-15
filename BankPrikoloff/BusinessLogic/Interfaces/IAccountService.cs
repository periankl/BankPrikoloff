using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface IAccountService
    {
        Task<List<Account>> GetAll();
        Task<Account> GetById(string id);
        Task Create(Account model);
        Task Update(Account model);
        Task Delete(string id);
    }
}
