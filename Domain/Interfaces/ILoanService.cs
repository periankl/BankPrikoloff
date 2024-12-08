using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface ILoanService
    {
        Task<List<Loan>> GetAll();
        Task<Loan> GetById(string id);
        Task<Loan> GetByAccountId(string accountId);
        Task Create(Loan model);
        Task Update(Loan model);
        Task Delete(string id);
    }
}