using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface IDepositTypeService
    {
        Task<List<DepositType>> GetAll();
        Task<DepositType> GetById(int id);
        Task Create(DepositType model);
        Task Update(DepositType model);
        Task Delete(int id);
    }
}
