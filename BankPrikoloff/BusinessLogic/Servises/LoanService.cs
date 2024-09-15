using BusinessLogic.Interfaces;
using DataAccess.Models;
using DataAccess.Wrapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Servises
{
    public class LoanService : ILoanService
    {
        private IRepositoryWrapper _repositoryWrapper;

        public LoanService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public Task<List<Loan>> GetAll()
        {
            return _repositoryWrapper.Loan.FindAll().ToListAsync();
        }

        public Task<Loan> GetById(string id)
        {
            var loan = _repositoryWrapper.Loan
                .FindByCondition(x => x.LoanId == id).First();
            return Task.FromResult(loan);
        }

        public Task Create(Loan model)
        {
            _repositoryWrapper.Loan.Create(model);
            _repositoryWrapper.Save();
            return Task.CompletedTask;
        }

        public Task Update(Loan model)
        {
            _repositoryWrapper.Loan.Update(model);
            _repositoryWrapper.Save();
            return Task.CompletedTask;
        }

        public Task Delete(string id)
        {
            var loan = _repositoryWrapper.Loan
                .FindByCondition(x => x.LoanId == id).First();

            _repositoryWrapper.Loan.Delete(loan);
            _repositoryWrapper.Save();
            return Task.CompletedTask;
        }
    }
}
