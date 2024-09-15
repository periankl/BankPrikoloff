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
    public class DepositService : IDepositService
    {
        private IRepositoryWrapper _repositoryWrapper;

        public DepositService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public Task<List<Deposit>> GetAll()
        {
            return _repositoryWrapper.Deposit.FindAll().ToListAsync();
        }

        public Task<Deposit> GetById(string id)
        {
            var deposit = _repositoryWrapper.Deposit
                .FindByCondition(x => x.DepositId == id).First();
            return Task.FromResult(deposit);
        }

        public Task Create(Deposit model)
        {
            _repositoryWrapper.Deposit.Create(model);
            _repositoryWrapper.Save();
            return Task.CompletedTask;
        }

        public Task Update(Deposit model)
        {
            _repositoryWrapper.Deposit.Update(model);
            _repositoryWrapper.Save();
            return Task.CompletedTask;
        }

        public Task Delete(string id)
        {
            var deposit = _repositoryWrapper.Deposit
                .FindByCondition(x => x.DepositId == id).First();

            _repositoryWrapper.Deposit.Delete(deposit);
            _repositoryWrapper.Save();
            return Task.CompletedTask;
        }
    }
}
    

