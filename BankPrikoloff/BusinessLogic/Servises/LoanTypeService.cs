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
    public class LoanTypeService : ILoanTypeService
    {
        private IRepositoryWrapper _repositoryWrapper;

        public LoanTypeService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<List<LoanType>> GetAll()
        {
            return await _repositoryWrapper.LoanType.FindAll();
        }

        public async Task<LoanType> GetById(int id)
        {
            var loanType = await _repositoryWrapper.LoanType
                .FindByCondition(x => x.LoanTypeId == id);
            return loanType.First();
        }

        public async Task Create(LoanType model)
        {
            await _repositoryWrapper.LoanType.Create(model);
            _repositoryWrapper.Save();
        }

        public async Task Update(LoanType model)
        {
            _repositoryWrapper.LoanType.Update(model);
            _repositoryWrapper.Save();
        }

        public async Task Delete(int id)
        {
            var loanType = await _repositoryWrapper.LoanType
                .FindByCondition(x => x.LoanTypeId == id);

            _repositoryWrapper.LoanType.Delete(loanType.First());
            _repositoryWrapper.Save();
        }
    }
}
