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
    public class MessageService : IMessageService
    {
        private IRepositoryWrapper _repositoryWrapper;

        public MessageService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<List<Message>> GetAll()
        {
            return await _repositoryWrapper.Message.FindAll();
        }

        public async Task<Message> GetById(int id)
        {
            var model = await _repositoryWrapper.Message
                .FindByCondition(x => x.MessageId == id);
            if (model is null || model.Count == 0)
            {
                throw new ArgumentException("Not found");
            }
            return model.First();
        }

        public async Task Create(Message model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            if (string.IsNullOrEmpty(model.ClientId))
            {
                throw new ArgumentException(nameof(model.ClientId));
            }
            if (string.IsNullOrEmpty(model.Content))
            {
                throw new ArgumentException(nameof(model.Content));
            }
            await _repositoryWrapper.Message.Create(model);
            _repositoryWrapper.Save();
        }

        public async Task Update(Message model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            if (!model.MessageId.HasValue)
            {
                throw new ArgumentException(nameof(model.MessageId));
            }
            if (string.IsNullOrEmpty(model.ClientId))
            {
                throw new ArgumentException(nameof(model.ClientId));
            }
            if (string.IsNullOrEmpty(model.Content))
            {
                throw new ArgumentException(nameof(model.Content));
            }
            _repositoryWrapper.Message.Update(model);
            _repositoryWrapper.Save();
        }

        public async Task Delete(int id)
        {
            var model = await _repositoryWrapper.Message
                .FindByCondition(x => x.MessageId == id);
            if (model is null || model.Count == 0)
            {
                throw new ArgumentException("Not found");
            }
            _repositoryWrapper.Message.Delete(model.First());
            _repositoryWrapper.Save();
        }
    }
}