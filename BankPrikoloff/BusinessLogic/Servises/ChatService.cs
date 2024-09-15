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
    public class ChatService : IChatService
    {
        private IRepositoryWrapper _repositoryWrapper;

        public ChatService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public Task<List<Chat>> GetAll()
        {
            return _repositoryWrapper.Chat.FindAll().ToListAsync();
        }

        public Task<Chat> GetById(int id)
        {
            var chat = _repositoryWrapper.Chat
                .FindByCondition(x => x.ChatId == id).First();
            return Task.FromResult(chat);
        }

        public Task Create(Chat model)
        {
            _repositoryWrapper.Chat.Create(model);
            _repositoryWrapper.Save();
            return Task.CompletedTask;
        }

        public Task Update(Chat model)
        {
            _repositoryWrapper.Chat.Update(model);
            _repositoryWrapper.Save();
            return Task.CompletedTask;
        }

        public Task Delete(int id)
        {
            var chat = _repositoryWrapper.Chat
                .FindByCondition(x => x.ChatId == id).First();

            _repositoryWrapper.Chat.Delete(chat);
            _repositoryWrapper.Save();
            return Task.CompletedTask;
        }
    }
}
