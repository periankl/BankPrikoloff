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
    public class DepositTypeService : IDepositTypeService
    {
        private IRepositoryWrapper _repositoryWrapper;

        public DepositTypeService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<List<DepositType>> GetAll()
        {
            return await _repositoryWrapper.DepositType.FindAll();
        }

        public async Task<DepositType> GetById(int id)
        {
            var model = await _repositoryWrapper.DepositType
                .FindByCondition(x => x.DepositTypeId == id);
            if (model is null || model.Count == 0)
            {
                throw new ArgumentException("Not found");
            }
            return model.First();
        }

        public async Task Create(DepositType model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            if (string.IsNullOrEmpty(model.Name))
            {
                throw new ArgumentException(nameof(model.Name));
            }
            if (model.InterestRate <= 0)
            {
                throw new ArgumentException(nameof(model.InterestRate));
            }
            if (model.MinAmount < 0)
            {
                throw new ArgumentException(nameof(model.MinAmount));
            }
            if (model.MinTerm < 0)
            {
                throw new ArgumentException(nameof(model.MinTerm));
            }
            await _repositoryWrapper.DepositType.Create(model);
            _repositoryWrapper.Save();
        }

        public async Task Update(DepositType model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            if (string.IsNullOrEmpty(model.Name))
            {
                throw new ArgumentException(nameof(model.Name));
            }
            if (model.InterestRate <= 0)
            {
                throw new ArgumentException(nameof(model.InterestRate));
            }
            if (model.MinAmount < 0)
            {
                throw new ArgumentException(nameof(model.MinAmount));
            }
            if (model.MinTerm < 0)
            {
                throw new ArgumentException(nameof(model.MinTerm));
            }
            _repositoryWrapper.DepositType.Update(model);
            _repositoryWrapper.Save();
        }

        public async Task Delete(int id)
        {
            var model = await _repositoryWrapper.DepositType
                .FindByCondition(x => x.DepositTypeId == id);
            if (model is null || model.Count == 0)
            {
                throw new ArgumentException("Not found");
            }
            _repositoryWrapper.DepositType.Delete(model.First());
            _repositoryWrapper.Save();
        }
    }
}