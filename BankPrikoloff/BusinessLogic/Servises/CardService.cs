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
    public class CardService : ICardService
    {
        private IRepositoryWrapper _repositoryWrapper;

        public CardService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public Task<List<Card>> GetAll()
        {
            return _repositoryWrapper.Card.FindAll().ToListAsync();
        }

        public Task<Card> GetById(string id)
        {
            var card = _repositoryWrapper.Card
                .FindByCondition(x => x.CardId == id).First();
            return Task.FromResult(card);
        }

        public Task Create(Card model)
        {
            _repositoryWrapper.Card.Create(model);
            _repositoryWrapper.Save();
            return Task.CompletedTask;
        }

        public Task Update(Card model)
        {
            _repositoryWrapper.Card.Update(model);
            _repositoryWrapper.Save();
            return Task.CompletedTask;
        }

        public Task Delete(string id)
        {
            var card = _repositoryWrapper.Card
                .FindByCondition(x => x.CardId == id).First();

            _repositoryWrapper.Card.Delete(card);
            _repositoryWrapper.Save();
            return Task.CompletedTask;
        }
    }
}
