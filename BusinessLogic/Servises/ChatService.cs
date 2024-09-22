﻿using BusinessLogic.Interfaces;
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
            var chat = await _repositoryWrapper.Chat
                .FindByCondition(x => x.ChatId == id);
            return chat.First();
        }

        public async Task Create(Chat model)
        {
            await _repositoryWrapper.Chat.Create(model);
            model.CreatedAt = DateTime.Now;
            _repositoryWrapper.Save();
        }

        public async Task Update(Chat model)
        {
            _repositoryWrapper.Chat.Update(model);
            _repositoryWrapper.Save();
        }

        public async Task Delete(int id)
        {
            var chat = await _repositoryWrapper.Chat
                .FindByCondition(x => x.ChatId == id);

            _repositoryWrapper.Chat.Delete(chat.First());
            _repositoryWrapper.Save();
        }
    }
}