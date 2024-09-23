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
    public class AccountService : IAccountService
    {
        private IRepositoryWrapper _repositoryWrapper;

        public AccountService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<List<Account>> GetAll()
        {
            return await _repositoryWrapper.Account.FindAll();
        }

        public async Task<Account> GetById(string id)
        {
            var account = await _repositoryWrapper.Account
                .FindByCondition(x => x.AccountId == id);
            return account.First();
        }


        public async Task Create(Account model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));

            }
            if (string.IsNullOrEmpty(model.ClientId))
            {
                throw new ArgumentException(nameof(model.ClientId));
            }
            if (string.IsNullOrEmpty(model.AccountId))
            {
                throw new ArgumentException(nameof(model.AccountId));
            }
            if (model.UpdatedAt > DateTime.Now)
            {
                throw new ArgumentException(nameof(model.UpdatedAt));
            }
            await _repositoryWrapper.Account.Create(model);
            _repositoryWrapper.Save();
        }

        public async Task Update(Account model)
        {
            _repositoryWrapper.Account.Update(model);
            _repositoryWrapper.Save();
        }

        public async Task Delete(string id)
        {
            var account = await _repositoryWrapper.Account
                .FindByCondition(x => x.AccountId == id);

            _repositoryWrapper.Account.Delete(account.First());
            _repositoryWrapper.Save();
        }
    }
}