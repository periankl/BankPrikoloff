﻿using BusinessLogic.Interfaces;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        public async Task<List<Deposit>> GetAll()
        {
            return await _repositoryWrapper.Deposit.FindAll();
        }

        public async Task<Deposit> GetById(string id)
        {
            var model = await _repositoryWrapper.Deposit
                .FindByCondition(x => x.DepositId == id);
            if (model is null || model.Count == 0)
            {
                throw new ArgumentException("Not found");
            }
            return model.First();
        }

        public async Task<Deposit> GetByAccountId(string accountId)
        {
            var model = await _repositoryWrapper.Deposit
                .FindByCondition(x => x.AccountId == accountId);
            if (model is null || model.Count == 0)
            {
                throw new ArgumentException("Not found");
            }
            return model.First();
        }

        public async Task Create(Deposit model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            if (string.IsNullOrEmpty(model.DepositId))
            {
                throw new ArgumentException(nameof(model.DepositId));
            }
            if (string.IsNullOrEmpty(model.AccountId))
            {
                throw new ArgumentException(nameof(model.AccountId));
            }
            if (string.IsNullOrEmpty(model.Name))
            {
                throw new ArgumentException(nameof(model.Name));
            }
            if (model.StartDate > model.EndDate)
            {
                throw new ArgumentException(nameof(model.StartDate));
            }
            await _repositoryWrapper.Deposit.Create(model);
            await _repositoryWrapper.Save();
        }

        public async Task Update(Deposit model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            if (string.IsNullOrEmpty(model.DepositId))
            {
                throw new ArgumentException(nameof(model.DepositId));
            }
            if (string.IsNullOrEmpty(model.DocumentId))
            {
                throw new ArgumentException(nameof(model.DocumentId));
            }
            if (string.IsNullOrEmpty(model.AccountId))
            {
                throw new ArgumentException(nameof(model.AccountId));
            }
            if (string.IsNullOrEmpty(model.Name))
            {
                throw new ArgumentException(nameof(model.Name));
            }
            if (model.StartDate > model.EndDate)
            {
                throw new ArgumentException(nameof(model.StartDate));
            }
            await _repositoryWrapper.Deposit.Update(model);
            await _repositoryWrapper.Save();
        }

        public async Task Delete(string id)
        {
            var model = await _repositoryWrapper.Deposit
                .FindByCondition(x => x.DepositId == id);
            if (model is null || model.Count == 0)
            {
                throw new ArgumentException("Not found");
            }
            await _repositoryWrapper.Deposit.Delete(model.First());
            await _repositoryWrapper.Save();
        }
    }
}