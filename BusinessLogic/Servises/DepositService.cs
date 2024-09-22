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
            var deposit = await _repositoryWrapper.Deposit
                .FindByCondition(x => x.DepositId == id);
            return deposit.First();
        }

        public async Task Create(Deposit model)
        {
            await _repositoryWrapper.Deposit.Create(model);
            _repositoryWrapper.Save();
        }

        public async Task Update(Deposit model)
        {
            _repositoryWrapper.Deposit.Update(model);
            _repositoryWrapper.Save();
        }

        public async Task Delete(string id)
        {
            var deposit = await _repositoryWrapper.Deposit
                .FindByCondition(x => x.DepositId == id);

            _repositoryWrapper.Deposit.Delete(deposit.First());
            _repositoryWrapper.Save();
        }
    }
}