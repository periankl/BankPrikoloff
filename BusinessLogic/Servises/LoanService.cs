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

        public async Task<Loan> GetByAccountId(string accountId)
        {
            var model = await _repositoryWrapper.Loan
                .FindByCondition(x => x.AccountId == accountId);
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

            // Проверка результатов запросов к базе данных
            var loanType = (await _repositoryWrapper.LoanType.FindByCondition(lt => lt.LoanTypeId == model.LoanTypeId))
                            .FirstOrDefault();

            if (loanType is null)
            {
                throw new ArgumentException("Loan type not found");
            }

            decimal maxAmount = loanType.MaxLoanAmount;

            if (model.Amount > maxAmount)
            {
                throw new ArgumentException("Amount more than max amount");
            }

            var accounts = await _repositoryWrapper.Account.FindByCondition(a => a.AccountId == model.AccountId);

            if (accounts.Count == 0)
            {
                throw new ArgumentException("Account not found");
            }

            if (accounts.Count > 1)
            {
                throw new ArgumentException("This account already has a loan");
            }

            var account = accounts.First();

            var user = (await _repositoryWrapper.User.FindByCondition(u => u.ClientId == account.ClientId)).FirstOrDefault();

            if (user == null)
            {
                throw new ArgumentException("User not found");
            }

            var userAccounts = (await _repositoryWrapper.Account.FindByCondition(ua => ua.ClientId == user.ClientId))
                                                                    .FindAll(ua => ua.TypeId == 2 && ua.StatusId == 1);

            if (userAccounts.Count > 0)
            {
                throw new ArgumentException("You already have a loan");
            }

            await _repositoryWrapper.Loan.Create(model);
            await _repositoryWrapper.Save();
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
            await _repositoryWrapper.Loan.Update(model);
            await _repositoryWrapper.Save();
        }

        public async Task Delete(string id)
        {
            var model = await _repositoryWrapper.Loan
                .FindByCondition(x => x.LoanId == id);
            if (model is null || model.Count == 0)
            {
                throw new ArgumentException("Not found");
            }
            await _repositoryWrapper.Loan.Delete(model.First());
            await _repositoryWrapper.Save();
        }
    }
}