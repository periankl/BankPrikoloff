﻿using BusinessLogic.Interfaces;
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
    public class LoanService : ILoanService
    {
        private IRepositoryWrapper _repositoryWrapper;

        public LoanService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<List<Loan>> GetAll()
        {
            return await _repositoryWrapper.Loan.FindAll();
        }

        public async Task<Loan> GetById(string id)
        {
            var model = await _repositoryWrapper.Loan
                .FindByCondition(x => x.LoanId == id);
            if (model is null || model.Count == 0)
            {
                throw new ArgumentException("Not found");
            }
            return model.First();
        }

        public async Task Create(Loan model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            if (string.IsNullOrEmpty(model.LoanId))
            {
                throw new ArgumentException(nameof(model.LoanId));
            }
            if (!model.LoanTypeId.HasValue)
            {
                throw new ArgumentException(nameof(model.LoanTypeId));
            }
            if (string.IsNullOrEmpty(model.AccountId))
            {
                throw new ArgumentException(nameof(model.AccountId));
            }
            if (model.StartDate > model.EndDate)
            {
                throw new ArgumentException(nameof(model.StartDate));
            }

            await _repositoryWrapper.Loan.Create(model);
            _repositoryWrapper.Save();
        }

        public async Task Update(Loan model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            if (string.IsNullOrEmpty(model.LoanId))
            {
                throw new ArgumentException(nameof(model.LoanId));
            }
            if (string.IsNullOrEmpty(model.DocumentId))
            {
                throw new ArgumentException(nameof(model.DocumentId));
            }
            if (string.IsNullOrEmpty(model.AccountId))
            {
                throw new ArgumentException(nameof(model.AccountId));
            }
            if (model.StartDate > model.EndDate)
            {
                throw new ArgumentException(nameof(model.StartDate));
            }
            _repositoryWrapper.Loan.Update(model);
            _repositoryWrapper.Save();
        }

        public async Task Delete(string id)
        {
            var model = await _repositoryWrapper.Loan
                .FindByCondition(x => x.LoanId == id);
            if (model is null || model.Count == 0)
            {
                throw new ArgumentException("Not found");
            }
            _repositoryWrapper.Loan.Delete(model.First());
            _repositoryWrapper.Save();
        }
    }
}