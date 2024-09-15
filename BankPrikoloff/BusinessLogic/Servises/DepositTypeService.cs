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
    public class DepositTypeService : IDepositTypeService
    {
        private IRepositoryWrapper _repositoryWrapper;

        public DepositTypeService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public Task<List<DepositType>> GetAll()
        {
            return _repositoryWrapper.DepositType.FindAll().ToListAsync();
        }

        public Task<DepositType> GetById(int id)
        {
            var depositType = _repositoryWrapper.DepositType
                .FindByCondition(x => x.DepositTypeId == id).First();
            return Task.FromResult(depositType);
        }

        public Task Create(DepositType model)
        {
            _repositoryWrapper.DepositType.Create(model);
            _repositoryWrapper.Save();
            return Task.CompletedTask;
        }

        public Task Update(DepositType model)
        {
            _repositoryWrapper.DepositType.Update(model);
            _repositoryWrapper.Save();
            return Task.CompletedTask;
        }

        public Task Delete(int id)
        {
            var depositType = _repositoryWrapper.DepositType
                .FindByCondition(x => x.DepositTypeId == id).First();

            _repositoryWrapper.DepositType.Delete(depositType);
            _repositoryWrapper.Save();
            return Task.CompletedTask;
        }
    }
}
