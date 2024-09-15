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
    public class OperationHistoryService : IOperationHistoryService
    {
        private IRepositoryWrapper _repositoryWrapper;

        public OperationHistoryService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public Task<List<OperationHistory>> GetAll()
        {
            return _repositoryWrapper.OperationHistory.FindAll().ToListAsync();
        }

        public Task<OperationHistory> GetById(string id)
        {
            var operationHistory = _repositoryWrapper.OperationHistory
                .FindByCondition(x => x.OperationId == id).First();
            return Task.FromResult(operationHistory);
        }

        public Task Create(OperationHistory model)
        {
            _repositoryWrapper.OperationHistory.Create(model);
            _repositoryWrapper.Save();
            return Task.CompletedTask;
        }

        public Task Update(OperationHistory model)
        {
            _repositoryWrapper.OperationHistory.Update(model);
            _repositoryWrapper.Save();
            return Task.CompletedTask;
        }

        public Task Delete(string id)
        {
            var operationHistory = _repositoryWrapper.OperationHistory
                .FindByCondition(x => x.OperationId == id).First();

            _repositoryWrapper.OperationHistory.Delete(operationHistory);
            _repositoryWrapper.Save();
            return Task.CompletedTask;
        }
    }
}
