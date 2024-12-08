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
            var model = await _repositoryWrapper.OperationHistory
                .FindByCondition(x => x.OperationId == id);
            if (model is null || model.Count == 0)
            {
                throw new ArgumentException("Not found");
            }
            return model.First();
        }

        public async Task Create(OperationHistory model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            if (model.Amount < 0)
            {
                throw new ArgumentException(nameof(model.Amount));
            }
            if (model.SenderAccount == model.DestinationAccount & model.DestinationCardId == null)
            {
                throw new ArgumentException(nameof(model.SenderAccount));
            }
            if (model.SenderCard != null & model.DestinationCardId != null & (model.SenderAccountId == null | model.DestinationAccountId == null))
            {
                throw new ArgumentException(nameof(model.SenderCard));
            }
            await _repositoryWrapper.OperationHistory.Create(model);
            await _repositoryWrapper.Save();
        }

        public async Task Update(OperationHistory model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            if (string.IsNullOrEmpty(model.OperationId))
            {
                throw new ArgumentException(nameof(model.OperationId));
            }
            if (model.Amount < 0)
            {
                throw new ArgumentException(nameof(model.Amount));
            }
            if (model.SenderAccount == model.DestinationAccount & model.DestinationCardId == null)
            {
                throw new ArgumentException(nameof(model.SenderAccount));
            }
            if (model.SenderCard != null & model.DestinationCardId != null & (model.SenderAccountId == null | model.DestinationAccountId == null))
            {
                throw new ArgumentException(nameof(model.SenderCard));
            }
            await _repositoryWrapper.OperationHistory.Update(model);
            await _repositoryWrapper.Save();
        }

        public async Task Delete(string id)
        {
            var model = await _repositoryWrapper.OperationHistory
                .FindByCondition(x => x.OperationId == id);
            if (model is null || model.Count == 0)
            {
                throw new ArgumentException("Not found");
            }
            await _repositoryWrapper.OperationHistory.Delete(model.First());
            await _repositoryWrapper.Save();
        }
    }
}