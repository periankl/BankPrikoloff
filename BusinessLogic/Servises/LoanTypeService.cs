using BusinessLogic.Interfaces;
using Domain.Interfaces;
using Domain.Models;
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
            var model = await _repositoryWrapper.LoanType
                .FindByCondition(x => x.LoanTypeId == id);
            if (model is null || model.Count == 0)
            {
                throw new ArgumentException("Not found");
            }
            return model.First();
        }

        public async Task Create(LoanType model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            if (string.IsNullOrEmpty(model.Name))
            {
                throw new ArgumentException(nameof(model.Name));
            }
            if (model.InterestRate < 0)
            {
                throw new ArgumentException(nameof(model.InterestRate));
            }
            if (model.MaxLoanAmount < 0)
            {
                throw new ArgumentException(nameof(model.MaxLoanAmount));
            }
            await _repositoryWrapper.LoanType.Create(model);
            _repositoryWrapper.Save();
        }

        public async Task Update(LoanType model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            if (string.IsNullOrEmpty(model.Name))
            {
                throw new ArgumentException(nameof(model.Name));
            }
            if (model.InterestRate < 0)
            {
                throw new ArgumentException(nameof(model.InterestRate));
            }
            if (model.MaxLoanAmount < 0)
            {
                throw new ArgumentException(nameof(model.MaxLoanAmount));
            }
            _repositoryWrapper.LoanType.Update(model);
            _repositoryWrapper.Save();
        }

        public async Task Delete(int id)
        {
            var model = await _repositoryWrapper.LoanType
                .FindByCondition(x => x.LoanTypeId == id);
            if (model is null || model.Count == 0)
            {
                throw new ArgumentException("Not found");
            }
            _repositoryWrapper.LoanType.Delete(model.First());
            _repositoryWrapper.Save();
        }
    }
}