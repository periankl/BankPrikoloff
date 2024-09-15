using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface IDepositService
    {
        Task<List<Deposit>> GetAll();
        Task<Deposit> GetById(string id);
        Task Create(Deposit model);
        Task Update(Deposit model);
        Task Delete(string id);
    }
}
