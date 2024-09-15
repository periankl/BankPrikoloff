using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Interfaces;
using DataAccess.Models;
using DataAccess.Wrapper;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Servises
{
    public class MessageService : IMessageService
    {
        private IRepositoryWrapper _repositoryWrapper;

        public MessageService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public Task<List<Message>> GetAll()
        {
            return _repositoryWrapper.Message.FindAll().ToListAsync();
        }

        public Task<Message> GetById(int id)
        {
            var message = _repositoryWrapper.Message.FindByCondition(x => x.MessageId == id).First();
            return Task.FromResult(message);
        }

        public Task Create(Message model)
        {
            _repositoryWrapper.Message.Create(model);
            _repositoryWrapper.Save();
            return Task.CompletedTask;
        }

        public Task Update(Message model)
        {
            _repositoryWrapper.Message.Update(model);
            _repositoryWrapper.Save();
            return Task.CompletedTask;
        }

        public Task Delete(int id)
        {
            var message = _repositoryWrapper.Message.FindByCondition(x => x.MessageId == id).First();
            _repositoryWrapper.Message.Delete(message);
            _repositoryWrapper.Save();
            return Task.CompletedTask;
        }
    }
}
