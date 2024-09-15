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
    public class LoanTypeService : ILoanTypeService
    {
        private IRepositoryWrapper _repositoryWrapper;

        public LoanTypeService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public Task<List<LoanType>> GetAll()
        {
            return _repositoryWrapper.LoanType.FindAll().ToListAsync();
        }

        public Task<LoanType> GetById(int id)
        {
            var loanType = _repositoryWrapper.LoanType
                .FindByCondition(x => x.LoanTypeId == id).First();
            return Task.FromResult(loanType);
        }

        public Task Create(LoanType model)
        {
            _repositoryWrapper.LoanType.Create(model);
            _repositoryWrapper.Save();
            return Task.CompletedTask;
        }

        public Task Update(LoanType model)
        {
            _repositoryWrapper.LoanType.Update(model);
            _repositoryWrapper.Save();
            return Task.CompletedTask;
        }

        public Task Delete(int id)
        {
            var loanType = _repositoryWrapper.LoanType
                .FindByCondition(x => x.LoanTypeId == id).First();

            _repositoryWrapper.LoanType.Delete(loanType);
            _repositoryWrapper.Save();
            return Task.CompletedTask;
        }
    }
}
