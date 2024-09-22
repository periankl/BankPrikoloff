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
            var depositType = await _repositoryWrapper.DepositType
                .FindByCondition(x => x.DepositTypeId == id);
            return depositType.First();
        }

        public async Task Create(DepositType model)
        {
            await _repositoryWrapper.DepositType.Create(model);
            _repositoryWrapper.Save();
        }

        public async Task Update(DepositType model)
        {
            _repositoryWrapper.DepositType.Update(model);
            _repositoryWrapper.Save();
        }

        public async Task Delete(int id)
        {
            var depositType = await _repositoryWrapper.DepositType
                .FindByCondition(x => x.DepositTypeId == id);

            _repositoryWrapper.DepositType.Delete(depositType.First());
            _repositoryWrapper.Save();
        }
    }
}