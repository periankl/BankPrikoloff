using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface ILoanTypeService
    {
        Task<List<LoanType>> GetAll();
        Task<LoanType> GetById(int id);
        Task Create(LoanType model);
        Task Update(LoanType model);
        Task Delete(int id);
    }
}