using BusinessLogic.Interfaces;
using Domain.Models;
using Domain.Interfaces;
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

        public async Task<List<Loan>> GetAll()
        {
            return await _repositoryWrapper.Loan.FindAll();
        }

        public async Task<Loan> GetById(string id)
        {
            var loan = await _repositoryWrapper.Loan
                .FindByCondition(x => x.LoanId == id);
            return loan.First();
        }

        public async Task Create(Loan model)
        {
            await _repositoryWrapper.Loan.Create(model);
            _repositoryWrapper.Save();
        }

        public async Task Update(Loan model)
        {
            _repositoryWrapper.Loan.Update(model);
            _repositoryWrapper.Save();
        }

        public async Task Delete(string id)
        {
            var loan = await _repositoryWrapper.Loan
                .FindByCondition(x => x.LoanId == id);

            _repositoryWrapper.Loan.Delete(loan.First());
            _repositoryWrapper.Save();
        }
    }
}
