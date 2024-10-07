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
    public class ChatService : IChatService
    {
        private IRepositoryWrapper _repositoryWrapper;

        public ChatService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<List<Chat>> GetAll()
        {
            return await _repositoryWrapper.Chat.FindAll();
        }

        public async Task<Chat> GetById(int id)
        {
            var model = await _repositoryWrapper.Chat
                .FindByCondition(x => x.ChatId == id);
            if (model is null || model.Count == 0)
            {
                throw new ArgumentException("Not found");
            }
            return model.First();
        }

        public async Task Create(Chat model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            await _repositoryWrapper.Chat.Create(model);
            model.CreatedAt = DateTime.Now;
            _repositoryWrapper.Save();
        }

        public async Task Update(Chat model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            _repositoryWrapper.Chat.Update(model);
            _repositoryWrapper.Save();
        }

        public async Task Delete(int id)
        {
            var model = await _repositoryWrapper.Chat
                .FindByCondition(x => x.ChatId == id);
            if (model is null || model.Count == 0)
            {
                throw new ArgumentException("Not found");
            }
            _repositoryWrapper.Chat.Delete(model.First());
            _repositoryWrapper.Save();
        }
    }
}