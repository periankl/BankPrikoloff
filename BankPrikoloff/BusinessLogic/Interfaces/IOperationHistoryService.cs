using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface IOperationHistoryService
    {
        Task<List<OperationHistory>> GetAll();
        Task<OperationHistory> GetById(string id);
        Task Create(OperationHistory model);
        Task Update(OperationHistory model);
        Task Delete(string id);
    }
}
