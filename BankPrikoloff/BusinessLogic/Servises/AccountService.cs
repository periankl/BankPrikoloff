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
    public class AccountService : IAccountService
    {
        private IRepositoryWrapper _repositoryWrapper;

        public AccountService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public Task<List<Account>> GetAll()
        {
            return _repositoryWrapper.Account.FindAll().ToListAsync();
        }

        public Task<Account> GetById(string id)
        {
            var account = _repositoryWrapper.Account
                .FindByCondition(x => x.AccountId == id).First();
            return Task.FromResult(account);
        }

        public Task Create(Account model)
        {
            _repositoryWrapper.Account.Create(model);
            _repositoryWrapper.Save();
            return Task.CompletedTask;
        }

        public Task Update(Account model)
        {
            _repositoryWrapper.Account.Update(model);
            _repositoryWrapper.Save();
            return Task.CompletedTask;
        }

        public Task Delete(string id)
        {
            var account = _repositoryWrapper.Account
                .FindByCondition(x => x.AccountId == id).First();

            _repositoryWrapper.Account.Delete(account);
            _repositoryWrapper.Save();
            return Task.CompletedTask;
        }
    }
}
