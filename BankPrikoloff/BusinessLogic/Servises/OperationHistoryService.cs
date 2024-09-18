using BusinessLogic.Interfaces;
using Domain.Models;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Servises
{
    public class OperationHistoryService : IOperationHistoryService
    {
        private IRepositoryWrapper _repositoryWrapper;

        public OperationHistoryService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<List<OperationHistory>> GetAll()
        {
            return await _repositoryWrapper.OperationHistory.FindAll();
        }

        public async Task<OperationHistory> GetById(string id)
        {
            var operationHistory = await _repositoryWrapper.OperationHistory
                .FindByCondition(x => x.OperationId == id);
            return operationHistory.First();
        }

        public async Task Create(OperationHistory model)
        {
            await _repositoryWrapper.OperationHistory.Create(model);
            _repositoryWrapper.Save();
        }

        public async Task Update(OperationHistory model)
        {
            _repositoryWrapper.OperationHistory.Update(model);
            _repositoryWrapper.Save();
        }

        public async Task Delete(string id)
        {
            var operationHistory = await _repositoryWrapper.OperationHistory
                .FindByCondition(x => x.OperationId == id);

            _repositoryWrapper.OperationHistory.Delete(operationHistory.First());
            _repositoryWrapper.Save();
        }
    }
}
