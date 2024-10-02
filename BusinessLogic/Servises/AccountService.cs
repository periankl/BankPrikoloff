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
            var model = await _repositoryWrapper.Account
                .FindByCondition(x => x.AccountId == id);
            if (model is null || model.Count == 0)
            {
                throw new ArgumentException("Not found");
            }
            return model.First();
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
            if (model.CurrencyId < 1)
            {
                throw new ArgumentException(nameof(model.CurrencyId));
            }
            if (model.TypeId < 1)
            {
                throw new ArgumentException(nameof(model.TypeId));
            }
            model.AccountId = Guid.NewGuid().ToString("N").Substring(0, 9);

            await _repositoryWrapper.Account.Create(model);
            _repositoryWrapper.Save();
        }

        public async Task Update(Account model)
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
            _repositoryWrapper.Account.Update(model);
            _repositoryWrapper.Save();
        }

        public async Task Delete(string id)
        {
            var model = await _repositoryWrapper.Account
                .FindByCondition(x => x.AccountId == id);

            if (model is null || model.Count == 0)
            {
                throw new ArgumentException("Not found");
            }
            _repositoryWrapper.Account.Delete(model.First());
            _repositoryWrapper.Save();
        }
    }
}